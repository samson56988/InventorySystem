using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagementSofware.Models
{
    public class Subcategory
    {
        public int ID { get; set; }

        public int CategoryID { get; set; }
        public string Categoryname { get; set; }
        
        public string SubcategoryName { get; set; }

        public string Status { get; set; }
    }
}