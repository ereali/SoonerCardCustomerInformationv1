using System;
using RestSharp;
using Newtonsoft.Json;
using RestSharp.Authenticators;
using System.Text;
using ISOParse.API.APIObjects;
using System.Collections.Generic;

namespace ISOParse.API
{
    public class CustVerify
    {
        public CustVerify()
        {
        }

        public List<string> CustVerifyOut(string token, string tokenSecret, string cardNum, bool includeImage)
        {
            var getSecrets = new ConfigBuilder();

            string image;

            var client = new RestClient($"https://{APIGlobalVars.HostName}/bbts/api/20140501/commerce/CustomerVerification");

            client.Timeout = 2000;

            var request = new RestRequest(Method.POST);

            request.AddHeader("Content-Type", "application/json");

            request.AddHeader("TransactSP-Institution-Route", $"InstitutionRouteId {getSecrets.InstitutionRouteId}");

            request.AddHeader("TransactSP-Merchant-Authorization", $"{getSecrets.MerchantAuthorization}");

            var tokenAuth = OAuth1Authenticator.ForAccessToken(getSecrets.OAuthConsumerKey, getSecrets.OAuthSecretKey, token, tokenSecret, RestSharp.Authenticators.OAuth.OAuthSignatureMethod.HmacSha1);

            if (includeImage)
            {
                image = APIGlobalVars.IncludeImageYes;
            }
            else
            {
                image = APIGlobalVars.IncludeImageNo;
            }

            var requestBody = new StringBuilder();
            requestBody.Append("{\n    \"CustomerVerifyRequest\": {\n        \"Customer\": {\n            \"Instrument\": {\n                \"Identification\": {\n                    \"Values\": {\n                        \"CardMagstripeTrack2\": \"");
            requestBody.Append(cardNum);
            requestBody.Append("\"\n                    },\n                    \"Method\": \"CardMagstripeTrack2\"\n                },\n                \"Type\": \"InstitutionCampusCard\"\n            }\n        },\n        \"ResponseEchoLevel\": \"Normal\",\n        \"ResponseOptions\": {\n            \"Image\": \"");
            requestBody.Append(image);
            requestBody.Append("\"\n        },\n        \"Terminal\": {\n            \"Identification\": {\n                \"Method\": \"InstitutionMerchantDefault\"\n            }\n        }\n    }\n}");

            request.AddParameter("application/json", requestBody, ParameterType.RequestBody);

            tokenAuth.Authenticate(client, request);

            IRestResponse response = client.Execute(request);

            var root = JsonConvert.DeserializeObject<CustVerifyRoot>(response.Content);

            //Try Catch exception handler for invalid ID numbers entered

            var custInfoList = new List<string>();

            try
            {
                custInfoList.Add(root.CustomerVerify.Customer.Person.FirstName); //0
                custInfoList.Add(root.CustomerVerify.Customer.Person.LastName); //1
                custInfoList.Add(root.CustomerVerify.Customer.CustomerNumber); //2
                custInfoList.Add(root.CustomerVerify.Customer.PrimaryEmailAddress); //3
                custInfoList.Add(root.CustomerVerify.Operation.ResponseTimestampUtc.ToString()); //4
            }
                
            catch (NullReferenceException)
            {
                Console.WriteLine("\n\rCard or ID number entered is invalid");
                custInfoList.Add("false");
            }

            if (includeImage)
            {
                try
                {
                    custInfoList.Add(root.CustomerVerify.Customer.Image); //5
                }

                catch (NullReferenceException)
                {
                    Console.WriteLine("\n\rError customer does not have image");
                }
            }

            //custInfoList.Add(root.CustomerVerify.Customer.Person.LastName); //1
            //custInfoList.Add(root.CustomerVerify.Customer.CustomerNumber); //2
            //custInfoList.Add(root.CustomerVerify.Customer.PrimaryEmailAddress); //3
            //custInfoList.Add(root.CustomerVerify.Operation.ResponseTimestampUtc.ToString()); //4
            //custInfoList.Add(root.CustomerVerify.Customer.Image); //5

            return custInfoList;

        }

    }
}
