using System;
using RestSharp;
using RestSharp.Authenticators;

namespace ISOParse.API
{
    public class TokenValidation
    {
        public TokenValidation()
        {
        }

        public string TokenValidationInit(string token, string tokenSecret)
        {
            var getSecrets = new ConfigBuilder();

            var client = new RestClient($"https://{APIGlobalVars.HostName}/transact/api/verify");

            client.Timeout = 2000;

            var request = new RestRequest(Method.POST);

            var tokenAuth = OAuth1Authenticator.ForAccessToken(getSecrets.OAuthConsumerKey, getSecrets.OAuthSecretKey, token, tokenSecret, RestSharp.Authenticators.OAuth.OAuthSignatureMethod.HmacSha1);

            request.AddParameter("text/plain", "", ParameterType.RequestBody);

            tokenAuth.Authenticate(client, request);

            IRestResponse response = client.Execute(request);

            //Add a catch to return error if necessary
            return response.Content;
        }
    }
}
