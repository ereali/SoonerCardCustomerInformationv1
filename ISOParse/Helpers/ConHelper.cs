using System;

namespace ISOParse
{
    public class ConHelper
    {
        public ConHelper()
        {

        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        public static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("**************************************************************");
            Console.WriteLine("      Sooner Card Customer Info Tool                          ");
            Console.WriteLine("**************************************************************");
            Console.WriteLine("      Display customer info in console                     : 1");
            Console.WriteLine("      Display customer info in console with stored values  : 2");
            Console.WriteLine("      Save customer info to .csv file                      : 3");
            Console.WriteLine("      Display Customer Door Access Plans                   : 4");
            Console.WriteLine("      Exit Application                                     : 5");
            Console.Write("Select an option: ");
        }
    }
}