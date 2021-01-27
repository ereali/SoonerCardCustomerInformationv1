using System;

namespace ISOParse
{
    public class InputParse
    {
        private string cardNum { get; set; }

        public InputParse()
        {
            
        }

        public string InputParseReturn(string input)
        {
            InitInputParse(input);
            return cardNum;
        }

        private void InitInputParse(string input)
        {
            bool complete = false;

            while (!complete)
            {
                if (input.StartsWith(CardRefs.idNumB))
                {
                    idParse(input);
                    break;
                }
                else if (input.StartsWith(CardRefs.magNumB))
                {
                    magParse(input);
                    break;
                }
                else if (input.StartsWith(CardRefs.isoNumB))
                {
                    isoParse(input);
                    break;
                }
                //Need to add type to break to invalid input
                else
                {
                    Console.WriteLine("\n\rNot a  valid Sooner Card, please try again...");
                    input = Console.ReadLine().Trim().ToLower();
                }
            }
        }

        private void idParse(string input)
        {
            if (input.Length > 9)
            {
                input = input.Left(9);
            }
        }


        private void magParse(string input)
        {
            string soonerId = "";
            soonerId = input.Remove(input.Length - 1).Substring(5);
            if (soonerId.Length > 9)
            {
                soonerId = soonerId.Left(9);
            }
        }

        private void isoParse(string input)
        {
            string isoNum = "";
            if (isoNum.Length > 16)
            {
                isoNum = isoNum.Left(16);
            }
        }

    }
}
