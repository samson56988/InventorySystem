using InventoryManagementSofware.BuisnessInterface.PurchaseProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventoryManagementSofware.Models;
using System.Data.SqlClient;
using System.Data;

namespace InventoryManagementSofware.BuisnessLogic.PurchaseProductBL
{
    public class PurchaseProductRepository : IPurchaseProductRepository
    {
        public IList<Purchaseproducts> ConfirmPurchasedProduct(string user)
        {
            List<Purchaseproducts> product = new List<Purchaseproducts>();
            
            using (SqlConnection con = new SqlConnection(ConnectionString.ConnectionConfig.GetConnection()))
            {
                SqlCommand cmd = new SqlCommand("Select PurchasedProducts.ID,p.Product,v.Vendor,PurchasedProducts.Quantity,PurchasedProducts.Purchaseprice,PurchasedProducts.Date,PurchasedProducts.Total,PurchasedProducts.Status from PurchasedProducts inner join Product p on PurchasedProducts.ProductID = p.ProductID inner join Vendor v on v.VendorID = PurchasedProducts.VendorID where PurchasedProducts.Status = 'Pending' and PurchasedProducts.PurchaseBy = '" + user+"'", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var products = new Purchaseproducts()
                    {
                        ID = Convert.ToInt32(rdr["ID"].ToString()),
                        Productname = rdr["Product"].ToString(),
                        Vendorname = rdr["Vendor"].ToString(),
                        Quantity = Convert.ToInt32(rdr["Quantity"].ToString()),
                        Purchaseprice = Convert.ToDecimal(rdr["Purchaseprice"].ToString()),
                        Date = Convert.ToDateTime(rdr["Date"].ToString()),
                        Total = Convert.ToDecimal(rdr["Total"].ToString()),
                        Status = rdr["Status"].ToString()



                    };
                    product.Add(products);
                }
                return (product);
            }
        }

        public void Delete(Purchaseproducts purchase)
        {
            throw new NotImplementedException();
        }

        public IList<Purchaseproducts> GetPurchaseProduct()
        {
            List<Purchaseproducts> product = new List<Purchaseproducts>();
            using (SqlConnection con = new SqlConnection(ConnectionString.ConnectionConfig.GetConnection()))
            {
                SqlCommand cmd = new SqlCommand("getPurchasedList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var products = new Purchaseproducts()
                    {
                        ID = Convert.ToInt32(rdr["ID"].ToString()),
                        Productname = rdr["Product"].ToString(),
                        Vendorname = rdr["Vendor"].ToString(),
                        TransactionNo = rdr["TransactionNo"].ToString(),
                        Quantity = Convert.ToInt32(rdr["Quantity"].ToString()),
                        Purchaseprice = Convert.ToDecimal(rdr["Purchaseprice"].ToString()),
                        Date = Convert.ToDateTime(rdr["Date"].ToString()),
                        Total = Convert.ToDecimal(rdr["Total"].ToString()),
                        Status = rdr["Status"].ToString()
                        


                    };
                    product.Add(products);
                }
                return (product);
            }
        }

        public Purchaseproducts GetPurchaseProductID(int? id)
        {
            throw new NotImplementedException();
        }

        public void InsertNew(Purchaseproducts purchase)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.ConnectionConfig.GetConnection()))
            {
                var cmd = new SqlCommand("PurchaseProduct", con);
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TransactionNo", "Pending");
                cmd.Parameters.AddWithValue("@PurchasePrice", purchase.Purchaseprice);
                cmd.Parameters.AddWithValue("@Quantity", purchase.Quantity);
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                cmd.Parameters.AddWithValue("@ProductID", purchase.ProductID);
                cmd.Parameters.AddWithValue("@Discount", purchase.Discount);
                cmd.Parameters.AddWithValue("@VendorID", purchase.Vendor);
                cmd.Parameters.AddWithValue("@Status", "Pending");
                cmd.Parameters.AddWithValue("@Purchaseby", purchase.PurchasedBy);
                cmd.ExecuteNonQuery();

                var cmd2 = new SqlCommand("Update Product Set Quantity = (Quantity +" + purchase.Quantity + ") where ProductID = '" + purchase.ProductID + "'", con);
                cmd2.ExecuteNonQuery();
            }
        }

        public List<Product> PopulateProduct()
        {
            List<Product> product = new List<Product>();

            using (SqlConnection con = new SqlConnection(ConnectionString.ConnectionConfig.GetConnection()))
            {

                using (SqlCommand cmd = new SqlCommand("select * from Product", con))
                {
                    cmd.Connection = con;

                    con.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            product.Add(
                                new Product
                                {
                                    ProductID = Convert.ToInt32(sdr["ProductID"].ToString()),
                                    Productname = sdr["Product"].ToString()
                                }

                                );
                        }
                        con.Close();
                    }


                }

                return product;
            }
        }

        public List<Vendor> PopulateVendor()
        {
            List<Vendor> vendor = new List<Vendor>();

            using (SqlConnection con = new SqlConnection(ConnectionString.ConnectionConfig.GetConnection()))
            {

                using (SqlCommand cmd = new SqlCommand("select * from Vendor", con))
                {
                    cmd.Connection = con;

                    con.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            vendor.Add(
                                new Vendor
                                {
                                    VendorID = Convert.ToInt32(sdr["VendorID"].ToString()),
                                    Vendorname = sdr["Vendor"].ToString()
                                }

                                );
                        }
                        con.Close();
                    }


                }

                return vendor;
            }
        }

        public IList<Purchaseproducts> TransactionList()
        {
            List<Purchaseproducts> product = new List<Purchaseproducts>();
            using (SqlConnection con = new SqlConnection(ConnectionString.ConnectionConfig.GetConnection()))
            {
                SqlCommand cmd = new SqlCommand("GetTransaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var products = new Purchaseproducts()
                    {
                        TransactionNo = rdr["TransactionNo"].ToString(),
                        Date = Convert.ToDateTime(rdr["Date"].ToString()),
                        PurchasedBy = rdr["PurchaseBy"].ToString()
                    };
                    product.Add(products);
                }
                return (product);
            }
        }

        public void Update(Purchaseproducts purchase)
        {
            throw new NotImplementedException();
        }
    }
}