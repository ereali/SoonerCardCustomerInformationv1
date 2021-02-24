using System;
namespace ISOParse
{
    public class CardHandle
    {
        public CardHandle()
        {
            //CardHandleMain();
        }

        public string CardHandleMain()
        {
            //System.Threading.Thread.Sleep(2000);
            var GetInputParse = new InputParse();
            string input = Console.ReadLine().Trim().ToLower();
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ConHelper.ClearCurrentConsoleLine();

            if (input == "done")
            { 
                return input;
            }
            else
            {
                return input;

                //Meant for card input validation

                //string card = CardHandleOutput(input);

                //if (card == "invalid")
                //{
                //    Console.WriteLine("\n\rError card invalid, please try again");
                //    return "";
                //}
                //else
                //{
                //    return card;
                //}
            }
        }


        //private string CardHandleOutput(string card)
        //{
        //    if (card.StartsWith(CardRefs.magNumB) && card.Length == 14)
        //    {
        //        return card;
        //    }
        //    else if (card.StartsWith(CardRefs.isoNumB) && card.Length == 16)
        //    {
        //        return card;
        //    }
        //    else if (card.StartsWith(CardRefs.idNumB) && card.Length == 9)
        //    {
        //        string cardMod = card.Insert(0, "97310");
        //        return cardMod;
        //    }
        //    else if (card.StartsWith("1") && card.Length == 9)
        //    {
        //        return card;
        //    }
        //    else
        //    {
        //        return "invalid";
        //    }
        //}
    }
}
