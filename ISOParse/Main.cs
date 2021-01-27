using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ISOParse.API;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace ISOParse
{
    class Program
    {
        static void Main(string[] args)
        {
            //var cardStart = new CardStart();

            var menuHandler = new MenuHandler();

            Console.WriteLine("\n\rThank you for using the Sooner Card Customer Info Application");

            Console.ReadKey();
        }
    }
}