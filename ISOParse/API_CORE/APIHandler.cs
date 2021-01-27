using System;
using ISOParse.API;
using System.Collections.Generic;
//using Microsoft.Extensions.DependencyInjection;

namespace ISOParse
{
    public class APIHandler
    {
        //need to output everything as a list to save each value into the csv as defined in the menu handler
        private List<string> custList = new List<string>();
        public List<string> svOutputList = new List<string>();
        public string custFirstName { get; set; }
        public string custLastName { get; set; }
        public string custNumber { get; set; }
        public string custPrimaryEmail { get; set; }
        public string custResponseTime { get; set; }
        public string custImage { get; set; }

        public APIHandler()
        {
        }

        //Add bool return to catch
        public bool APIHandlerInit(bool includeImage, string custIDInput)
        {
            var tempToken = new TempTokenRequest();
            var tokenRequest = new TokenRequest();
            var tokenValidation = new TokenValidation();
            //var custVerify = new CustVerify();

            //Build in better catch
            if (!tempToken.TempTokenRequestInit())
            {
                Console.WriteLine($"\n\rError connecting to API Service, please try again.");
                Console.ReadKey();
                return false;
                //Environment.Exit(0);
            }

            if(!tokenRequest.TokenInit(tempToken.TempToken, tempToken.TempTokenSecret))
            {
                Console.WriteLine($"\n\rError connecting to API Service, please try again.");
                Console.ReadKey();
                return false;
            }

            string tokenVal = tokenValidation.TokenValidationInit(tokenRequest.Token, tokenRequest.TokenSecret);

            if (tokenVal == "Success")
            {
                bool custValid = CustAPIHandler(tokenRequest.Token, tokenRequest.TokenSecret, custIDInput, includeImage);

                if(custValid)
                {
                    SVAPIHandler(tokenRequest.Token, tokenRequest.TokenSecret, custNumber);

                    ///////////////////////////////////

                    //doorAccess.DoorAccessPlansByCustomer(tokenRequest.Token, tokenRequest.TokenSecret, custNumber);

                    //////////////////////////////////
                }
                else
                {
                    //returns false
                    return custValid;
                }


                //custList = custVerify.CustVerifyOut();
                //Console.WriteLine();
                //doorAccess.DoorAccessGet(tokenRequest.Token, tokenRequest.TokenSecret, custList[2]);
                //Console.WriteLine();
                //storedValue.SVGet(tokenRequest.Token, tokenRequest.TokenSecret, custList[2]);
            }
            else
            {
                Console.WriteLine("\n\rError connecting to API Service, please try again.");
                return false;
            }

            return false;

        }

        public bool CustAPIHandler(string token, string tokenSecret, string custIDInput, bool includeImage)
        {
            bool verify = true;
            var custVerify = new CustVerify();
            custList = custVerify.CustVerifyOut(token, tokenSecret, custIDInput, includeImage);

            for (int i = 0; i < custList.Count; i++)
            {
                if (custList[0] == "false")
                {
                    verify = false;
                }
                else if(custList[i] == "" || custList[i] == ",")
                {
                    Console.WriteLine("Customer card or ID read error");
                    verify = false;
                }
            }

            if (verify)
            {
                custFirstName = custList[0];
                custLastName = custList[1];
                custNumber = custList[2];
                custPrimaryEmail = custList[3];
                custResponseTime = custList[4];

                //Returns customer information
                //Console.WriteLine($"\n\r{custFirstName} {custLastName}\n\r{custNumber} | {custPrimaryEmail}\n\r{custResponseTime}");
            }

            if(verify && includeImage)
            {
                custImage = custList[5];
                Console.WriteLine($"\n\r{custList[5]}");
            }

            return verify;

        }

        private bool SVAPIHandler(string token, string tokenSecret, string custIDInput)
        {
            var storedValue = new StoredValue();
            var svList = storedValue.SVGet(token, tokenSecret, custIDInput);

            if (svList[0].typeName == "null")
            {
                return false;
            }
            else
            {
                //Outputs stored values
                Console.WriteLine();
                foreach(var i in svList)
                {
                    //Console.WriteLine($"{i.name} Balance: ${i.balance}");
                    svOutputList.Add($"{i.name} Balance: ${i.balance}");
                }
                return true;
            }

            //return false;
        }
    }
}
