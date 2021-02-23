using System;
namespace ISOParse
{
    public static class TryParse
    {
        public static int TryParseInt()
        {
            bool isNum = false;
            int input = 0;
            while (!isNum)
            {
                isNum = Int32.TryParse(Console.ReadLine().Trim(), out input);
                ConHelper.ClearCurrentConsoleLine();
                if (!isNum)
                {
                    Console.WriteLine("\nInvalid Input, please try again");
                }
            }
            return input;
        }

        public static int MenuSel(int input)
        {
            bool isValid = false;
            while (!isValid)
            {
                //switch (input)
                //{
                //    case 1:
                //        break;
                //    case 2:
                //        break;
                //    case 3:
                //        break;
                //    case 4:
                //        break;
                //    default:
                //        input = TryParseInt();
                //        isValid = false;
                //        break;
                //}

                if (input == 1 || input == 2 || input == 3 || input == 4 || input == 5)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\nInvalid Input, please try again");
                    input = TryParseInt();
                }    
            }

            return input;   
        }
    }
}
