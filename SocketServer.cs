using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json.Linq;

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
            bool courseCheck = false;

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
                            int trackId = Convert.ToInt32(jstr["trackId"]);
                            courseCheck = checkCourseExists(trackId);
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

                JObject resp = new JObject
                {
                    ["available"] = courseCheck,
                    ["path"] = "A:\\Emulators\\NEWERVERSION\\Scripts\\tempfiles\\ghost-dump.json" // TODO -- Figure out how to find this
                };

                //Write response, delete maybe
                string stringResp = resp.ToString();
                Console.WriteLine("Sending this: " + stringResp);

                //Have to send, shut down, and re-accept or emulator hangs
                handler.Send(Encoding.ASCII.GetBytes(stringResp));
                handler.Shutdown(SocketShutdown.Both);
                handler = listener.Accept();

                courseCheck = false;
            }

            handler.Close();
        }
        public static bool checkCourseExists(int courseId)
        {
            /*TODO - how do we check this in WPF? Logic here to ckeck if a courseID has a ghost or not
             * Breakdown:
             *  Check if the user has this cousre selected as flap or 3lap (or none) in wpf
             *      If it is selected, 
             *          Query gus' server with type/courseid
             *          If it exists:
             *              download file
             *              Write file for reading
             *              Return True
             *          If it doesn't
             *              Write to WPF that it doesn't exist (Or is this check done before)
             *              reutn False
             *      If it is not selected in either: 
             *          return false
             */

            //For now, return true if ID is luigi
            if (courseId == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
