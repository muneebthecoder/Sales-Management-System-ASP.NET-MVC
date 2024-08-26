using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace SalesManagementSystem.Models
{
    public class Sales
    {
        public string OrderId { get; set; }
        public string OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }
        public string SelectProduct { get; set; }
        public int Cost { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }

        DBAccess obj = new DBAccess();

        public List<Sales> Saleslist()
        {
            List<Sales> clist = new List<Sales>();
            obj.OpenCon();
            string q = "Select * from sales";
            obj.cmd = new SqlCommand(q, obj.con);
            SqlDataReader sdr = obj.cmd.ExecuteReader();
            while (sdr.Read())
            {
                Sales s = new Sales();
                s.OrderId = sdr[0].ToString();
                s.OrderDate = sdr[1].ToString();
                s.CustomerName = sdr[2].ToString();
                s.CustomerNumber = sdr[3].ToString();
                s.SelectProduct = sdr[4].ToString();
                s.Cost = int.Parse(sdr[5].ToString());
                s.Quantity = int.Parse(sdr[7].ToString());
                s.Total= int.Parse(sdr[6].ToString());
                clist.Add(s);
            }
            sdr.Close();
            obj.CloseCon();
            return clist;
        }

        public Sales SalesDetail(int id)
        {
            Sales s = new Sales();
            obj.OpenCon();
            string q = "Select * from sales where OrderId='"+id+"'";
            obj.cmd = new SqlCommand(q, obj.con);
            SqlDataReader sdr = obj.cmd.ExecuteReader();
            sdr.Read();

            s.OrderId = sdr[0].ToString();
            s.OrderDate = sdr[1].ToString();
            s.CustomerName = sdr[2].ToString();
            s.CustomerNumber = sdr[3].ToString();
            s.SelectProduct = sdr[4].ToString();
            s.Cost = int.Parse(sdr[5].ToString());
            s.Quantity = int.Parse(sdr[6].ToString());

            sdr.Close();
            obj.CloseCon();

            return s;
        }
    }
}