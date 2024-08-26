using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;

namespace SalesManagementSystem.Models
{
    public class Company
    {
        public int id{get;set;}

        [Display(Name = "Company Name")]
        public string cname { get; set; }

        [Display(Name = "Company Logo")]
        [Required(ErrorMessage = "Please Upload Comapny Logo")]
        public HttpPostedFileBase logo { get; set; }

        public string filepath { get; set; }

        [Display(Name = "Contact No")]
        public string mobile { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email Required!")]
        public string email { get; set; }

        [Display(Name = "Website URL")]
        [Required(ErrorMessage = "WebSite Required!")]
        public string websiteurl { get; set; }

        [Display(Name = "Country")]
        public string country { get; set; }

        DBAccess obj = new DBAccess();

        public List<Company> Companylist()
        {
            List<Company> clist = new List<Company>();
            obj.OpenCon();
            string q = "Select * from Company";
            obj.cmd = new SqlCommand(q, obj.con);
            SqlDataReader sdr = obj.cmd.ExecuteReader();
            while (sdr.Read())
            {
                Company s = new Company();
                s.id = int.Parse(sdr[0].ToString());
                s.cname = sdr[1].ToString();
                s.filepath = sdr[2].ToString();
                s.mobile = sdr[3].ToString();
                s.email = sdr[4].ToString();
                s.websiteurl =sdr[5].ToString();
                s.country = sdr[6].ToString();
                clist.Add(s);
            }
            sdr.Close();
            obj.CloseCon();
            return clist;
        }
    }

}