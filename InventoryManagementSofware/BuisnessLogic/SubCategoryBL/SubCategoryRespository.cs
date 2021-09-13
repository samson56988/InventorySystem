using InventoryManagementSofware.BuisnessInterface.SubCategoryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagementSofware.Models;
using System.Data;
using System.Data.SqlClient;


namespace InventoryManagementSofware.BuisnessLogic.SubCategoryBL
{
    public class SubCategoryRespository : ISubCategoryRepository
    {
        public void Delete(Subcategory subcategory)
        {
            throw new NotImplementedException();
        }

        public IList<Subcategory> GetSubCategory()
        {
            List<Subcategory> subcategory = new List<Subcategory>();
            using (SqlConnection con = new SqlConnection(ConnectionString.ConnectionConfig.GetConnection()))
            {
                SqlCommand cmd = new SqlCommand("GetSubcategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var subcategorys = new Subcategory()
                    {
                        ID = Convert.ToInt32(rdr["ID"].ToString()),
                        Categoryname = rdr["Name"].ToString(),
                        SubcategoryName = rdr["SubCategory"].ToString(),
                        Status = rdr["Status"].ToString()
                    };
                    subcategory.Add(subcategorys);
                }
                return (subcategory);
            }
        }      

        public void InsertNew(Subcategory subcategory)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.ConnectionConfig.GetConnection()))
            {
                var cmd = new SqlCommand("InsertSubcategory", con);
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CategoryID", subcategory.CategoryID);
                cmd.Parameters.AddWithValue("@Subcategory", subcategory.SubcategoryName);
                cmd.Parameters.AddWithValue("@Status", subcategory.Status);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Category> PopulateCategory()
        {
            List<Category> category = new List<Category>();

            using (SqlConnection con = new SqlConnection(ConnectionString.ConnectionConfig.GetConnection()))
            {

                using (SqlCommand cmd = new SqlCommand("select * from Category", con))
                {
                    cmd.Connection = con;

                    con.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            category.Add(
                                new Category
                                {
                                    ID = Convert.ToInt32(sdr["ID"].ToString()),
                                    Name = sdr["Name"].ToString()
                                }

                                );
                        }
                        con.Close();
                    }


                }

                return category;
            }
        }

        public void Update(Subcategory subcategory)
        {
            throw new NotImplementedException();
        }

        Subcategory ISubCategoryRepository.GetCategoryId(int? id)
        {
            throw new NotImplementedException();
        }

       
    }
}
