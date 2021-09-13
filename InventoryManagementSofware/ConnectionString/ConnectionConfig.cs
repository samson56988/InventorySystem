using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace InventoryManagementSofware.ConnectionString
{
    public class ConnectionConfig
    {
        public static string GetConnection()
        {
            return ConfigurationManager.ConnectionStrings["InventoryConnection"].ConnectionString;

        }
    }
}