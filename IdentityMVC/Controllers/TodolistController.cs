using IdentityMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentityMVC.Controllers
{
    [Authorize]
    public class TodolistController : Controller
    {
        
        ApplicationDbContext conn = new ApplicationDbContext();
        // GET: Todolist
        public ActionResult Index()
        {
            var email = Session["Id"];
            List<Todolist> rdatas = new List<Todolist>();
            foreach (Todolist tdah in conn.todolist.ToList())
            {
                if (tdah.UserId == Session["Id"].ToString())
                {
                    rdatas.Add(tdah);
                }
            }
            //if ()
            return View(rdatas.ToList());
            //return View(conn.todolist.ToList());
        }

        // GET: Todolist/Details/5
        public ActionResult Details(int id)
        {
            return View(conn.todolist.Where(x => x.Id == id).FirstOrDefault());
        }

        // GET: Todolist/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Todolist/Create
        [HttpPost]
        public ActionResult Create(Todolist item)
        {
            try
            {
                item.UserId = Session["Id"].ToString();
                conn.todolist.Add(item);
                conn.SaveChanges();
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Todolist/Edit/5
        public ActionResult Edit(int id)
        {
            return View(conn.todolist.Where(x => x.Id == id).FirstOrDefault());
        }

        // POST: Todolist/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Todolist item)
        {
            // TODO: Add update logic here
            if (ModelState.IsValid)
            {
                item.UserId = Session["Id"].ToString();
                conn.Entry(item).State = EntityState.Modified;
                conn.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Todolist/Delete/5
        public ActionResult Delete(int id)
        {
            return View(conn.todolist.Where(x => x.Id == id).FirstOrDefault());
        }

        // POST: Todolist/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Todolist todoo = conn.todolist.Where(x => x.Id == id).FirstOrDefault();
                conn.todolist.Remove(todoo);
                conn.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
