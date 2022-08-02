using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json.Linq;
using CourseHandler;

namespace SocketServer
{
    public class SynchronousSocketServer
    {
        // Data buffer for incoming data.
        public static byte[] bytes = new byte[1024];
        public Socket listener;
        public Socket handler;

        public void create_listener(int port = 8064)
        {
            //create IPEndPoint as localhost:port
            IPHostEntry ipHostInfo = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

            // Create a TCP/IP socket listener
            listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            //bind IP+port, and start listening
            listener.Bind(localEndPoint);
            listener.Listen(100);
        }

        public void run_handler()
        {
            Console.WriteLine("Waiting for a connection...");

            //Variable initiations to check if queries are successful
            int trackId = -1;

            //Initiate handler socket
            handler = listener.Accept();

            //Enter loop to wait for input from emulator
            while (true)
            {
                String data = null;
                while (true)
                {
                    //Receive ASCII, decode into data
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    //Search for "<EOF>" String, break when received, close connection.
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        try
                        {
                            //Parse json, discard <EOF>
                            string[] jstring = data.Split(new[] { "<EOF>" }, StringSplitOptions.None);
                            Console.WriteLine("Data: " + data); //TODO--delete
                            JObject jstr = JObject.Parse(jstring[0]);
                            Console.WriteLine("Received json: " + jstr);
                            trackId = Convert.ToInt32(jstr["trackId"]);
                        }
                        catch (Newtonsoft.Json.JsonReaderException jsonEx)
                        {
                            Console.WriteLine(jsonEx.Message);
                            Console.WriteLine("Could not parse JSON from PJ64 response: " + data);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    }
                }

                //(Temp) Check the CourseHandler files to see if we have a ghost
                JObject resp = CoursePaths.course_file_json(trackId);

                //Write response, delete maybe
                string stringResp = resp.ToString();
                Console.WriteLine("Sending this: " + stringResp);

                //Have to send, shut down, and re-accept or emulator hangs
                handler.Send(Encoding.ASCII.GetBytes(stringResp));
                handler.Shutdown(SocketShutdown.Both);
                handler = listener.Accept();
                trackId = -1;
            }

            handler.Close();
        }
    }
}
