using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagementSofware.Models
{
    public class Purchaseproducts
    {
        public int ID { get; set; }

        public string TransactionNo { get; set; }

        public decimal Purchaseprice { get; set; }

        public int ProductID { get; set; }

        public string Productname { get; set; }

        public int Quantity { get; set; }

        public DateTime Date { get; set; }

        public decimal Discount { get; set; }

        public decimal Total { get; set; }

        public int Vendor { get; set; }

        public string Vendorname { get; set; }

        public string Status { get; set; }

        public string PurchasedBy { get; set; }
    }
}