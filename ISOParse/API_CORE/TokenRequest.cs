using System;
using RestSharp;
using RestSharp.Authenticators;

namespace ISOParse.API
{
    public class TokenRequest
    {
        public string Token { get; set; }
        public string TokenSecret { get; set; }

        public TokenRequest()
        {
        }

        public bool TokenInit(string tempToken, string tempTokenSecret)
        {
            var getSecrets = new ConfigBuilder();

            var client = new RestClient($"https://{APIGlobalVars.HostName}/transact/api/token");

            client.Timeout = 2000;

            var request = new RestRequest(Method.POST);

            var tokenAuth = OAuth1Authenticator.ForAccessToken(getSecrets.OAuthConsumerKey, getSecrets.OAuthSecretKey, tempToken, tempTokenSecret, RestSharp.Authenticators.OAuth.OAuthSignatureMethod.HmacSha1);

            request.AddParameter("text/plain", "", ParameterType.RequestBody);

            tokenAuth.Authenticate(client, request);

            IRestResponse tokenResponse = client.Execute(request);

            //Token = tokenResponse.Content.Split("&")[0].Split("=")[1];
            //TokenSecret = tokenResponse.Content.Split("&")[1].Split("=")[1];

            if (tokenResponse.Content != "")
            {
                //Make exception clause if not responding
                Token = tokenResponse.Content.Split("&")[0].Split("=")[1];
                TokenSecret = tokenResponse.Content.Split("&")[1].Split("=")[1];

                return true;
                //For debugging purposes, remove later
                //Console.WriteLine($"Temp Token: {TempToken}\n\rTemp Secret: {TempTokenSecret}");
            }
            else
            {
                //error handler in-case real token request fails
                return false;
            }

        }
    }
}
