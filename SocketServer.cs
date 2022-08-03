using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json.Linq;
using CourseHandler;

namespace SocketServer
{
    //Object that 
    public class EmulatorResponse
    {
        public int trackId;
        public string function;

        public EmulatorResponse(int tid, string func)
        {
            trackId = tid;
            function = func;
        }
    }

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
                            //Parse data into EmulatorResponse Object
                            EmulatorResponse emuResp = parse_response(data);

                            //(Temp) Check the CourseHandler files to see if we have a ghost
                            JObject replyResp = CoursePaths.course_file_json(emuResp.trackId);

                            //Write response, delete console.writelline maybe
                            string stringResp = replyResp.ToString();
                            Console.WriteLine("Sending this: " + stringResp);

                            //Send response, emulator will continue once response returns.
                            handler.Send(Encoding.ASCII.GetBytes(stringResp));
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
                        finally
                        {
                            //Have to shut down, and re-listen to continue
                            handler.Shutdown(SocketShutdown.Both);
                            handler = listener.Accept();
                        }
                        break;
                    }
                }


            }
            handler.Close();
        }

        public static EmulatorResponse parse_response(string data)
        {
            try
            {
                //Parse json, discard <EOF>
                string[] jstring = data.Split(new[] { "<EOF>" }, StringSplitOptions.None);
                Console.WriteLine("Data: " + data); //TODO--delete
                JObject jstr = JObject.Parse(jstring[0]);
                Console.WriteLine("Received json: " + jstr);
                int trackId = Convert.ToInt32(jstr["trackId"]); ;
                string func = (string)jstr["function"];
                return new EmulatorResponse(trackId , func );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
