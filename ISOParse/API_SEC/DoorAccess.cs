using System;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using System.Collections.Generic;
using ISOParse.API;

namespace ISOParse.API_SEC
{
    public class DoorAccess
    {
        public List<int> CustDoorAccessPlans { get; set; }
        public List<DoorAccessVars> AllDoorAccessPlans { get; set; }

        public DoorAccess()
        {
        }

        //Maker seperate classes for cust door access and all door access plans to associate id with plan
        public void DoorAccessPlansGet(string token, string tokenSecret)
        {
            var getSecrets = new ConfigBuilder();

            var client = new RestClient($"https://{APIGlobalVars.HostName}/bbts/api/management/v1/dooraccessplans");

            //var client = new RestClient($"https://{APIGlobalVars.HostName}/bbts/api/management/v1/customers/{custNum}/dooraccessplans");

            var request = new RestRequest(Method.GET);

            client.Timeout = 2000;

            request.AddHeader("Content-Type", "application/json");

            request.AddHeader("TransactSP-Institution-Route", $"InstitutionRouteId {getSecrets.InstitutionRouteId}");

            //request.AddHeader("TransactSP-Merchant-Authorization", $"{getSecrets.MerchantAuthorization}");

            var tokenAuth = OAuth1Authenticator.ForAccessToken(getSecrets.OAuthConsumerKey, getSecrets.OAuthSecretKey, token, tokenSecret, RestSharp.Authenticators.OAuth.OAuthSignatureMethod.HmacSha1);

            tokenAuth.Authenticate(client, request);

            IRestResponse response = client.Execute(request);

            var doorAccessVarsRoot = JsonConvert.DeserializeObject<DoorAccessVarsRoot>(response.Content);

            //var test = new List<DoorAccessVars>();

            AllDoorAccessPlans = new List<DoorAccessVars>();

            foreach (var i in doorAccessVarsRoot.DoorAccessPlans)
            {
                AllDoorAccessPlans.Add(i);
            }

            //Console.WriteLine(response.Content);
        }

        public void DoorAccessPlansByCustomer(string token, string tokenSecret, string custNum)
        {
            var getSecrets = new ConfigBuilder();

            var client = new RestClient($"https://{APIGlobalVars.HostName}/bbts/api/management/v1/customers/{custNum}/dooraccessplans");

            var request = new RestRequest(Method.GET);

            client.Timeout = 2000;

            request.AddHeader("Content-Type", "application/json");

            request.AddHeader("TransactSP-Institution-Route", $"InstitutionRouteId {getSecrets.InstitutionRouteId}");

            var tokenAuth = OAuth1Authenticator.ForAccessToken(getSecrets.OAuthConsumerKey, getSecrets.OAuthSecretKey, token, tokenSecret, RestSharp.Authenticators.OAuth.OAuthSignatureMethod.HmacSha1);

            tokenAuth.Authenticate(client, request);

            IRestResponse response = client.Execute(request);

            //var doorAccessRoot = JsonConvert.DeserializeObject<DoorAccessRoot>(response.Content);

            var doorAccessRoot = JsonConvert.DeserializeObject<DoorAccessCustVars>(response.Content);

            CustDoorAccessPlans = new List<int>();

            foreach (var i in doorAccessRoot.DoorAccessPlans)
            {
                CustDoorAccessPlans.Add(i);
            }
        }
    }
}
