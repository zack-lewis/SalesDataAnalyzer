using System;
using System.Collections.Generic;
using System.IO;

namespace SalesDataAnalyzer
{
    class DataLoader
    {
        // pass filename into load method
        // create return list
        // parse each line of csv
        // throws exception and ends program if unable to load csv
        // throws exception and ignores line if type mismatch
        // ignore first line
        // split on comma
        // create temp struct
        // add data to struct
        // add struct to return list
        // repeat
        public static List<salesDataStruct> LoadSalesData(string fileName) {
            List<salesDataStruct> output = new List<salesDataStruct>();
            using(StreamReader inputFile = new StreamReader(fileName)) {
                string line = inputFile.ReadLine();
                int lineNum = 1;
                while((line = inputFile.ReadLine()) != null) {
                    lineNum++;
                    var lineSD = line.Split(",");
                    try {
                        salesDataStruct sd = new salesDataStruct(lineSD[0]);
                        sd.Branch = Enum.Parse<SalesBranch>(lineSD[1]);
                        sd.City = lineSD[2];
                        sd.CustomerType = Enum.Parse<CustType>(lineSD[3]);
                        sd.Gender = Enum.Parse<GenderType>(lineSD[4]);
                        sd.ProductLine = lineSD[5];
                        sd.UnitPrice = Decimal.Parse(lineSD[6]);
                        sd.Quantity = Int32.Parse(lineSD[7]);
                        sd.Date = DateTime.Parse(lineSD[8]);
                        sd.Payment = lineSD[9];
                        sd.Rating = Decimal.Parse(lineSD[10]);


                        output.Add(sd);
                    }
                    catch(ArgumentException ex){ 
                        Console.WriteLine($"Skipping line { lineNum }\n\tError loading { ex.ParamName } record: { ex.Message }");
                    }
                    catch(FormatException ex) {
                        Console.WriteLine($"Skipping line { lineNum }\n\tError loading Rating/Price record: { ex.Message }");
                    }
                    catch(Exception ex){
                        System.Console.WriteLine($"Unknown error on line { lineNum}\n\t{ ex.Message }\n\t{ ex.StackTrace }");
                    }

                }
            }

            return output;
        }

    }
}