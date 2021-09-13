using InventoryManagementSofware.BuisnessInterface.VendorService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventoryManagementSofware.Models;
using System.Data.SqlClient;
using System.Data;

namespace InventoryManagementSofware.BuisnessLogic.VendorBL
{
    public class VendorRepository : IVendorRepository
    {
        public void Delete(Vendor vendor)
        {
            throw new NotImplementedException();
        }

        public IList<Vendor> GetVendor()
        {
            List<Vendor> vendor = new List<Vendor>();
            using (SqlConnection con = new SqlConnection(ConnectionString.ConnectionConfig.GetConnection()))
            {
                SqlCommand cmd = new SqlCommand("getVendor", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var vendors = new Vendor()
                    {
                        VendorID = Convert.ToInt32(rdr["VendorID"].ToString()),
                        Vendorname = rdr["Vendor"].ToString()

                    };
                    vendor.Add(vendors);
                }
                return (vendor);
            }
        }

        public Vendor GetVendorId(int? id)
        {
            throw new NotImplementedException();
        }

        public void InsertNew(Vendor vendor)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.ConnectionConfig.GetConnection()))
            {
                var cmd = new SqlCommand("Insertvendor", con);
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Vendorname", vendor.Vendorname);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Vendor> PopulateVendor()
        {
            throw new NotImplementedException();
        }

        public void Update(Vendor vendor)
        {
            throw new NotImplementedException();
        }
    }
}