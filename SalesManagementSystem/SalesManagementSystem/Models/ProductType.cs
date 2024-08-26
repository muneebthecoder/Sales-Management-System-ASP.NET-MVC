using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;


namespace SalesManagementSystem.Models
{
    public class ProductType
    {
        public int id { get; set; }

        [Display(Name = "Enter Product Title")]
        public string title { get; set; }
        [Display(Name = "Decription")]
        public string decscription { get; set; }

        DBAccess obj = new DBAccess();

        public List<ProductType> GetType()
        {
            List<ProductType> ulist = new List<ProductType>();
            obj.OpenCon();
            string q = "Select * from ProductType";
            obj.cmd = new SqlCommand(q, obj.con);
            SqlDataReader sdr = obj.cmd.ExecuteReader();
            while (sdr.Read())
            {
                ProductType u = new ProductType();
                u.id = int.Parse(sdr[0].ToString());
                u.title = sdr[1].ToString();
                u.decscription = sdr[2].ToString();

                ulist.Add(u);
            }
            sdr.Close();
            obj.CloseCon();
            return ulist;
        }
    }
}