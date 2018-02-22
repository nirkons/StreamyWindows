using Streamy;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

//Taken from https://docs.microsoft.com/en-us/dotnet/framework/network-programming/asynchronous-server-socket-example
//And modified to fit this project's needs
//State object for reading client data asynchronously  

public class StateObject
{
    // Client  socket.  
    public Socket workSocket = null;
    // Size of receive buffer.  
    public const int BufferSize = 1024;
    // Receive buffer.  
    public byte[] buffer = new byte[BufferSize];
    // Received data string.  
    public StringBuilder sb = new StringBuilder();
}

public class AsynchronousSocketListener 
{
    // Thread signal.  
    public static ManualResetEvent allDone = new ManualResetEvent(false);

    public AsynchronousSocketListener()
    {
        StartListening();
        
    }

    public static void StartListening()
    {
        // Data buffer for incoming data.  
        byte[] bytes = new Byte[1024];

        // Establish the local endpoint for the socket.  
        // The DNS name of the computer  
        // running the listener is "host.contoso.com".  
        //IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());

        IPAddress ipAddress = Dns.GetHostEntry("localhost").AddressList[0];

        //get local network IP
        
        using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
        {
            socket.Connect("8.8.8.8", 65530);
            IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
            ipAddress = endPoint.Address;
        }

        //alternative method of getting the internal network IP
        /* 
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                ipAddress = ip;
            }
        }
        */
        
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 13000);

        // Create a TCP/IP socket.  
        Socket listener = new Socket(ipAddress.AddressFamily,
            SocketType.Stream, ProtocolType.Tcp);

        // Bind the socket to the local endpoint and listen for incoming connections.  
        try
        {
            listener.Bind(localEndPoint);
            listener.Listen(100);

            while (true)
            {
                // Set the event to nonsignaled state.  
                allDone.Reset();

                // Start an asynchronous socket to listen for connections.  
                Console.WriteLine("Waiting for a connection...");
                listener.BeginAccept(
                    new AsyncCallback(AcceptCallback),
                    listener);

                // Wait until a connection is made before continuing.  
                allDone.WaitOne();
            }

        }
        catch (Exception e)
        {
            // Get stack trace for the exception with source file information
            var st = new StackTrace(e, true);
            // Get the top stack frame
            var frame = st.GetFrame(0);
            // Get the line number from the stack frame
            var line = frame.GetFileLineNumber();
            Console.WriteLine(line);


        }

        //Console.WriteLine("\nPress ENTER to continue...");
        //Console.Read();

    }

    public static void AcceptCallback(IAsyncResult ar)
    {
        // Signal the main thread to continue.  
        allDone.Set();

        // Get the socket that handles the client request.  
        Socket listener = (Socket)ar.AsyncState;
        Socket handler = listener.EndAccept(ar);

        // Create the state object.  
        StateObject state = new StateObject();
        state.workSocket = handler;
        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
            new AsyncCallback(ReadCallback), state);
    }

    public static void ReadCallback(IAsyncResult ar)
    {

        String content = String.Empty;

        // Retrieve the state object and the handler socket  
        // from the asynchronous state object.  
        StateObject state = (StateObject)ar.AsyncState;
        Socket handler = state.workSocket;

        // Read data from the client socket.   
        int bytesRead = handler.EndReceive(ar);

        if (bytesRead > 0)
        {
            // There  might be more data, so store the data received so far.  
            state.sb.Append(Encoding.ASCII.GetString(
                state.buffer, 0, bytesRead));

            // Check for end-of-file tag. If it is not there, read   
            // more data.  
            content = state.sb.ToString();
            if (content.IndexOf("<EOF>") > -1 || content.IndexOf("-EOF-") > -1 )
            {
                // All the data has been read from the   
                // client. Display it on the console.  
                Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",content.Length, content);
                
                // Echo the data back to the client.  
                //Send(handler, content);
                Send(handler, "200");

                string getdata = content.Substring((content.IndexOf("?") + 1),1);

                //launch the functions according to the data received
                if (getdata == "1")
                {
                    if (System.Windows.Forms.Application.OpenForms["Form1"] != null)
                    {
                        (System.Windows.Forms.Application.OpenForms["Form1"] as Form1).OpenVideoService();
                    }
                }
                else if (getdata =="2")
                {
                    if (System.Windows.Forms.Application.OpenForms["Form1"] != null)
                    {
                        (System.Windows.Forms.Application.OpenForms["Form1"] as Form1).MusicStream();
                    }
                }
                else if (getdata == "4")
                {
                    if (System.Windows.Forms.Application.OpenForms["Form1"] != null)
                    {
                        (System.Windows.Forms.Application.OpenForms["Form1"] as Form1).stopSong();
                    }
                }
                else if (getdata == "3")
                {
                    if (System.Windows.Forms.Application.OpenForms["Form1"] != null)
                    {
                        (System.Windows.Forms.Application.OpenForms["Form1"] as Form1).LaunchVLC();
                    }
                }



            }
            else
            {
                // Not all data received. Get more.  
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
            }
        }

        //PERFORM TASKS WITH CONTENT STRING


    }

    private static void Send(Socket handler, String data)
    {
        //SEND BACK TO ARDUINO


        // Convert the string data to byte data using ASCII encoding.  
        byte[] byteData = Encoding.ASCII.GetBytes(data);

        // Begin sending the data to the remote device.  
        handler.BeginSend(byteData, 0, byteData.Length, 0,
            new AsyncCallback(SendCallback), handler);
    }

    private static void SendCallback(IAsyncResult ar)
    {
        try
        {
            // Retrieve the socket from the state object.  
            Socket handler = (Socket)ar.AsyncState;

            // Complete sending the data to the remote device.  
            int bytesSent = handler.EndSend(ar);
            Console.WriteLine("Sent {0} bytes to client.", bytesSent);

            handler.Shutdown(SocketShutdown.Both);
            handler.Close();

        }
        catch (Exception e)
        {
            // Get stack trace for the exception with source file information
            var st = new StackTrace(e, true);
            // Get the top stack frame
            var frame = st.GetFrame(0);
            // Get the line number from the stack frame
            var line = frame.GetFileLineNumber();
            Console.WriteLine(line);

        }
    }
}