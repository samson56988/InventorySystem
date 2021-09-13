using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventoryManagementSofware.Models;
using InventoryManagementSofware.BuisnessInterface.CategoryService;
using InventoryManagementSofware.ConnectionString;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace InventoryManagementSofware.BuisnessLogic.CategoryBL
{
    public class CategoryRepository:ICategoryRepository
    {

        private readonly string CS = ConfigurationManager.ConnectionStrings["InventoryConnection"].ConnectionString;


        public void Delete(Category category)
        {
            throw new NotImplementedException();
        }

        public IList<Category> GetCategory()
        {
            List<Category> category = new List<Category>();
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("GetCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var categorys = new Category()
                    {
                       Name = rdr["Name"].ToString(),
                       Active =rdr["Active"].ToString()

                    };
                    category.Add(categorys);
                }
                return (category);
            }
        }

        public Category GetCategoryId(int? id)
        {
            throw new NotImplementedException();
        }

        public void InsertNew(Category category)
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                var cmd = new SqlCommand("InsertCategory", con);
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", category.Name);
                cmd.Parameters.AddWithValue("@Active", category.Active);
                cmd.ExecuteNonQuery();
            }

        }

        public void Update(Category category)
        {
            throw new NotImplementedException();
        }
    }
}