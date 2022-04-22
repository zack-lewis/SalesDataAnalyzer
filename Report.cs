using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SalesDataAnalyzer
{
    public static class Report
    {
        public static void generateReport(List<salesDataStruct> salesList, string reportFile) {
            string reportHeadingBorder = "******************************************";
                // create stringbuilder
                StringBuilder report = new StringBuilder();
                report.AppendLine(reportHeadingBorder);
                report.AppendLine("Sales Data Report");
                report.AppendLine(reportHeadingBorder);
                report.AppendLine();

                // call all 12 q's
                var q1 = totalSales(salesList);
                var q2 = uniqueProductLines(salesList);
                var q3 = totalSaleByProductLine(salesList);
                var q4 = totalSalesByCity(salesList);
                var q5 = highestSalesProductLines(salesList);
                var q6 = totalSalesByMonth(salesList);
                var q7 = totalSalesByPaymentType(salesList);
                var q8 = numberSalesByMemberType(salesList);
                var q9 = averageRatingByProductLine(salesList);
                var q10 = totalTransactionsByPaymentType(salesList);
                var q11 = totalQuantityByCity(salesList);
                var q12 = fivePercentTaxesPerTransaction(salesList);

                // output all 12 q's into individual formatted strings

                
                // add all 12 formatted strings to sb object
                report.AppendLine(reportHeadingBorder);
                report.AppendLine("Total Sales: ");
                report.AppendLine(reportHeadingBorder);
                report.AppendLine($"Total: { q1.ToString("C2") }");

                report.AppendLine();
                report.AppendLine(reportHeadingBorder);
                report.AppendLine("Unique Productlines");
                report.AppendLine(reportHeadingBorder);
                foreach(var i in q2) {
                    report.AppendLine($"{ i.Key }");
                }

                report.AppendLine();
                report.AppendLine(reportHeadingBorder);
                report.AppendLine("Total Sales per Product Line");
                report.AppendLine(reportHeadingBorder);
                foreach(var i in q3) {
                    report.AppendLine(i);
                }


                report.AppendLine();
                report.AppendLine(reportHeadingBorder);
                report.AppendLine("Total Sales per City");
                report.AppendLine(reportHeadingBorder);
                foreach(var i in q4){
                    report.AppendLine(i);
                }

                report.AppendLine();
                report.AppendLine(reportHeadingBorder);
                report.AppendLine("Product lines with Highest Unit Price");
                report.AppendLine(reportHeadingBorder);
                foreach(var i in q5){
                    report.AppendLine($"{ i.ProductLine }: { i.UnitPrice.ToString("C2") }");
                }

                report.AppendLine();
                report.AppendLine(reportHeadingBorder);
                report.AppendLine("Total Sales per Month");
                report.AppendLine(reportHeadingBorder);
                foreach(var i in q6){
                    report.AppendLine(i);
                }
                

                report.AppendLine();
                report.AppendLine(reportHeadingBorder);
                report.AppendLine("Total Sales by Payment Type");
                report.AppendLine(reportHeadingBorder);
                foreach(var i in q7){
                    report.AppendLine(i);
                }

                report.AppendLine();
                report.AppendLine(reportHeadingBorder);
                report.AppendLine("Total Transactions by Member Type");
                report.AppendLine(reportHeadingBorder);
                foreach(var i in q8){
                    report.AppendLine(i);
                }

                report.AppendLine();
                report.AppendLine(reportHeadingBorder);
                report.AppendLine("Average Rating per Product Line");
                report.AppendLine(reportHeadingBorder);
                foreach(var i in q9){
                    report.AppendLine(i);
                }

                report.AppendLine();
                report.AppendLine(reportHeadingBorder);
                report.AppendLine("Total Transactions by Payment Type");
                report.AppendLine(reportHeadingBorder);
                foreach(var i in q10){
                    report.AppendLine(i);
                }

                report.AppendLine();
                report.AppendLine(reportHeadingBorder);
                report.AppendLine("Number of Products Sold per City");
                report.AppendLine(reportHeadingBorder);
                foreach(var i in q11){
                    report.AppendLine(i);
                }

                report.AppendLine();
                report.AppendLine(reportHeadingBorder);
                report.AppendLine("Tax per Sale per Product Line");
                report.AppendLine(reportHeadingBorder);
                foreach(var i in q12){
                    report.AppendLine(i);
                }

                // open/create file to write
                using(StreamWriter sw = new StreamWriter(reportFile)) {
                    // write sb object to file
                    sw.Write(report);
                }
                
                // Console.WriteLine(report);
        }

        //// REPORT QUESTIONS //////

        // Q1 Calculate the total sales in the data set.
        private static decimal totalSales(List<salesDataStruct> salesList) {
            decimal total = 0;
            var data = (from item in salesList select item.InvTotal );
            foreach(var d in data){
                total += d;
            }
            return total;
        }

        // Q2 Show the unique product lines in the data set.
        private static System.Collections.Generic.IEnumerable<System.Linq.IGrouping<string, SalesDataAnalyzer.salesDataStruct>> uniqueProductLines(List<salesDataStruct> salesList) {
            var output = from item in salesList group item by item.ProductLine into uniquePL select uniquePL;
            return output;
        }

        // Q3 Calculate the total sales for each product line. Sales total will be the sum of (Quantity * UnitPrice) for all products sold in the product line. List the product line and total sales.
        private static List<string> totalSaleByProductLine(List<salesDataStruct> salesList)
        {
            List<string> output = new List<string>();
            var query = (from item in salesList group item by item.ProductLine into uniquePL select $"{ uniquePL.Key}: { uniquePL.Sum(item => item.InvTotal).ToString("C2") }");
            foreach(var q in query) {
                output.Add(q.ToString());
            }
            return output;
        }

        // Q4 Calculate the total Sales per city? List the city name and total sales.
        private static List<string> totalSalesByCity(List<salesDataStruct> salesList)
        {
            var output = (from item in salesList group item by item.City into uniqueCity select $"{ uniqueCity.Key }: { uniqueCity.Sum(item => item.InvTotal).ToString("C2") }").ToList<string>();
            return output;
        }

        // Q5 Which product line(s) have the sale with the highest unit price? List the product line and the price.
        private static IEnumerable<salesDataStruct> highestSalesProductLines(List<salesDataStruct> salesList)
        {
            var output = from item in salesList where item.UnitPrice == ((from items in salesList orderby items.UnitPrice descending select items.UnitPrice).FirstOrDefault()) select item;
            return output;
        }

        // Q6 Calculate the total sales per month in the data set. List the city month and total sales.
        private static List<string> totalSalesByMonth(List<salesDataStruct> salesList)
        {
            var output = (from item in salesList group item by item.Date.Month into monthlySales orderby monthlySales.Key ascending select $"{ monthlySales.Key }: { monthlySales.Sum(item => item.InvTotal).ToString("C2") }").ToList<string>();
            return output;
        }

        // Q7 Calculate the total sales per payment type. List the payment type and total sales.
        private static List<string> totalSalesByPaymentType(List<salesDataStruct> salesList)
        {
            var output = (from item in salesList group item by item.Payment into paymentSales select $"{ paymentSales.Key }: { paymentSales.Sum(item => item.InvTotal).ToString("C2") }").ToList<string>();
            return output;
        }

        // Q8 Calculate the number of sales transactions per member type. List the member type and number of transactions.
        private static List<string> numberSalesByMemberType(List<salesDataStruct> salesList)
        {
            var output = (from item in salesList group item by item.CustomerType into memberSales select  $"{ memberSales.Key }: { memberSales.Count() }").ToList<string>();
            return output;
        }

        // Q9 Calculate the average rating per product line. List the product line and the average rating.
        private static List<string> averageRatingByProductLine(List<salesDataStruct> salesList)
        {
            var output = (from item in salesList group item by item.ProductLine into ProductLineRating select $"{ ProductLineRating.Key }: { ProductLineRating.Average(item => item.Rating).ToString("F2") }").ToList<string>();
            return output;
        }

        // Q10 Calculate the total number of transactions per payment type. List the payment type and number of transactions.
        private static List<string> totalTransactionsByPaymentType(List<salesDataStruct> salesList)
        {
            var output = (from item in salesList group item by item.Payment into paymentList select $"{ paymentList.Key }: { paymentList.Count() }").ToList<string>();
            return output;
        }

        // Q11 Calculate the total quantity of products sold per city. List the city and number of products sold.
        private static List<string> totalQuantityByCity(List<salesDataStruct> salesList)
        {
            var output = (from item in salesList group item by item.City into cityList select $"{ cityList.Key }: { cityList.Sum(item => item.Quantity) }").ToList<string>();
            return output;
        }

        // Q12 Using a 5% sales tax, Calculate the tax for each sales transaction in each product line. List the invoice number, total sales for the transaction, and the tax amount for the transaction, ordered by the product line.
        private static List<string> fivePercentTaxesPerTransaction(List<salesDataStruct> salesList)
        {
            decimal taxRate = 0.05M;
            List<string> outputList = new List<string>();
            var pList = from item in salesList group item by item.ProductLine into plList select plList.Key;
            foreach(var p in pList) {
                outputList.Add($"**********{ p }**********");
                var output = (from item in salesList where item.ProductLine == p select $"Invoice: { item.InvoiceID } - Total : { (item.InvTotal * (taxRate+1)).ToString("C2") } \t- Tax { (item.InvTotal * taxRate).ToString("C2") }");    
                outputList.AddRange(output);
                outputList.Add("\n");
            }
            
            return outputList;
        }

        
    }
}