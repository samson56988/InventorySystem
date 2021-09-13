using InventoryManagementSofware.BuisnessInterface.AuthenticationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventoryManagementSofware.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.Security;

namespace InventoryManagementSofware.BuisnessLogic.AuthenticationBL
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        void connectionString()
        {
            con.ConnectionString = ConnectionString.ConnectionConfig.GetConnection();
        }

        public IEnumerable<Login> Login(Login log)
        {
            string username = "", rolename = "";
            bool found = false;
            SqlDataReader dr;
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "Select * from Login where Username = '" + log.username + "'and Password = '" + log.password + "'";

            dr = com.ExecuteReader();

            if (dr.Read())
            {
                found = true;
                username = dr["Username"].ToString();
                rolename = dr["Role"].ToString();
                log.Role = dr["Role"].ToString();
                FormsAuthentication.SetAuthCookie(log.username, true);
                FormsAuthentication.SetAuthCookie(log.Role, true);

            }
            else
            {
                found = false;
            }
            dr.Close();
            con.Close();
            if (found == true)
            {
                if (rolename == "Teacher")
                {
                    FormsAuthentication.SetAuthCookie(log.username, true);

                }
                else if (rolename == "Admin")
                {
                    FormsAuthentication.SetAuthCookie(log.username, true);

                }
            }
            else
            {

            }
            con.Close();
            yield return log;
        }

        public void Registerusers(Login login)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.ConnectionConfig.GetConnection()))
            {
                var cmd = new SqlCommand("AddUsers", con);
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Role", login.Role);
                cmd.Parameters.AddWithValue("@Username", login.username);
                cmd.Parameters.AddWithValue("@Password", login.password);
                cmd.ExecuteNonQuery();
            }

        }

        public IList<Login> GetUsers()
        {
            List<Login> login = new List<Login>();
            using (SqlConnection con = new SqlConnection(ConnectionString.ConnectionConfig.GetConnection()))
            {
                SqlCommand cmd = new SqlCommand("GetUsers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var logins = new Login()
                    {
                        username = rdr["Username"].ToString(),
                        password = rdr["Password"].ToString(),
                        Role = rdr["Role"].ToString()

                    };
                    login.Add(logins);
                }
                return (login);
            }
        }
    }
}