using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using InventoryManagementSofware.BuisnessInterface;
using InventoryManagementSofware.BuisnessLogic;
using System.Data.SqlClient;
using System.Data;
using InventoryManagementSofware.BuisnessInterface.ProductService;
using InventoryManagementSofware.Models;
using System.Web.Mvc;

namespace InventoryManagementSofware.BuisnessLogic.ProductService
{
    public class ProductRepository : IProductRepository
    {
        public void Delete(Product product)
        {
            throw new NotImplementedException();
        }

        public IList<Product> GetProduct()
        {
            List<Product> product = new List<Product>();
            using (SqlConnection con = new SqlConnection(ConnectionString.ConnectionConfig.GetConnection()))
            {
                SqlCommand cmd = new SqlCommand("GetProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var products = new Product()
                    {
                        ProductID = Convert.ToInt32(rdr["ProductID"].ToString()),
                        Productname = rdr["Product"].ToString(),
                        Category = rdr["Name"].ToString(),
                        Subcategory = rdr["SubCategory"].ToString(),
                        Price = Convert.ToDecimal(rdr["Price"].ToString()),
                        
                    };
                    product.Add(products);
                }
                return (product);
            }
        }

        public Product GetProduct(int? id)
        {
            throw new NotImplementedException();
        }

        public IList<Product> GetProductStock()
        {
            List<Product> product = new List<Product>();

            using (SqlConnection con = new SqlConnection(ConnectionString.ConnectionConfig.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("getProductStock", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtTable = new DataTable();

                    dtTable.Load(sdr);

                    foreach (DataRow row in dtTable.Rows)
                    {
                        product.Add(
                            new Product
                            {
                                ProductID = Convert.ToInt32(row["ProductID"].ToString()),
                                Productname = row["Product"].ToString(),
                                Category = row["Name"].ToString(),
                                Subcategory = row["SubCategory"].ToString(),
                                Quantity = Convert.ToInt32(row["Quantity"].ToString())
                            }

                            );
                    }
                }
            }
            return product;
        }

        public void InsertNew(Product product)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.ConnectionConfig.GetConnection()))
            {
                var cmd = new SqlCommand("Addproduct", con);
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SubcategoryID", product.SubcategoryID);
                cmd.Parameters.AddWithValue("@Product", product.Productname);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Subcategory> PopulateSubcategory()
        {
            List<Subcategory> subcategory = new List<Subcategory>();

            using (SqlConnection con = new SqlConnection(ConnectionString.ConnectionConfig.GetConnection()))
            {

                using (SqlCommand cmd = new SqlCommand("select * from SubCategory", con))
                {
                    cmd.Connection = con;

                    con.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            subcategory.Add(
                                new Subcategory
                                {
                                    ID = Convert.ToInt32(sdr["ID"].ToString()),
                                    SubcategoryName = sdr["SubCategory"].ToString()
                                }

                                );
                        }
                        con.Close();
                    }


                }

                return subcategory;
            }
        }

        public void Update(Product product)
        {
            
        }
    }
}