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
    
    [MustBeLoggedIn]public class ObservationController : Controller
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
            List<ObservationsBLL> Model = new List<ObservationsBLL>();
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    ViewBag.TotalCount = ctx.ObtainObservationsCount();
                    Model = ctx.GetObservationsRelatedToObs(PageNumber * PageSize, PageSize);
                }
                return View("Index", Model);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
        }
            // GET: Observation
        public ActionResult Index()
        {
            List<ObservationsBLL> Model = new List<ObservationsBLL>();
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    ViewBag.PageNumber = 0;
                    ViewBag.PageSize = 3;
                    ViewBag.TotalCount = ctx.ObtainObservationsCount();
                    Model = ctx.GetObservationsRelatedToObs(0, 4);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View(Model);
        }
        // GET: Observation/Details/5
        public ActionResult Details(int id)
        {
            ObservationsBLL Observation;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    Observation = ctx.FindObservation(id);
                    if (null == Observation)
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
            return View(Observation);
        }

        // GET: Observation/Create
        [MustBeInRole(Roles = "Admin")]
        public ActionResult Create()
        {
            ObservationsBLL defObservation = new ObservationsBLL();
            defObservation.ObsID = 0;
            ViewBag.Roles = GetRoleItems();
            return View(defObservation);
        }

        // POST: Observation/Create
        [HttpPost]
        [MustBeInRole(Roles = "Admin")]
        public ActionResult Create(RavenBLL.ObservationsBLL collection)
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
                    ctx.CreateObservation(collection);
                }

                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                ViewBag.Exception = Ex;
                return View("Error");
            }
        }

        // GET: Observation/Edit/5
        [MustBeInRole(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            ObservationsBLL Observation;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    Observation = ctx.FindObservation(id);
                    if (null == Observation)
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
            return View(Observation);
        }

        // POST: Observation/Edit/5
        [HttpPost]
        [MustBeInRole(Roles = "Admin")]
        public ActionResult Edit(int id, RavenBLL.ObservationsBLL collection)
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
                    ctx.JustUpdateObservation(collection);
                }

                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                ViewBag.Exception = Ex;
                return View("Error");
            }
        }

        // GET: Observation/Delete/5
        [MustBeInRole(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            ObservationsBLL Observation;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    Observation = ctx.FindObservation(id);
                    if (null == Observation)
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
            return View(Observation);
        }

        // POST: Observation/Delete/5
        [HttpPost]
        [MustBeInRole(Roles = "Admin")]
        public ActionResult Delete(int id, RavenBLL.ObservationsBLL collection)
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
                    ctx.DeleteObservation(id);
                }

                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                ViewBag.Exception = Ex;
                return View("Error");
            }

        }
        public ActionResult IndxOfObservationsToDrones(int id, int PageNumber, int PageSize)
        {

            ViewBag.PageNumber = PageNumber;
            ViewBag.PageSize = PageSize;
            List<ObservationsBLL> Model = new List<ObservationsBLL>();

            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {


                    ViewBag.TotalCount = ctx.ObtainUserCount();
                    Model = ctx.GetObservationsRelatedToDroneID(id, PageNumber * PageSize, PageSize);
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
