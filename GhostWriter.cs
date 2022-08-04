using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostWriter
{
    class WriteGhost
    {
        //return the path of the file
        public static string write_file(string jsonString)
        {
            //TODO - get path of current execution, append the \\ghostloader\\ghost.json to it
            var path = "A:\\Emulators\\NEWERVERSION\\Scripts\\ghostloader\\ghost.json";
            File.WriteAllText(path, jsonString);
            return path;
        }
    }
}
