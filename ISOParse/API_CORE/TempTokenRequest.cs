using System;
using RestSharp;
using RestSharp.Authenticators;
using Microsoft.Extensions.Options;

namespace ISOParse.API
{
    public class TempTokenRequest
    {
        public string TempToken { get; set; }
        public string TempTokenSecret { get; set; }

        public TempTokenRequest()
        {

        }

        public bool TempTokenRequestInit()
        {
            var getSecrets = new ConfigBuilder();
            var getAPIVars = new APIGlobalVars();

            //RestClient setup using Temp Token specific URI
            var client = new RestClient($"https://{APIGlobalVars.HostName}/transact/api/initiate");

            client.Timeout = -1;

            //Setups REST API request
            var request = new RestRequest(Method.POST);

            //Passes in Consumer Key and Secret Key and then magically generates the rest of the auth blueprint
            var tempSignature = OAuth1Authenticator.ForRequestToken(getSecrets.OAuthConsumerKey, getSecrets.OAuthSecretKey, RestSharp.Authenticators.OAuth.OAuthSignatureMethod.HmacSha1);

            request.AddParameter("text/plain", "", ParameterType.RequestBody);

            //Does the authentication passthrough
            tempSignature.Authenticate(client, request);

            //Executes the API Request
            IRestResponse tempResponse = client.Execute(request);

            if (tempResponse.Content != "")
            {
                //Make exception clause if not responding
                TempToken = tempResponse.Content.Split("&")[0].Split("=")[1];
                TempTokenSecret = tempResponse.Content.Split("&")[1].Split("=")[1];

                return true;
                //For debugging purposes, remove later
                //Console.WriteLine($"Temp Token: {TempToken}\n\rTemp Secret: {TempTokenSecret}");
            }
            else
            {
                return false;
            }
        }
    }
}
