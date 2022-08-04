using System;
using SocketServer;
using GhostDownload;
using GhostWriter;

namespace MainSpace
{
    class RunProgram
    {
        public static void Main(string[] args)
        {
            GhostQuery gq = new GhostQuery();
            gq.get_record(8, "3lap");
            string jsonFile = gq.download_ghost_json();
            WriteGhost.write_file(jsonFile);
            Console.WriteLine("Done!");
        }
        public static void tempMain(string[] args)
        {
            //kick's off the watcher loop
            SynchronousSocketServer mk64socket = new SynchronousSocketServer();
            mk64socket.create_listener();
            mk64socket.run_handler();
            Console.WriteLine("Done!");
        }
    }
}