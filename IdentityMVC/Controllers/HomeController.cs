﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentityMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult About()
        {
            if (Session["Id"] != null)
            {
                ViewBag.Message = "Your application description page.";
                return View();
            }
            else
            {
                
                return Redirect ("/Account/Login#about");
                //return RedirectToAction("Login", "Account");
            }
            
        }

        public ActionResult Contact()
        {
            if (Session["Id"] != null)
            {
                ViewBag.Message = "Your contact page.";
                return View();
            }
            else
            {
                return Redirect("/Account/Login#about");
            }
            
        }
        public ActionResult Role()
        {
            return View();
        }
    }
}