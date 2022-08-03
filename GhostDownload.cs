using System.Text;
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

            //print:
            Console.WriteLine("RESPONSE: " + response);
            Console.WriteLine("CONTENT: " + response.Content);

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

        public void parse_record_response(string stringResponse)
        {
            //CONVERT TO JSSON, CLAL THE RecordQuery object here
        }
    }
}
