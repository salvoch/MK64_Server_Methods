//TODO - DELETE THIS COMPLETELY

using Newtonsoft.Json.Linq;

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

namespace CourseHandler
{
    public class CoursePaths
    {
        //TODO - Extract path from user somehow
        public static string dirpath = "A:\\Emulators\\NEWERVERSION\\Scripts\\tempfiles\\";

        //Returns a JSON with two keys:
        //  "available" -- if the course is available
        //  "path" -- the path of the file
        public static JObject course_file_json(int courseId)
        {
            //List of our current local records, this will be pulled from the server
            bool available;
            string filename = "NA";

            //Match the course ID
            switch (courseId)
            {
                //Mushroom Cup
                case 0:
                    available = true;
                    filename = "luigi - flap.mpk.json";
                    break;
                case 1:
                    available = false;
                    filename = "NA";
                    break;
                case 2:
                    available = true;
                    filename = "koopa-flap.mpk.json";
                    break;
                case 3:
                    available = false;
                    filename = "NA";
                    break;

                //Flower Cup
                case 4:
                    available = false;
                    filename = "NA";
                    break;
                case 5:
                    available = true;
                    filename = "frappe-3lap.mpk.json";
                    break;
                case 6:
                    available = true;
                    filename = "choco-3lap.mpk.json";
                    break;
                case 7:
                    available = true;
                    filename = "mario-flap.mpk.json";
                    break;

                //Star Cap
                case 8:
                    available = true;
                    filename = "warrio-flap.mpk.json";
                    //filename = "warrio-3lap.mpk.json";
                    break;
                case 9:
                    available = false;
                    filename = "NA";
                    break;
                case 10:
                    available = false;
                    filename = "NA";
                    break;
                case 11:
                    available = false;
                    filename = "NA";
                    break;

                //Special Cup
                case 12:
                    available = false;
                    filename = "NA";
                    break;
                case 13:
                    available = true;
                    filename = "yoshi-flap.mpk.json";
                    break;
                case 14:
                    available = true;
                    filename = "banshee-flap.mpk.json";
                    break;
                case 15:
                    available = false;
                    filename = "NA";
                    break;

                default:
                    available = false;
                    filename = "NA";
                    break;
            }

            //Build javascript payload
            JObject resp = new JObject
            {
                ["available"] = available,
                ["path"] = dirpath + filename
            };

            //return JObject
            return resp;
        }
    }
}
