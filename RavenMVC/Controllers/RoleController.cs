using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RavenBLL;
using RavenMVC.App_Start;
using RavenMVC.Models.Filters;

namespace RavenMVC.Controllers
{
    [MustBeInRole(Roles = "Admin")]
    public class RoleController : Controller
    {
        public ActionResult Page(int PageNumber, int PageSize)
        {

            ViewBag.PageNumber = PageNumber;
            ViewBag.PageSize = PageSize;
            List<RolesBLL> Model = new List<RolesBLL>();
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    ViewBag.TotalCount = ctx.ObtainRoleCount();
                    Model = ctx.GetRoles(PageNumber * PageSize, PageSize);
                }
                return View("Index", Model);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            

        }
        // GET: Role
        public ActionResult Index()
        {
            List<RolesBLL> Model = new List<RolesBLL>();
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    ViewBag.PageNumber = 0;
                    ViewBag.PageSize = 3;
                    ViewBag.TotalCount = ctx.ObtainRoleCount();
                    Model = ctx.GetRoles(0, 3);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View(Model);

            //return RedirectToRoute(new { Controller = "Role", Action = "Page", PageNumber = 0, PageSize = ApplicationConfig.DefaultPageSize });
        }

        // GET: Role/Details/5
        public ActionResult Details(int id)
        {
            RolesBLL Role;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    Role = ctx.FindRoleByID(id);
                    if (null == Role)
                    {
                        return View("ItemNotFound"); // EAG make this view
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View(Role);
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            RolesBLL defRole = new RolesBLL();
            defRole.RoleID = 0;
            return View(defRole);
        }

        // POST: Role/Create
        [HttpPost]
        public ActionResult Create(RavenBLL.RolesBLL collection)
        {
            if (!ModelState.IsValid)
            {
                return View(collection);
            }
            try
            {
                // TODO: Add insert logic here
                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.CreateRole(collection);
                }

                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                ViewBag.Exception = Ex;
                return View("Error");
            }
        }

        // GET: Role/Edit/5
        public ActionResult Edit(int id)
        {
            RolesBLL Role;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    Role = ctx.FindRoleByID(id);
                    if (null == Role)
                    {
                        return View("ItemNotFound"); 
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View(Role);
        }

        // POST: Role/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, RavenBLL.RolesBLL collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(collection);
                }
                // TODO: Add insert logic here
                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.JustUpdateRole(collection);
                }

                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                ViewBag.Exception = Ex;
                return View("Error");
            }
        }

        // GET: Role/Delete/5
        public ActionResult Delete(int id)
        {
            RolesBLL Role;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    Role = ctx.FindRoleByID(id);
                    if (null == Role)
                    {
                        return View("ItemNotFound"); // EG make this view
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View(Role);
        }

        // POST: Role/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, RavenBLL.RolesBLL collection)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return View(collection);
                //}
                // TODO: Add insert logic here
                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.DeleteRole(id);
                }

                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                ViewBag.Exception = Ex;
                return View("Error");
            }
        }
    }
}
