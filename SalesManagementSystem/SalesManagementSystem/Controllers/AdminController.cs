using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.IO;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        DBAccess dbaobj = new DBAccess();

        // GET: Admin
        //Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(Admin a)
        {
         
            dbaobj.OpenCon();
            string q = "Select aid,name from Admin where aid='" + a.aid + "' and password= '" + a.password + "'";
            dbaobj.cmd = new SqlCommand(q, dbaobj.con);
            SqlDataReader sdr = dbaobj.cmd.ExecuteReader();
            sdr.Read();
            if (sdr.HasRows)
            {
                Session["aid"] = sdr["aid"].ToString();
                Session["aname"] = sdr["name"].ToString();
            }
            else
            {
                Response.Write("<script>alert('Invalid User!');</script>");
            }
            sdr.Close();
            dbaobj.CloseCon();

            return RedirectToAction("DashBoard");
        }
        //Signup
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(Admin a)
        {

            if (ModelState.IsValid)
            {

                    a.aid = a.email.Split('@')[0];
                    string q = "insert into admin values('" + a.aid + "','" + a.name + "','" + a.age + "','" + a.email + "','" + a.password + "') ";
                    dbaobj.InsertUpdateDelete(q);
                    Response.Write("<script>alert('" + a.name + " Registered!');</script>");
                
            }
            return View(a);
        }
        //Dashboard
        public ActionResult DashBoard()
        {
            if (Session["aid"] != null && Session["aname"] != null)
            {
                return View();
            }
            return RedirectToAction("SignUp");
        }
        //Logout
        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }
    }
}