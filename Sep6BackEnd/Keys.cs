// TODO - Check comment in Program.cs

namespace Sep6BackEnd
{
    public class Keys
    {
        public readonly string APIKEY;
        public readonly string DBSKEY;

        public Keys(string apiKey, string dbConnection)
        {
            APIKEY = apiKey;
            DBSKEY = dbConnection;
        }
    }
}