using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith_MAUI.Common;

namespace Zenith_MAUI
{
    public static class Api
    {
        private static RestClient _client;

        private static bool authorizationSet = false;
        
        public static string BaseUrl => "http://localhost:5001/api/";

        public static RestClient Client
        {
            get
            {
                if(_client == null)
                {
                    _client = new RestClient(BaseUrl);
                }

                var user = SecureStorage.Default.GetUser();

                if (user != null && !authorizationSet)
                {
                    authorizationSet = true;
                    _client.AddDefaultHeader("Authorization", "Bearer " + user.Token);
                }

                return _client;
            }
        }
    }
}
