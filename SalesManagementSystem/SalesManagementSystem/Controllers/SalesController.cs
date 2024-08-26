using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesManagementSystem.Models;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace SalesManagementSystem.Controllers
{
    public class SalesController : Controller
    {
        DBAccess dbaobj = new DBAccess();
        // GET: Sales
        //AddCompany
        public ActionResult AddCompany()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCompany(Company c)
        {

            if (ModelState.IsValid)
            {
                string filename = c.logo.FileName;
                string ext = Path.GetExtension(filename);
                string[] extarr = { ".png", ".PNG", ".jpg", ".bmp", ".JPG" };
                if (extarr.Contains(ext))
                {
                    string ufilename = c.cname.Trim() + ext;
                    string path = Path.Combine(Server.MapPath("/images"), ufilename);
                    c.logo.SaveAs(path);
                    c.filepath = "/images/" + ufilename;
                    string q = "insert into company values('" + c.cname + "','" + c.filepath + "','" + c.mobile + "','" + c.email + "','" + c.websiteurl + "','" + c.country+ "') ";
                    dbaobj.InsertUpdateDelete(q);
                    Response.Write("<script>alert('" + c.cname + " Company Registered!');</script>");
                }
            }
            return View(c);
        }
        //Product Type
        public ActionResult ProductType()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ProductType(ProductType pt)
        {
            string q = "insert into producttype values('"+pt.title+"','"+pt.decscription+"')";
            dbaobj.InsertUpdateDelete(q);
            return View();
        }
        //Add Product

        public ActionResult AddProduct()
        {
            //ProductType
            List<SelectListItem> plist = new List<SelectListItem>();
            string pr = "select title from producttype";
            dbaobj.OpenCon();
            dbaobj.cmd = new SqlCommand(pr, dbaobj.con);
            SqlDataReader sdr1 = dbaobj.cmd.ExecuteReader();
            while (sdr1.Read())
            {
                plist.Add(new SelectListItem { Text = "" + sdr1["title"].ToString() + "", Value = "" + sdr1["title"].ToString() + "" });
            }
            sdr1.Close();
            dbaobj.CloseCon();

            //Company
            List<SelectListItem> clist = new List<SelectListItem>();
            string com = "select cname from Company";
            dbaobj.OpenCon();
            dbaobj.cmd = new SqlCommand(com, dbaobj.con);
            SqlDataReader sdr = dbaobj.cmd.ExecuteReader();
            while (sdr.Read())
            {
                clist.Add(new SelectListItem { Text = "" + sdr["cname"].ToString() + "", Value = "" + sdr["cname"].ToString() + "" });
            }
            sdr.Close();
            dbaobj.CloseCon();



            TempData["plist"] = plist.ToList();
            TempData["clist"] = clist.ToList();

            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(Product p)
        {         
            string q = "insert into product values('" + p.pname + "','" + p.pcompany + "','" + p.ptype + "','" + p.price + "','" + p.stock + "')";
            dbaobj.InsertUpdateDelete(q);
            return View();
        }
        //Sales
        public ActionResult AddSales()
        {
            List<SelectListItem> slist = new List<SelectListItem>();
            string s = "select pname,price from product";
            dbaobj.OpenCon();
            dbaobj.cmd = new SqlCommand(s, dbaobj.con);
            SqlDataReader sdr = dbaobj.cmd.ExecuteReader();
            while (sdr.Read())
            {
                slist.Add(new SelectListItem { Text = "" + sdr["pname"].ToString() + "", Value = "" + sdr["pname"].ToString() + "" });
            }
            sdr.Close();
            dbaobj.CloseCon();
            TempData["slist"] = slist;
            return View();
        }
        [HttpPost]
        public ActionResult AddSales(Sales s)
        {
            int total = s.Cost * s.Quantity;
            int qun = s.Quantity;
            string dt = DateTime.Now.ToShortDateString();
          //  string q = "update product set stock='"++"' where OrderId='" + s.OrderId + "'";

            string q = "insert into sales values('SM00" + s.CustomerNumber + "','" + dt + "','" + s.CustomerName + "','" + s.CustomerNumber + "','" + s.SelectProduct + "','" + s.Cost + "', '"+total+"' ,'" + s.Quantity + "')";
            dbaobj.InsertUpdateDelete(q);
            return View();
        }

        public ActionResult ProductTypeReport()
        {
            ProductType PT = new ProductType();
            return View(PT.GetType());
        }
        public ActionResult CompanyReport()
        {
            Company s = new Company();
            return View(s.Companylist());
        }
        public ActionResult ProductReport()
        {
            Product s = new Product();
            return View(s.productlist());
        }
        public ActionResult SalesReport()
        {
            Sales s = new Sales();
            return View(s.Saleslist());
        }



        public ActionResult DeleteSale(int id)
        {
            string q = "Delete from sales where OrderId='" + id + "'";
            dbaobj.InsertUpdateDelete(q);
            return RedirectToAction("DetailSale");
        }
        public ActionResult DetailSale(int id)
        {
            Sales s = new Sales();
            return View(s.SalesDetail(id));
        }
        public ActionResult EditSale(int id)
        {
            Sales s = new Sales();
            return View(s.SalesDetail(id));
        }
        [HttpPost]
        public ActionResult EditSale(Sales s)
        {
            string q = "update sales set CustomerName='" + s.CustomerName + "',CustomerNumber='" + s.CustomerNumber + "',SelectProduct='" + s.SelectProduct + "',Cost='" + s.Cost + "',Quantity='" + s.Quantity + "' where OrderId='" + s.OrderId+ "'";
            dbaobj.InsertUpdateDelete(q);
            return RedirectToAction("DetailSale");
        }
    }
}