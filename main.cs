﻿using System;
using SocketServer;
using GhostDownload;

namespace MainSpace
{
    class RunProgram
    {
        public static void Main(string[] args)
        {
            GhostQuery gq = new GhostQuery();
            gq.get_record(8, "3lap");
            gq.download_ghost_json();

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