﻿using IdentityMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentityMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SupplierController : Controller
    {
        newConn conn2 = new newConn();
        ApplicationDbContext conn = new ApplicationDbContext();
        // GET: Supplier
        public ActionResult Index()
        {
            return View(conn2.Suppliers.ToList());
        }

        // GET: Supplier/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            return RedirectToAction("Register", "LR");
        }

        // POST: Supplier/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Supplier/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Supplier/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Supplier/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Supplier/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}