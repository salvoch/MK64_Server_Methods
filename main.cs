﻿using System;
using SocketServer;

namespace MainSpace
{
    class RunProgram
    {
        public static void Main(string[] args)
        {
            //kick's off the watcher loop
            SynchronousSocketServer mk64socket = new SynchronousSocketServer();
            mk64socket.create_listener();
            mk64socket.run_handler();
            Console.WriteLine("Done!");
        }
    }
}