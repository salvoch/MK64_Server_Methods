using System.Net;
using Newtonsoft.Json.Linq;
using RestSharp;
using GameData;

namespace GhostDownload
{
    //Blueprint for RecordQuery
    class RecordQuery
    {
        public string categorySlug;
        public string created;
        public string fileName;
        public string link;
        public string note;
        public string score;
        public string subcategorySlug;
        public string userId;
        public string entryId;
        public string fileDownloadUrl;

        public RecordQuery(string categorySlug, string created, string fileName, string link, string note, string score, string subcategorySlug, string userId, string entryId, string fileDownloadUrl)
        {
            this.categorySlug = categorySlug;
            this.created = created;
            this.fileName = fileName;
            this.link = link;
            this.note = note;
            this.score = score;
            this.subcategorySlug = subcategorySlug;
            this.userId = userId;
            this.entryId = entryId;
            this.fileDownloadUrl = fileDownloadUrl;
        }
    }
    class GhostQuery
    {
        public static string siteUrl = "https://us-central1-mk64-ad77f.cloudfunctions.net/";
        public RecordQuery recordQuery;
        public bool available = false;
        public string path;

        //Build payload to query server to retrieve link for ghost data
        //Example curl:
        //  curl --location --request GET 'https://us-central1-mk64-ad77f.cloudfunctions.net/record?gameId=mk64&categorySlug=flap&subcategorySlug=luigiraceway'
        public void get_record(int trackId, string recordCategory)
        {
            //Use RestSharp to make a Post request to gus' website
            var client = new RestClient(siteUrl + "record");
            var request = new RestRequest("", Method.Get);

            //Add header and parameters to request object
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("gameId", Constants.GAME_ID);
            request.AddParameter("categorySlug", recordCategory);
            request.AddParameter("subcategorySlug", Constants.TRACK_SLUGS[trackId]);

            //Send it!
            RestResponse response = client.Execute(request);

            //print: TODO - DELETE
            Console.WriteLine("Server response content: " + response.Content);

            //If response is not ok, log it to console
            if (response.StatusCode.ToString() != "OK")
            {
                Console.WriteLine(response.Content);
                Console.WriteLine("Time not uploaded! Please make sure username is typed correctly in the config.txt as it is in the website.");
                Console.WriteLine("Contact site administrator if you have not set up a username.");
                Console.WriteLine("Please fix issue and restart application to continue...");
            }
            else
            {
                parse_record_response(response.Content);
            }
        }
        public string download_ghost_json()
        {
            if (available)
            {
                //TODO - try-except, etf
                //get request the file!
                var client = new RestClient(recordQuery.fileDownloadUrl);
                var request = new RestRequest("", Method.Get);
                RestResponse response = client.Execute(request);
                Console.WriteLine(response);
                Console.WriteLine(response.Content);
                return response.Content;
            } else
            {
                //raise error
                Console.WriteLine("Ghost not available, cannot download"); //TODO exception/catch??
                return "NA";
            }
        }

        //Write ghost to a file for emulator to read
        public static void write_file(string file_path, string jsonString)
        {
            //TODO - get path of current execution, append the \\ghostloader\\ghost.json to it
            File.WriteAllText(file_path, jsonString);
        }

        public void parse_record_response(string stringResponse)
        {
            //TODO - TRY? Raise/catch/etc/ Also, handle no response so it doesn't hang
            JObject jsonResponse = JObject.Parse(stringResponse);

            if (jsonResponse.ContainsKey("fileDownloadUrl")) {
                //Build record as normal
                recordQuery = new RecordQuery(
                jsonResponse.GetValue("categorySlug").ToString(),
                jsonResponse.GetValue("created").ToString(),
                jsonResponse.GetValue("fileName").ToString(),
                jsonResponse.GetValue("link").ToString(),
                jsonResponse.GetValue("note").ToString(),
                jsonResponse.GetValue("score").ToString(),
                jsonResponse.GetValue("subcategorySlug").ToString(),
                jsonResponse.GetValue("userId").ToString(),
                jsonResponse.GetValue("entryId").ToString(),
                jsonResponse.GetValue("fileDownloadUrl").ToString());

                available = true;
            } else {

                //Build record without filename and filedownloadurl 
                recordQuery = new RecordQuery(
                jsonResponse.GetValue("categorySlug").ToString(),
                jsonResponse.GetValue("created").ToString(),
                "NA",
                jsonResponse.GetValue("link").ToString(),
                jsonResponse.GetValue("note").ToString(),
                jsonResponse.GetValue("score").ToString(),
                jsonResponse.GetValue("subcategorySlug").ToString(),
                jsonResponse.GetValue("userId").ToString(),
                jsonResponse.GetValue("entryId").ToString(),
                "NA");

                available = false;
            }
        }
    }
}
