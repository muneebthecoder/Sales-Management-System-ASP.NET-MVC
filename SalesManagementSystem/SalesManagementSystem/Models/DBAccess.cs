using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace SalesManagementSystem.Models
{
    public class DBAccess
    {
        static string constr = @"Data Source=DESKTOP-T6BURMO\SQLEXPRESS; initial catalog=Sales ;integrated security=true;";
        public SqlConnection con = new SqlConnection(constr);
        public SqlCommand cmd = null;
        public void OpenCon()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }

        public void CloseCon()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        public void InsertUpdateDelete(string query)
        {
            OpenCon();
            cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            CloseCon();
        }
    }
}