using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RavenBLL;

namespace RavenMVC.Controllers
{
    public class ErrorTestController : Controller
    {
        // GET: ErrorTest
        public ActionResult Index()
        {
            return View();
        }

        // GET: ErrorTest/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ErrorTest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ErrorTest/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(collection);
                }
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ErrorTest/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ErrorTest/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(collection);
                }
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ErrorTest/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ErrorTest/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(collection);
                }
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
