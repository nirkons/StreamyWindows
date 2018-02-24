using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Reflection;
using System.IO;
using Microsoft.Win32;
using System.Security.Permissions;
using System.Security.Principal;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using System.Text.RegularExpressions;

namespace Streamy
{
    public partial class Form1 : Form
    {
        //import required libraries and functions for video and music service functionality
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        public delegate bool Win32Callback(IntPtr hwnd, IntPtr lParam);

        [DllImport("user32.dll")]
        protected static extern bool EnumWindows(Win32Callback enumProc, IntPtr lParam);

        private static bool EnumWindow(IntPtr handle, IntPtr pointer)
        {
            List<IntPtr> pointers = GCHandle.FromIntPtr(pointer).Target as List<IntPtr>;
            pointers.Add(handle);
            return true;
        }
        [DllImport("user32.dll")]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        private static List<IntPtr> GetAllWindows()
        {
            Win32Callback enumCallback = new Win32Callback(EnumWindow);
            List<IntPtr> pointers = new List<IntPtr>();
            GCHandle listHandle = GCHandle.Alloc(pointers);
            try
            {
                EnumWindows(enumCallback, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                if (listHandle.IsAllocated) listHandle.Free();
            }
            return pointers;
        }

        [DllImport("User32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr windowHandle, StringBuilder stringBuilder, int nMaxCount);

        [DllImport("user32.dll", EntryPoint = "GetWindowTextLength", SetLastError = true)]
        internal static extern int GetWindowTextLength(IntPtr hwnd);
        private static string GetTitle(IntPtr handle)
        {
            int length = GetWindowTextLength(handle);
            StringBuilder sb = new StringBuilder(length + 1);
            GetWindowText(handle, sb, sb.Capacity);
            return sb.ToString();
        }

        [DllImport("user32.dll")]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);


        public Form1()
        {
            InitializeComponent();
            //Setup tooltip for "close video streaming windows" checkbox
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.closevideobox, "Netflix does not allow multiple streaming tabs on the same browser, so this will close the open tab");

            //Setup configuration
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            

            if ((config.AppSettings.Settings["username"].Value != "{{usernameval}}"))
            {
                videonotconfiguredlabel.Visible = false;
                configurevideo.Visible = false;
                videoconfiguredlbl.Visible = true;
                confvideoagain.Visible = true;
            }

            
            //Check if program is running as admin, in order to startup with windows
            bool isadmin = IsAdministrator();
            if (isadmin==false)
            {
                checkBox1.Enabled = false;
                runasadminlbl.Visible = true;
            }
            //Check if VLC is installed
            try
            {
                if (IsSoftwareInstalled("VLC") == true)
                {
                    vlcinstalled.Visible = false;
                    downloadVLC.Visible = false;
                    configurevlc.Visible = true;
                    vlcnotconfiguredlabel.Visible = true;


                    //check if vlc is configured to allow controlling vlc via HTTP
                    string path = (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\vlc\\vlcrc");
                    if (File.ReadLines(path).Any(line => line.Contains("streamyarduino2018")) == true)
                    {
                        vlcisconfiguredlbl.Visible = true;
                        vlcnotconfiguredlabel.Visible = false;

                    }

                }
            }
            catch (Exception vlc)
            {

            }
            

            //Call function to check if Spotify is running every few seconds
            InitTimer();
            

        }

        //Check every few seconds if Spotify is running
        private System.Windows.Forms.Timer timer1;
        public void InitTimer()
        {
            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 2800; // in miliseconds
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //check if Spotify service app is running
         
                Process[] P = Process.GetProcessesByName("Spotify");

                if (P.Length > 0)
                {

                    label10.Visible = true;
                    label8.Visible = false;

                }
                else
                {
                    label8.Visible = true;
                    label10.Visible = false;
                }
            

        }

        public void LaunchVLC()
        {

            //get settings
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            string blynkauthkey = config.AppSettings.Settings["username"].Value;
            
                string html = "";
                string url = @"http://blynk-cloud.com/" + blynkauthkey + "/get/V2";

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                using (HttpWebResponse blynkresponse = (HttpWebResponse)request.GetResponse())
                using (Stream stream = blynkresponse.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }
            }
            catch (Exception es)
            {

            }
                

            //clean html and get only the list of IPS
            html = Regex.Replace(html, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
            string[] ips = new string[3];
                //get index of fourth dot
                int fourth = ordinalIndexOf(html, ".", 4);
                //get first IP
                ips[0] = html.Substring(0, fourth);
                //get index of eighth dot
                int eighth = ordinalIndexOf(html, ".", 8);
                //get second IP
                ips[1] = html.Substring(fourth + 1, eighth-fourth-1);
                //get third IP
                ips[2] = html.Substring(eighth+1, html.Length-eighth-1);

                for (int i = 0; i < ips.Length; i++)
                {
                Console.WriteLine(ips[i]);
                    try
                    {
                    //get information about currently playing media file
                    var client = new MyWebClient { Credentials = new NetworkCredential("", "streamyarduino2018") };

                    //vlc port 8080 requires basic authentication, only password
                    string credentials = Convert.ToBase64String(
                        Encoding.ASCII.GetBytes(":streamyarduino2018"));
                    client.Headers[HttpRequestHeader.Authorization] = string.Format(
                        "Basic {0}", credentials);
                    //download the xml file containing the status
                    string response = client.DownloadString("http://" + ips[i] + ":8080/requests/status.xml");
                    string response2 = client.DownloadString("http://" + ips[i] + ":8080/requests/playlist.xml");
                    
                    if (response != null && response2 != null)
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(response);

                        //get filename
                        int firstpart = response2.LastIndexOf("file:///") + 11;
                        int lastpart = response2.Length - response2.IndexOf("current=") +2;
                        string filename = response2.Substring(firstpart, response2.Length-firstpart-lastpart);
                        Console.WriteLine(filename);
                        //try to get which folder was shared
                        int count = filename.Length - filename.Replace("/", "").Length;

                        //fallback, populate if next loop doesnt work
                        string finalpath = "\\\\" + ips[i] + "\\" + filename.Replace("/", "\\");


                        //get the actual location of the file
                        for (int x = 0; x< count; x++)
                        {
                            try
                            {
                                if (File.Exists("\\\\" + ips[i] + "\\" + filename.Replace("/", "\\")) == true)
                                {
                                    finalpath = "\\\\" + ips[i] + "\\" + filename.Replace("/", "\\");
                                    x = count;
                                }
                                else
                                {
                                    filename = filename.Substring(filename.IndexOf("/") + 1, filename.Length - filename.IndexOf("/")-1);
                                }
                            }
                            catch (Exception xx)
                            {
                            }
                            
                        }
                        string pathToExe = getSoftwareLocation("VLC media player") + "\\vlc.exe";

                        string xpath = "/root/time/text()";
                        var nodes = xmlDoc.SelectNodes(xpath);
                        //get the time the video should start in seconds
                        int starttime = int.Parse(nodes[0].InnerText);
                        //set the launch arguments
                        string launchArguments = finalpath + " --start-time=" + starttime;
                        Console.WriteLine(launchArguments);
                        Process p = new Process();
                        p.StartInfo.FileName = pathToExe; //filePath of the application
                        p.StartInfo.Arguments = launchArguments;
                        p.StartInfo.CreateNoWindow = true;
                        p.Start();
                        
                    }

                    //stop the loop on success
                    i = ips.Length;
                    }
                    catch (Exception ex)
                    {
                    }
                }
        }
            
        

        private string GetAppPath(string productName)
        {
            const string foldersPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Installer\Folders";
            var baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);

            var subKey = baseKey.OpenSubKey(foldersPath);
            if (subKey == null)
            {
                baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
                subKey = baseKey.OpenSubKey(foldersPath);
            }
            return subKey != null ? subKey.GetValueNames().FirstOrDefault(kv => kv.Contains(productName)) : "ERROR";
        }
       
        //Go to system tray
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                Hide();
        }

        //Restore window on system tray double click
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }
        //video service configure
        private void button3_Click(object sender, EventArgs e)
        {
            configurevideo.Visible = false;
            usernamelabel.Visible = true;
            Username.Visible = true;
            confvideobtn.Visible = true;
        }

        //configure VLC configuration file
        private void configurevlc_Click(object sender, EventArgs e)
        {
            try
            {
                string path = (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\vlc\\vlcrc");

                // replaces #extraintf= with extraintf=http
                // replaces #http-password= with http-password=streamyarduino2018
                string text = File.ReadAllText(path);
                text = text.Replace("#extraintf=", "extraintf=http");
                //text = text.Replace("#http-password=", "http-password=streamyarduino2018");
                text = text.Replace("#http-password=", "http-password=streamyarduino2018");
                File.WriteAllText(path, text);


                if (File.ReadLines(path).Any(line => line.Contains("streamyarduino2018")) == true)
                {
                    vlcisconfiguredlbl.Visible = true;
                    vlcnotconfiguredlabel.Visible = false;
                    log.Items.Add("VLC Configured, if VLC is running, please restart it");

                }
                else
                {
                    log.Items.Add("Failed to configure VLC, did you launch VLC at least once? if so try this:");

                    log.Items.Add(@"Please go to C:\Users\YourUser\AppData\Roaming\vlc, open up vlcrc file, find #extraintf= and replace it with extraintf=http, find #http-password= and replace it with http-password=streamyarduino2018");

                }
            }
            catch (Exception vlconf)
            {
                log.Items.Add("Failed to configure VLC, did you launch VLC at least once? if so try this:");

                log.Items.Add(@"Please go to C:\Users\YourUser\AppData\Roaming\vlc, open up vlcrc file, find #extraintf= and replace it with extraintf=http, find #http-password= and replace it with http-password=streamyarduino2018");

            }

        }

        //Download VLC media player button
        private void downloadVLC_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.videolan.org/vlc/index.html");

        }

        //start the async server to accept connections from Arduino
        private void Form1_Load(object sender, EventArgs e)
        {
            Task.Run(() => { AsynchronousSocketListener asl = new AsynchronousSocketListener(); });
        }

        private void confvideobtn_Click(object sender, EventArgs e)
        {
            ////get configuration file
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            //require both fields, remove if existing username is present in app config file
            string oldusername = "{{usernameval}}";
            if ((Username.Text != null))
            {
                if (config.AppSettings.Settings["username"].Value != "")
                {
                    oldusername = config.AppSettings.Settings["username"].Value;
                    
                }
                
                config.AppSettings.Settings.Remove("username");
                config.AppSettings.Settings.Add("username", Username.Text);

                //hide and show fields
                confvideobtn.Visible = false;

                Username.Visible = false;
                usernamelabel.Visible = false;
                confvideoagain.Visible = true;
                videoconfiguredlbl.Visible = true;
                videonotconfiguredlabel.Visible = false;


            }

            // Save the changes in App.config file.
            config.Save(ConfigurationSaveMode.Modified);

            // Force a reload of a changed section.
            ConfigurationManager.RefreshSection("appSettings");
        }
        //configure blynk auth again
        private void confvideoagain_Click(object sender, EventArgs e)
        {
            videoconfiguredlbl.Visible = false;
            confvideoagain.Visible = false;
            configurevideo.Visible = false;
            usernamelabel.Visible = true;
            Username.Visible = true;
            confvideobtn.Visible = true;

        }
        //Add to startup checkbox
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                AddApplicationToStartup();
                log.Items.Add("Added to startup");
            }
            if (checkBox1.Checked == false)
            {
                RemoveApplicationFromStartup();
                log.Items.Add("Removed from startup");
            }

        }
        public static void AddApplicationToStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.SetValue("Streamy", "\"" + Application.ExecutablePath + "\"");
            }
        }

        public static void RemoveApplicationFromStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.DeleteValue("Streamy", false);
            }
        }
        //check if running as administrator
        public static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        //Check if certain software is installed on the computer
        private static bool IsSoftwareInstalled(string softwareName)
        {
            var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall") ??
                      Registry.LocalMachine.OpenSubKey(
                          @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");

            if (key == null)
                return false;

            return key.GetSubKeyNames()
                .Select(keyName => key.OpenSubKey(keyName))
                .Select(subkey => subkey.GetValue("DisplayName") as string)
                .Any(displayName => displayName != null && displayName.Contains(softwareName));
        }
        //Get installation path of certain software
        private static string getSoftwareLocation(string softwareName)
        {

            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\"+softwareName) ??
                      Registry.LocalMachine.OpenSubKey(
                          @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\"+ softwareName))
                {
                    if (key != null)
                    {
                        Object o = key.GetValue("InstallLocation");
                        if (o != null)
                        {
                            string location = o as string;  //"as" because it's REG_SZ...otherwise ToString() might be safe(r)
                            return location;                                             //do what you like with version
                        }
                    }
                }
            }
            catch (Exception e)  //just for demonstration...it's always best to handle specific exceptions
            {
                //react appropriately
                // Get stack trace for the exception with source file information
                var st = new StackTrace(e, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();
                Console.WriteLine(line);
            }
            return null;

        }

        //Get last watched URL
        public string LastWatched()
        {
            ////get configuration file
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            string BLYNKAPIKEY = config.AppSettings.Settings["username"].Value;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://blynk-cloud.com/" + BLYNKAPIKEY + "/get/V4");
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                string result;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
                //get only the integer ID of the show from the GET request result
                string resultString = Regex.Match(result, @"\d+").Value;
                return resultString;
            }
            catch (Exception e)
            {
                log.Items.Add("Problem with Netflix account or wrong Blynk API key");
                log.Items.Add(e.ToString());
                return e.ToString();
            
            }
            
        }



        //Add/update values to settings file
        private void WriteToSettings(string keyname, string value)
        {
            ////get configuration file
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings.Remove(keyname);
            config.AppSettings.Settings.Add(keyname, value);
            // Save the changes in App.config file.
            config.Save(ConfigurationSaveMode.Modified);

            // Force a reload of a changed section.
            ConfigurationManager.RefreshSection("appSettings");
        }
        //read value from settings
        public string ReadFromSettings(string key)
        {
            //Setup configuration
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (config.AppSettings.Settings[key] != null || config.AppSettings.Settings[key].Value != "")
            {
                return config.AppSettings.Settings[key].Value;
            }
            else
            {
                return "";
            }
        }

        //Open video streaming service and close existing windows
        public void OpenVideoService()
        {

            string lastwatchedlink = LastWatched();

                //close windows
                List<IntPtr> ls = GetAllWindows();
                bool once = false;
                foreach (var tb in ls)
                {
                    if ((GetTitle(tb).Contains("netflix") || GetTitle(tb).Contains("Netflix")) && (GetTitle(tb).Contains("Google Chrome") && once == false))
                    {
                        IntPtr zero = FindWindow(null, GetTitle(tb));
                        SwitchToThisWindow(zero, true);
                        SetForegroundWindow(zero);
                        SendKeys.SendWait("^W");
                        SendKeys.Flush();
                        once = true;
                    }
                }

                System.Diagnostics.Process.Start("https://www.netflix.com/watch/"+lastwatchedlink);
                System.Threading.Thread.Sleep(5500);
                ls = GetAllWindows();
                //full screen video
                foreach (var tb in ls)
                {
                    if ((GetTitle(tb).Contains("netflix") || GetTitle(tb).Contains("Netflix")) && (GetTitle(tb).Contains("Google Chrome")))
                    {
                        IntPtr zero = FindWindow(null, GetTitle(tb));
                        SwitchToThisWindow(zero, true);
                        SetForegroundWindow(zero);
                        SendKeys.SendWait("{F11}");
                        SendKeys.Flush();
                    }
                }
            }


        //open Music stream
        public void MusicStream()
        {

            Process[] P = Process.GetProcessesByName("Spotify");

            if (P.Length <= 0)
            {
                Process proc = Process.Start("C:\\Users\\"+Environment.UserName+"\\AppData\\Roaming\\Spotify\\Spotify.exe");
                
                while (string.IsNullOrEmpty(proc.MainWindowTitle) || proc.MainWindowTitle=="")
                {
                    System.Threading.Thread.Sleep(1500);
                    proc.Refresh();
                    System.Threading.Thread.Sleep(1500);
                }

                System.Threading.Thread.Sleep(1500);
            }
            else
            {
                System.Threading.Thread.Sleep(1000);
            }
            P = Process.GetProcessesByName("Spotify");
            if (P.Length > 0)
            {
                string spotwindow = "";
                int counter = 0;
                while (spotwindow == "" && counter != P.Length)
                {
                    if (P[counter].MainWindowTitle != "")
                    {
                        spotwindow = P[counter].MainWindowTitle;
                        counter = P.Length;
                    }
                    else
                    {
                        counter++;
                    }
                }

                List<IntPtr> ls = GetAllWindows();

                foreach (var tb in ls)
                {
                    if (GetTitle(tb).Contains("Spotify") || GetTitle(tb).Contains(spotwindow))
                    {
                        IntPtr zero = FindWindow(null, GetTitle(tb));
                        SwitchToThisWindow(zero, true);
                        SetForegroundWindow(zero);
                        SendKeys.SendWait(" ");
                        SendKeys.Flush();
                    }
                }
            }
      }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIcon1.Dispose();
        }

        

        public int ordinalIndexOf(string str, string substr, int n)
        {
            int pos = str.IndexOf(substr);
            while (--n > 0 && pos != -1)
                pos = str.IndexOf(substr, pos + 1);
            return pos;
        }

        private class MyWebClient : WebClient
        {
            protected override WebRequest GetWebRequest(Uri uri)
            {
                WebRequest w = base.GetWebRequest(uri);
                w.Timeout = Convert.ToInt32(TimeSpan.FromSeconds(2).TotalMilliseconds);
                return w;
            }
        }
    }
}
