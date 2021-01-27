using System;
using System.IO;
using System.Collections.Generic;
using CsvHelper;
using System.Text;
using System.Globalization;

namespace ISOParse
{
    public class CSVHandler
    {
        string filePath { get; set; }
        List<string> exportList { get; set; }

        //string fileName { get; set; }

        public CSVHandler()
        {
            csvStart();
        }

        private void csvStart()
        {
            var path = Directory.GetCurrentDirectory();

            string rootUserPath;
            string[] pathSplit;
            bool isForwardSlash = false;

            //Catch for Windows vs MacOS file structure
            try
            {
                //pathSplit = path.Split("/");

                //rootUserPath = "/" + pathSplit[1] + "/" + pathSplit[2] + "/";

                pathSplit = path.Split(@"\");

                rootUserPath = @"\" + pathSplit[1] + @"\" + pathSplit[2] + @"\";
            }

            catch (IndexOutOfRangeException)
            {
                pathSplit = path.Split("/");

                rootUserPath = "/" + pathSplit[1] + "/" + pathSplit[2] + "/";

                isForwardSlash = true;
            }

            bool isPath = false;

            while (!isPath)
            {
                Console.WriteLine("\n\rPlease specify file path:");

                Console.Write(rootUserPath);
                string remainingFilePath = Console.ReadLine().ToLower().Trim();

                filePath = rootUserPath + remainingFilePath;

                if (Directory.Exists(filePath))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid file path");
                }
            }

            bool isFileName = false;

            while (!isFileName)
            {
                Console.Write("\n\rInput file name without extensions: ");

                string fileName = Console.ReadLine().Trim();

                //fileName = fileName + "_" + DateTime.Now + ".csv";

                fileName = fileName + "_" + DateTime.Now.ToString("MM-dd-yy") + ".csv";

                if (isForwardSlash)
                {
                    filePath = filePath + "/" + fileName;
                }
                else
                {
                    filePath = filePath + @"\" + fileName;
                }
                
                Console.WriteLine($"Full file path: {filePath}");

                if (File.Exists(filePath))
                {
                    Console.WriteLine("File name already exists");
                }
                else
                {
                    break;
                }

            }

            Console.Write("\n\rInclude stored values in .csv file export (Y/N): ");

            string includeSV = Console.ReadLine().ToLower().Trim();

            bool done = false;

            while (!done)
            {
                if (includeSV == "y")
                {
                    exportSV();
                    break;
                }
                else if (includeSV == "n")
                {
                    exportRegular();
                    break;
                }
                else
                {
                    Console.Write("Incorrect input, please try again (Y/N): ");
                    includeSV = Console.ReadLine().ToLower().Trim();
                }
            }
        }

        //Move card handlers to new file/class in order to reduce clutter

        private void exportSV()
        {
            bool done = false;

            var apiObjList = new List<APIHandler>();

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
                    
                    //var exportObj = new List<exportObjects>();

                    //Set to T/F to include base64 image in console
                    apiHandle.APIHandlerInit(false, cardNum);
                    if (apiHandle.custPrimaryEmail == null)
                    {
                        Console.WriteLine($"{apiHandle.custFirstName} {apiHandle.custLastName}\n\r{apiHandle.custNumber}\n\r{apiHandle.custResponseTime}");

                        //var export = new exportObjects();

                        //export.custFirstName = apiHandle.custFirstName;
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

                    apiObjList.Add(apiHandle);
                }
            }

            saveSV(apiObjList);
        }

        private void exportRegular()
        {
            bool done = false;

            var apiObjList = new List<APIHandler>();

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

                    apiObjList.Add(apiHandle);
                }
            }

            saveRegular(apiObjList);
        }

        //Saves customer info without stored values
        private void saveRegular(List<APIHandler> apiObjects)
        {
            string path = filePath;

            //using (var mem = new MemoryStream())
            using var writer = new StreamWriter(path);
            using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csvWriter.Configuration.Delimiter = ",";

            csvWriter.WriteField("Sooner ID");
            csvWriter.WriteField("First Name");
            csvWriter.WriteField("Last Name");
            csvWriter.WriteField("Email");
            csvWriter.WriteField("Entry time");
            csvWriter.NextRecord();

            foreach (var i in apiObjects)
            {
                csvWriter.WriteField(i.custNumber);
                csvWriter.WriteField(i.custFirstName);
                csvWriter.WriteField(i.custLastName);
                csvWriter.WriteField(i.custPrimaryEmail);
                csvWriter.WriteField(i.custResponseTime);
                csvWriter.NextRecord();
            }

            writer.Flush();

            Console.WriteLine("\n\rFile saved successfully!");
        }

        //Saves .csv with stored values included
        private void saveSV(List<APIHandler> apiObjects)
        {
            string path = filePath;

            //using (var mem = new MemoryStream())
            using var writer = new StreamWriter(path);
            using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csvWriter.Configuration.Delimiter = ",";

            csvWriter.WriteField("Sooner ID");
            csvWriter.WriteField("First Name");
            csvWriter.WriteField("Last Name");
            csvWriter.WriteField("Email");
            csvWriter.WriteField("Entry time");
            csvWriter.WriteField("Stored Value");
            csvWriter.NextRecord();

            foreach (var i in apiObjects)
            {
                csvWriter.WriteField(i.custNumber);
                csvWriter.WriteField(i.custFirstName);
                csvWriter.WriteField(i.custLastName);
                csvWriter.WriteField(i.custPrimaryEmail);
                csvWriter.WriteField(i.custResponseTime);
                foreach (var n in i.svOutputList)
                {
                    csvWriter.WriteField(n);
                }
                csvWriter.NextRecord();
            }

            writer.Flush();

            Console.WriteLine("\n\rFile saved successfully!");
        }
    }
}
