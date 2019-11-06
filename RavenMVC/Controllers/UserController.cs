using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using RavenBLL;
using RavenMVC.Models.Filters;

namespace RavenMVC.Controllers
{
    public class UserController : Controller
    {
        List<SelectListItem> GetRoleItems()
        {

            //This is to help find the list of items that will show up in the 
            //Roles Drop down list
            List<SelectListItem> ProposedReturnValue = new List<SelectListItem>();
            using (ContextBLL ctx = new ContextBLL())
            {
                int rolecount = ctx.ObtainRoleCount();

                List<RolesBLL> roles = ctx.GetRoles(0, rolecount);
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
        //This is the paging that I have  initiated
        public ActionResult Page(int PageNumber, int PageSize)
        {

            ViewBag.PageNumber = PageNumber;
            ViewBag.PageSize = PageSize;
            List<UsersBLL> Model = new List<UsersBLL>();

            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    ViewBag.TotalCount = ctx.ObtainUserCount();
                    Model = ctx.GetUsers(PageNumber * PageSize, PageSize);
                }
                return View("Index", Model);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }

        }



        // GET: User
        [MustBeInRole(Roles = "PoliceAdmin,Admin,PoliceUser")]
        public ActionResult Index()
        {
            List<UsersBLL> Model = new List<UsersBLL>();
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    ViewBag.PageNumber = 0;
                    ViewBag.PageSize = 3;
                    ViewBag.TotalCount = ctx.ObtainUserCount();
                    Model = ctx.GetUsers(0, 3);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View(Model);
        }


        // GET: User/Details/5
        [MustBeInRole(Roles = "PoliceAdmin,Admin,PoliceUser")]
        public ActionResult Details(int id)
        {
            UsersBLL User;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    User = ctx.FindUser(id);
                    if (null == User)
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
            return View(User);
        }

        // GET: User/Create
        [MustBeInRole(Roles ="PoliceAdmin,Admin")]
        public ActionResult Create()
        {
            UsersBLL defUser = new UsersBLL();
            defUser.UserID = 0;
            ViewBag.Roles = GetRoleItems();
            return View(defUser);
        }

        // POST: User/Create
        [HttpPost]
        [MustBeInRole(Roles = "PoliceAdmin,Admin")]
        public ActionResult Create(RavenBLL.UsersBLL collection)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    ViewBag.Roles = GetRoleItems();
                    return View(collection);
                }
                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.CreateUser(collection);
                }
                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                ViewBag.Exception = Ex;
                return View("Error");
            }
        }

        // GET: User/Edit/5
        [MustBeInRole(Roles = "PoliceAdmin,Admin")]
        public ActionResult Edit(int id)
        {
            UsersBLL User;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    User = ctx.FindUser(id);
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
            ViewBag.Roles = GetRoleItems();
            return View(User);
        }

        // POST: User/Edit/5
        [HttpPost]
        [MustBeInRole(Roles = "PoliceAdmin,Admin")]
        public ActionResult Edit(int id, RavenBLL.UsersBLL collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Roles = GetRoleItems();
                    return View(collection);
                }
                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.JustUpdateUser(collection);
                }
                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                ViewBag.Exception = Ex;
                return View("Error");
            }
        }

        // GET: User/Delete/5
        [MustBeInRole(Roles = "PoliceAdmin,Admin")]
        public ActionResult Delete(int id)
        {
            UsersBLL User;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    User = ctx.FindUser(id);
                    if (null == User)
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
            return View(User);
        }

        // POST: User/Delete/5
        [HttpPost]
        [MustBeInRole(Roles = "PoliceAdmin,Admin")]
        public ActionResult Delete(int id, RavenBLL.UsersBLL collection)
        {
            try
            {
                //if(!ModelState.IsValid)
                //{
                //    return View(collection);
                //}

                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.DeleteUser(id);
                }

                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                ViewBag.Exception = Ex;
                return View("Error");
            }
        }
        public ActionResult IndexOfMyStuff()
        {
            try
            {

                using (ContextBLL ctx = new ContextBLL())
                {
                    UsersBLL U = ctx.FindUserByEmail(User.Identity.Name);//need to replace User.Identity.Name with search bar logic
                }

                return View("Details");//change to Details later
            }
            catch (Exception Ex)
            {
                ViewBag.Exception = Ex;
                return View("Error");
            }
        }
        
    }
}

