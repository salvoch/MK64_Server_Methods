using System.Text;
using Newtonsoft.Json.Linq;
using RestSharp;
using GameData;

namespace GhostDownload
{
    class GhostQuery
    {
        public static string siteUrl = "https://us-central1-mk64-ad77f.cloudfunctions.net/";

        //Build payload to query server to retrieve link for ghost data
        //Example curl:
        //  curl --location --request GET 'https://us-central1-mk64-ad77f.cloudfunctions.net/record?gameId=mk64&categorySlug=flap&subcategorySlug=luigiraceway'
        public JObject get_file_url(int trackId, string recordCategory)
        {

        }
    }
}
