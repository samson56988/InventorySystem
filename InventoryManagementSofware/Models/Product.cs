using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagementSofware.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Productname { get; set; }

        public int CategoryID { get; set; }
        public string Category { get; set; }

        public int SubcategoryID { get; set; }
        public string Subcategory { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}