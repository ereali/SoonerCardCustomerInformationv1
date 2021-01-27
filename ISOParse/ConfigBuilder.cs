using System;
using ISOParse.API;
using Microsoft.Extensions.Configuration;

namespace ISOParse
{
    //Handles Secret injection into runtime of application
    public class ConfigBuilder
    {
        public string OAuthConsumerKey { get; set; }
        public string OAuthSecretKey { get; set; }
        public string InstitutionRouteId { get; set; }
        public string MerchantAuthorization { get; set; }

        public ConfigBuilder()
        {
            builder();
        }

        private void builder()
        {
            //Builds config as template of appsettings.json which mirrors to internal secrets file
            var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            //.AddUserSecrets<APIConnect>()
            .Build();

            //Converts to vars
            var section = config.GetSection(nameof(APIConnect));
            var apiConnect = section.Get<APIConnect>();

            //Sets pulled vars to public vars for use within runtime of app
            OAuthConsumerKey = apiConnect.OAuthConsumerKey;
            OAuthSecretKey = apiConnect.OAuthSecretKey;
            InstitutionRouteId = apiConnect.InstitutionRouteId;
            MerchantAuthorization = apiConnect.MerchantAuthorization;
        }
    }
}
