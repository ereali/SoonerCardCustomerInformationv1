using System;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ISOParse.API
{
    public class SVAccount
    {
        public int id { get; set; }
        public int accountNumber { get; set; }
        public string storedValueAccountShareType { get; set; }
        public string currencyCodeIsoAlphabetic { get; set; }
        public double balance { get; set; }
        public string typeName { get; set; }
        public int storedValueAccountTypeId { get; set; }
        public string storedValueAccountAssociationType { get; set; }
        public string name { get; set; }
        public double balanceForward { get; set; }
        public double creditLimit { get; set; }
    }

    public class SVRoot
    {
        public List<SVAccount> storedValueAccounts { get; set; }
    }

    public class StoredValue
    {
        public StoredValue()
        {
        }

        //Add class handler for json deserialization response to output proper values
        public List<SVAccount> SVGet(string token, string tokenSecret, string custNum)
        {
            var getSecrets = new ConfigBuilder();

            var clientSV = new RestClient($"https://{APIGlobalVars.HostName}/bbts/api/management/v1/customers/{custNum}/storedvalueaccounts");

            var requestSV = new RestRequest(Method.GET);

            clientSV.Timeout = 2000;

            requestSV.AddHeader("Content-Type", "application/json");

            requestSV.AddHeader("TransactSP-Institution-Route", $"InstitutionRouteId {getSecrets.InstitutionRouteId}");

            var tokenAuth = OAuth1Authenticator.ForAccessToken(getSecrets.OAuthConsumerKey, getSecrets.OAuthSecretKey, token, tokenSecret, RestSharp.Authenticators.OAuth.OAuthSignatureMethod.HmacSha1);

            tokenAuth.Authenticate(clientSV, requestSV);

            IRestResponse responseSV = clientSV.Execute(requestSV);

            //var svRoot = new List<SVAccount>();
            var svRoot = JsonConvert.DeserializeObject<SVRoot>(responseSV.Content);

            if (svRoot == null)
            {
                var svRootNull = new List<SVAccount>();
                var svAccountNull = new SVAccount();
                svAccountNull.typeName = "null";

                return svRootNull;
            }
            else
            {
                return svRoot.storedValueAccounts;
            }


            //try
            //{
            //    foreach (var i in test)
            //    {
                    
            //    }
            //    //foreach (var i in svRoot.storedValueAccounts)
            //    //{
            //    //    Console.WriteLine($"{i.name} Balance: ${i.balance}");
            //    //}
            ////}

            //catch(NullReferenceException e)
            //{
            //    Console.WriteLine("Null exception");
            //    //return svRoot.storedValueAccounts;
            //}
            
        }
    }
}
