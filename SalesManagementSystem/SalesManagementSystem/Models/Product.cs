using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace SalesManagementSystem.Models
{
    public class Product
    {
        [Display(Name = "Product Id")]

        public int id { get; set; }
        [Display(Name = "Product Name")]

        public string pname { get; set; }
        [Display(Name = "Product Company")]

        public string pcompany { get; set; }
        [Display(Name = "Product Type")]

        public string ptype { get; set; }
        [Display(Name = "Product Ptice")]

        public int price { get; set; }
        [Display(Name = "Product Stock")]

        public int stock { get; set; }

        DBAccess obj = new DBAccess();

        public List<Product> productlist()
        {
            List<Product> clist = new List<Product>();
            obj.OpenCon();
            string q = "Select * from product";
            obj.cmd = new SqlCommand(q, obj.con);
            SqlDataReader sdr = obj.cmd.ExecuteReader();
            while (sdr.Read())
            {
                Product s = new Product();
                s.id = int.Parse(sdr[0].ToString());
                s.pname = sdr[1].ToString();
                s.pcompany = sdr[2].ToString();
                s.ptype = sdr[3].ToString();
                s.price = int.Parse(sdr[4].ToString());
                s.stock = int.Parse(sdr[5].ToString());
                clist.Add(s);
            }
            sdr.Close();
            obj.CloseCon();
            return clist;
        }
    }
}