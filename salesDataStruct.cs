using System;
using System.Text;

namespace SalesDataAnalyzer
{
    public struct salesDataStruct
    {
        private string _invoiceID;
        private SalesBranch _branch;
        private string _city;
        private CustType _customerType;
        private GenderType _gender;
        private string _productLine;
        private decimal _unitPrice;
        private int _quantity;
        private DateTime _date;
        private PaymentType _payment;
        private decimal _rating;

        public string InvoiceID {get => _invoiceID; set => _invoiceID = value; }
        public SalesBranch Branch {get => _branch; set => _branch = (SalesBranch)value; }
        public string City { get => _city; set => _city = value; }
        public CustType CustomerType { get => _customerType; set => _customerType = (CustType)value; }
        public GenderType Gender { get => _gender; set => _gender = (GenderType)value; }
        public string ProductLine { get => _productLine; set => _productLine = value; }
        public decimal UnitPrice { get => _unitPrice; set => _unitPrice = value; }
        public int Quantity { get => _quantity; set => _quantity = value; }
        public DateTime Date { get => _date; set => _date = value; }
        public string Payment { get => _payment.ToString().Replace("_"," "); set => _payment = Enum.Parse<PaymentType>(value.Replace(" ","_")); }
        public decimal Rating { get => _rating; set => _rating = value; }
        public decimal InvTotal { get => _unitPrice * _quantity; }

        public salesDataStruct(string InvID) : this() {
            InvoiceID = InvID;
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine($"Invoice: { InvoiceID }");
            output.AppendLine($"\tBranch: { Branch }");
            output.AppendLine($"\tCity: { City }");
            output.AppendLine($"\tCustomer Type: { CustomerType }");
            output.AppendLine($"\tGender: { Gender }");
            output.AppendLine($"\tProduct Line: { ProductLine }");
            output.AppendLine($"\tUnit Price: { UnitPrice }");
            output.AppendLine($"\tQuantity: { Quantity }");
            output.AppendLine($"\tDate: { Date.ToString("d") }");
            output.AppendLine($"\tPayment Method: { Payment }");
            output.AppendLine($"\tRating: { Rating }");
            
            return output.ToString();
        }
    }


}