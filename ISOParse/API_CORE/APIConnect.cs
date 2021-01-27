using System;
namespace ISOParse.API
{
    //Json mirror for appsettings.json and secrets file stored locally
    public class APIConnect
    {
        public string OAuthConsumerKey { get; set; }

        public string OAuthSecretKey { get; set; }

        public string InstitutionRouteId { get; set; }

        public string MerchantAuthorization { get; set; }

        public APIConnect()
        {
            
        }
    }
}
