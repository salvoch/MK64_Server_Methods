using System;
using SocketServer;
using GhostDownload;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using Win32Form1Namespace;

namespace MainSpace
{
    class RunProgram
    {
        public static void Main(string[] args)
        {
            //Create the input dialog box with the parameters below
            //string input = Interaction.InputBox("What is at the end of the rainbow?", "Riddle", "...", 10, 10);
            //System.Windows.Forms.ComboBox comboBox1 = new System.Windows.Forms.ComboBox();
            Win32Form1 form1 = new Win32Form1();
            System.Windows.Forms.Application.Run(form1);
            Console.WriteLine(form1.type_result);
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