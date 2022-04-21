using System;
using System.Collections.Generic;
using System.IO;

namespace SalesDataAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            // check for cmd line args
            if(args.Length != 2) {
                // error out if not
                Console.WriteLine("<<<--------------------->>>");
                Console.WriteLine("Usage: ");
                Console.WriteLine("SalesDataAnalyzer <sales_data_file_path> <report_file_path>");
                Console.WriteLine("<sales_data_file_path>\t\tFile to analyze in CSV format. Complete path optional if file is in current directory");
                Console.WriteLine("<report_file_path>\t\tReport File to write out upon completion of the analysis. File will be overwritten each time");
                return;
            }

            string dataFile = args[0];
            string reportFile = args[1];

            if(!File.Exists(dataFile)) {
                Console.WriteLine("Error: File '[ dataFile ]' does not exist!");
                Console.WriteLine("Exiting....");
                return;
            }

            // create list of structs 
            List<salesDataStruct> salesData = new List<salesDataStruct>();

            // load data into list of structs
            salesData = DataLoader.LoadSalesData(dataFile);


            // create report
                // create stringbuilder
                // call all 12 q's
                // output all 12 q's into individual formatted strings
                // add all 12 formatted strings to sb object

                // open/create file to write
                // write sb object to file

            foreach(salesDataStruct data in salesData) {
                Console.WriteLine($"{ data.ToString() }");
            }

        }
    }
}
