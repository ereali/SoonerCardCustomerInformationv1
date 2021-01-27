using System;
using ISOParse.API;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace ISOParse.API_SEC
{
    public class DoorAccessAPIHandler
    {
        public DoorAccessAPIHandler()
        {
            DoorAccessAPIInit();
        }

        public void DoorAccessAPIInit()
        {
            bool done = false;

            while (!done)
            {
                var cardHandle = new CardHandle();

                Console.Write($"\n\rTap SoonerCard, Phone or Watch on Reader\n\rType done to continue: ");
                string cardNum = cardHandle.CardHandleMain();

                if (cardNum == "done")
                {
                    break;
                }

                if (cardNum != "")
                {
                    DoorAccessProcess(cardNum);
                }
            }
            
        }

        private void DoorAccessProcess(string cardNum)
        {
            var tempToken = new TempTokenRequest();
            var tokenRequest = new TokenRequest();
            var tokenValidation = new TokenValidation();

            //Build in better catch
            if (!tempToken.TempTokenRequestInit())
            {
                Console.WriteLine($"\n\rError connecting to API Service, please try again.");
                Console.ReadKey();
                //return false;
                //Environment.Exit(0);
            }

            if (!tokenRequest.TokenInit(tempToken.TempToken, tempToken.TempTokenSecret))
            {
                Console.WriteLine($"\n\rError connecting to API Service, please try again.");
                Console.ReadKey();
                //return false;
            }

            string tokenVal = tokenValidation.TokenValidationInit(tokenRequest.Token, tokenRequest.TokenSecret);

            if (tokenVal == "Success")
            {
                var custVerify = new CustVerify();

                var custList = custVerify.CustVerifyOut(tokenRequest.Token, tokenRequest.TokenSecret, cardNum, false);

                bool verify = true;

                for (int i = 0; i < custList.Count; i++)
                {
                    if (custList[0] == "false")
                    {
                        verify = false;
                    }
                    else if (custList[i] == "" || custList[i] == ",")
                    {
                        Console.WriteLine("Customer card or ID read error");
                        verify = false;
                    }
                }

                if (verify)
                {
                    Console.WriteLine("\n\r{0} {1} | {2}",custList[0],custList[1],custList[2]);
                    DoorAccessOutput(tokenRequest.Token, tokenRequest.TokenSecret, custList[2]);
                }
            }
        }

        public void DoorAccessOutput(string token, string tokenSecret, string custNum)
        {
            var doorAccess = new DoorAccess();

            doorAccess.DoorAccessPlansGet(token, tokenSecret);

            doorAccess.DoorAccessPlansByCustomer(token, tokenSecret, custNum);

            //Function for outputting list matches for door access
            List<DoorAccessVars> test3 = doorAccess.AllDoorAccessPlans.Where(w => doorAccess.CustDoorAccessPlans.Contains(w.Id)).ToList();

            int n = 0;

            Console.WriteLine();
            foreach (var i in test3)
            {
                Console.WriteLine(i.Name);
                n++;
            }

            Console.WriteLine($"\n\rCustomer has {n} door access plans");

            //foreach(var i in doorAccess.AllDoorAccessPlans)
            //{
            //    if(i.Id == doorAccess.CustDoorAccessPlans)
            //}

            //foreach(var i in doorAccess.CustDoorAccessPlans)
            //{
            //    if(i == doorAccess.AllDoorAccessPlans.)
            //}
        }
        

    }
}
