using System;
using ISOParse.API_SEC;

namespace ISOParse
{
    public class MenuHandler
    {
        public MenuHandler()
        {
            MenuStart();
        }

        private void MenuStart()
        {
            ConHelper.DisplayMenu();
            int menuSel = TryParse.TryParseInt();
            int menuSelN = TryParse.MenuSel(menuSel);

            switch (menuSelN)
            {
                case 1:
                    DisplayInConsole();
                    break;
                case 2:
                    DisplayInConsoleSV();
                    break;
                case 3:
                    //calls csv class for exporting
                    var csvHandler = new CSVHandler();
                    break;
                case 4:
                    var doorAccess = new DoorAccessAPIHandler();
                    break;
            }

            //if (menuSelN == 1)

            //if (menuSelN == 1)
            //{
            //    //menu1();
            //}
            //else
            //{
            //    Console.WriteLine("Incorrect Option Selected");
            //}

        }

        private void DisplayInConsole()
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
                    var apiHandle = new APIHandler();

                    //Set to T/F to include base64 image in console
                    apiHandle.APIHandlerInit(false, cardNum);
                    if (apiHandle.custPrimaryEmail == null)
                    {
                        Console.WriteLine($"{apiHandle.custFirstName} {apiHandle.custLastName}\n\r{apiHandle.custNumber}\n\r{apiHandle.custResponseTime}");
                    }
                    else
                    {
                        Console.WriteLine($"{apiHandle.custFirstName} {apiHandle.custLastName}\n\r{apiHandle.custNumber} | {apiHandle.custPrimaryEmail}\n\r{apiHandle.custResponseTime}");
                    }
                }
            }
        }

        private void DisplayInConsoleSV()
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
                    var apiHandle = new APIHandler();

                    //Set to T/F to include base64 image in console
                    apiHandle.APIHandlerInit(false, cardNum);
                    if (apiHandle.custPrimaryEmail == null)
                    {
                        Console.WriteLine($"{apiHandle.custFirstName} {apiHandle.custLastName}\n\r{apiHandle.custNumber}\n\r{apiHandle.custResponseTime}");
                    }
                    else
                    {
                        Console.WriteLine($"{apiHandle.custFirstName} {apiHandle.custLastName}\n\r{apiHandle.custNumber} | {apiHandle.custPrimaryEmail}\n\r{apiHandle.custResponseTime}");
                    }
                    Console.WriteLine();
                    foreach (var i in apiHandle.svOutputList)
                    {
                        Console.WriteLine(i);
                    }
                }
            }
        }

    }
}
