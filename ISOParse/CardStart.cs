using System;
using ISOParse.API;

namespace ISOParse
{
    public class CardStart
    {
        //Make a menu handler instead for different options
        public CardStart()
        {
            InitStart();
        }

        private void InitStart()
        {
            ConHelper.DisplayMenu();
            int menuSel = TryParse.TryParseInt();
            int menuSelN = TryParse.MenuSel(menuSel);

            if (menuSelN == 1)
            {
                menu1();
            }
            else
            {
                Console.WriteLine("Incorrect Option Selected");
            }

        }

        private void menu1()
        {
            bool done = false;

            while (!done)
            {
                var cardHandle = new CardHandle();

                Console.WriteLine($"\n\rTap SoonerCard, Phone or Watch on Reader\n\rType done to continue");
                string cardNum = cardHandle.CardHandleMain();

                if (cardNum == "done")
                {
                    break;
                }

                if (cardNum != "")
                {
                    var apiHandle = new APIHandler();

                    //Set to T/F to include base64 image in console
                    apiHandle.APIHandlerInit(false, cardNum);

                    //Console.WriteLine($"\n\r{apiHandle.custFirstName} {apiHandle.custLastName}\n\r{apiHandle.custNumber} | {apiHandle.custPrimaryEmail}");
                    //Console.WriteLine(apiHandle.custResponseTime);
                }
            }
        }
    }
}
