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
    
    public class DronesController : Controller
    {
        List<SelectListItem> GetRoleItems()
        {
            List<SelectListItem> ProposedReturnValue = new List<SelectListItem>();
            using (ContextBLL ctx = new ContextBLL())
            {
                List<RolesBLL> roles = ctx.GetRoles(0, 25);
                foreach (RolesBLL r in roles)
                {
                    SelectListItem i = new SelectListItem();

                    i.Value = r.RoleID.ToString();
                    i.Text = r.RoleName;
                    ProposedReturnValue.Add(i);
                }
            }
            return ProposedReturnValue;
        }

        public ActionResult Page(int PageNumber, int PageSize)
        {

            ViewBag.PageNumber = PageNumber;
            ViewBag.PageSize = PageSize;
            List<DronesBLL> Model = new List<DronesBLL>();

            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {

                    ViewBag.TotalCount = ctx.ObtainDroneCount();
                    Model = ctx.GetDrones(PageNumber * PageSize, PageSize);
                }
                return View("Index", Model);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }

        }

        // GET: Drones
        [MustBeInRole(Roles = "Admin")]
        public ActionResult Index()
        {

            List<DronesBLL> Model = new List<DronesBLL>();
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    ViewBag.PageNumber = 0;
                    ViewBag.PageSize = 3;
                    ViewBag.TotalCount = ctx.ObtainDroneCount();
                    Model = ctx.GetDrones(0, 3);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View(Model);
        }
        [MustBeInRole(Roles = "Admin")]
        // GET: Drones/Details/5
        public ActionResult Details(int id)
        {
            DronesBLL Drone;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    Drone = ctx.FindDrone(id);
                    if (null == User)
                    {
                        return View("ItemNotFound"); // BKW make this view
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View(Drone);
        }
        [MustBeInRole(Roles = "Admin")]
        // GET: Drones/Create
        public ActionResult Create()
        {
            DronesBLL defDrone = new DronesBLL();
            defDrone.DroneID = 0;
            ViewBag.Roles = GetRoleItems();
            return View(defDrone);
        }
        [MustBeInRole(Roles = "Admin")]
        // POST: Drones/Create
        [HttpPost]
        public ActionResult Create(RavenBLL.DronesBLL collection)
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
                    ctx.CreateDrone(collection);
                }

                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                ViewBag.Exception = Ex;
                return View("Error");
            }
        }
        [MustBeInRole(Roles = "Admin")]
        // GET: Drones/Edit/5
        public ActionResult Edit(int id)
        {
            DronesBLL Drone;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    Drone = ctx.FindDrone(id);
                    if (null == Drone)
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
            ViewBag.Roles = GetRoleItems();
            return View(Drone);
        }
        [MustBeInRole(Roles = "Admin")]
        // POST: Drones/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, RavenBLL.DronesBLL collection)
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
                    ctx.JustUpdateDrone(collection);
                }

                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                ViewBag.Exception = Ex;
                return View("Error");
            }
        }
        [MustBeInRole(Roles = "Admin")]
        // GET: Drones/Delete/5
        public ActionResult Delete(int id)
        {
            DronesBLL Drone;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    Drone = ctx.FindDrone(id);
                    if (null == Drone)
                    {
                        return View("ItemNotFound"); // BKW make this view
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View(Drone);
        }
        [MustBeInRole(Roles = "Admin")]
        // POST: Drones/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, RavenBLL.DronesBLL collection)
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
                    ctx.DeleteDrone(id);
                }

                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                ViewBag.Exception = Ex;
                return View("Error");
            }
        }

        public ActionResult IndxOfDronesToUsers(int PageNumber, int PageSize)
        {

            ViewBag.PageNumber = PageNumber;
            ViewBag.PageSize = PageSize;
            List<DronesBLL> Model = new List<DronesBLL>();

            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    UsersBLL U = ctx.FindUserByEmail(User.Identity.Name);
                    ViewBag.TotalCount = ctx.ObtainUserCount();
                    Model = ctx.GetDronesRelatedToUser(U.UserID, PageNumber * PageSize, PageSize);
                }
                return View("Index", Model);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
        }
    }
}
