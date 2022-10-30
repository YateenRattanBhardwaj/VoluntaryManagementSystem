using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using VMSApp.Filters;
using VMSApp.Models;
using VMSApp.ViewModels;
using System.Configuration;
using System.Data.Objects.SqlClient;
using AttributeRouting.Web.Mvc;
using VMSApp.Helpers;

namespace VMSApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.RegisterURL = "Register";
            ViewBag.Title = "Login";
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        
        [Route("Admin/Login")]
        [AllowAnonymous]
        [HttpGet]
        public ActionResult AdminLogin(string returnUrl)
        {
            ViewBag.Title = "Admin Login";
            ViewBag.RegisterURL = "SignUpAdmin";
            return View("Login");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AdminLogin(AdminNWorkerViewModel model, string returnUrl)
        {
            
            if (ModelState.IsValid)
            {
                using (DbVMSEntities entities = new DbVMSEntities())
                {

                    return RedirectToLocal(LoginHelper(entities, model.Email, model.Password));

                }


            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (DbVMSEntities entities = new DbVMSEntities())
                {

                    return RedirectToLocal(LoginHelper(entities, model.EmailId, model.Password));

                }


            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }
        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Guid AuthToken = Guid.Parse(Request.Cookies["AuthToken"].Value);
            using (DbVMSEntities entities = new DbVMSEntities())
            {
                UserLogin loggedInUser = entities.UserLogins.FirstOrDefault(ul => ul.AuthToken == AuthToken);
                if (loggedInUser != null)
                {
                    loggedInUser.IsLoggedOut = true;
                    loggedInUser.LogOutTimeStamp = DateTime.Now;
                    entities.SaveChanges();
                }
            }
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }

        [AllowAnonymous]

        public ActionResult Register()
        {
            UserTypeViewModel model = new UserTypeViewModel();
            using (DbVMSEntities entities = new DbVMSEntities())
            {
                model.UserTypes = entities.usertypes.Where(x => x.Name != "Admin").AsEnumerable().Select(x => new SelectListItem()
                  {
                      Text = x.Name,
                      Value = x.Id.ToString()
                  }).ToList();
            }
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserTypeViewModel model)
        {
            string returnUrl = "";
            if (ModelState.IsValid)
            {
                if (model.UserType == int.Parse(ConfigurationManager.AppSettings["Organization"]))
                {
                    returnUrl = "/SignUp/Organization";
                }
                else
                {
                    returnUrl = "/SignUp/Worker";
                }
                return RedirectToLocal(returnUrl);
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Please Select a value");
            return View(model);

        }


        [Route("SignUp/Organization")]
        [AllowAnonymous]
        [HttpGet]

        public ActionResult SignUpOrganization()
        {
            OrganizationViewModel model = new OrganizationViewModel();

            return View(PrepRegisterationModel(model));
        }

        // [Route("SignUp/Organization")]
        [AllowAnonymous]
        [HttpPost]

        public ActionResult SignUpOrganization(OrganizationViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (DbVMSEntities entities = new DbVMSEntities())
                {
                    User user=  getUser(model, int.Parse(ConfigurationManager.AppSettings["Organization"]))
;
                    VolunteerOrganization org = new VolunteerOrganization();
                    org.Description = model.Description;
                    org.Name = model.Name;
                    org.Phone = model.Phone;
                    org.Website = model.Website;
                    org.Status = 1;
                    user.VolunteerOrganizations.Add(org);
                    entities.Users.Add(user);
                    try
                    {
                        int i = entities.SaveChanges();
                        if (i > 0)
                        {
                            return RedirectToLocal(LoginHelper(entities, model.Email, model.Password));



                        }
                    }
                    catch (Exception ex)
                    {
                    }

                }

            }

            PrepRegisterationModel(model, model.Country, model.State, model.City);

            ModelState.AddModelError("", "Please fill all the fields correctly");

            return View(model);

        }





        [Route("SignUp/Worker")]
        [AllowAnonymous]
        [HttpGet]

        public ActionResult SignUpWorker()
        {
            AdminNWorkerViewModel model = new AdminNWorkerViewModel();

            return View(PrepRegisterationModel(model));
        }


        [AllowAnonymous]
        [HttpPost]

        public ActionResult SignUpWorker(AdminNWorkerViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (DbVMSEntities entities = new DbVMSEntities())
                {
                    User user = getUser(model, int.Parse(ConfigurationManager.AppSettings["Worker"]))
;
                    Worker worker = new Worker();
                    worker.FirstName = model.FirstName;
                    worker.MiddleName = model.MiddleName;
                    worker.LastName = model.LastName;
                    worker.Status = 1;
                    user.Workers.Add(worker);
                    entities.Users.Add(user);
                    try
                    {
                        int i = entities.SaveChanges();
                        if (i > 0)
                        {
                            return RedirectToLocal(LoginHelper(entities, model.Email, model.Password));



                        }
                    }
                    catch (Exception ex)
                    {
                    }

                }

            }

            PrepRegisterationModel(model, model.Country, model.State, model.City);

            ModelState.AddModelError("", "Please fill all the fields correctly");

            return View(model);

        }



        [Route("SignUp/Admin")]
        [AllowAnonymous]
        [HttpGet]

        public ActionResult SignUpAdmin()
        {
            AdminNWorkerViewModel model = new AdminNWorkerViewModel();

            return View("SignUpWorker", PrepRegisterationModel(model));
        }


        [AllowAnonymous]
        [HttpPost]

        public ActionResult SignUpAdmin(AdminNWorkerViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (DbVMSEntities entities = new DbVMSEntities())
                {
                    User user = getUser(model, int.Parse(ConfigurationManager.AppSettings["Admin"]))
;
                    Admin worker = new Admin();
                    worker.FirstName = model.FirstName;
                    worker.MiddleName = model.MiddleName;
                    worker.LastName = model.LastName;
                    user.Admins.Add(worker);
                    entities.Users.Add(user);
                    try
                    {
                        int i = entities.SaveChanges();
                        if (i > 0)
                        {
                            return RedirectToLocal(LoginHelper(entities, model.Email, model.Password));



                        }
                    }
                    catch (Exception ex)
                    {
                    }

                }

            }

            PrepRegisterationModel(model, model.Country, model.State, model.City);

            ModelState.AddModelError("", "Please fill all the fields correctly");

            return View(model);

        }

        #region Helpers


        private User getUser(UserViewModel model, int type) {

            User user = new User();
            user.EmailId = model.Email;
            user.Password = model.Password;
            user.Country = model.Country;
            user.State = model.State;
            user.City = string.IsNullOrEmpty(model.City) ? "" : model.City;
            user.PinCode = model.PinCode;
            user.Address = model.Address;
            user.UserType = type;

            return user;    
        
        }
        private string LoginHelper(DbVMSEntities entities, string EmailId, string Password)
        {

            string returnUrl = "";
            spValidateUserResult result = entities.spValidateUser(EmailId, Password).FirstOrDefault();
            if (result != null)
            {
                User user = entities.Users.FirstOrDefault(u => u.Id == result.UserId);
                HttpCookie c = new HttpCookie("AuthToken");
                c.Value = result.TokenResult.Value.ToString();
                c.Expires = DateTime.Now.AddHours(24);
                HttpCookie uc = new HttpCookie("UserId");
                uc.Value = result.UserId.ToString();
                Response.Cookies.Add(c);
                Response.Cookies.Add(uc);
                ViewBag.UserType = user.UserType;
                if (user.UserType == int.Parse(ConfigurationManager.AppSettings["Organization"]))
                {
                    returnUrl = "/Organization/Index";
                    
                    FormsAuthentication.SetAuthCookie(user.VolunteerOrganizations.FirstOrDefault().Name, true);
                }
                else if (user.UserType == int.Parse(ConfigurationManager.AppSettings["Worker"]))
                {
                    returnUrl = "/Worker/Index";
                    FormsAuthentication.SetAuthCookie(user.Workers.FirstOrDefault().FirstName, true);
                }
                else if (user.UserType == int.Parse(ConfigurationManager.AppSettings["Admin"]))
                {
                    returnUrl = "/Admin/Index";
                    FormsAuthentication.SetAuthCookie(user.Admins.FirstOrDefault().FirstName, true);
                }


                return returnUrl;

            }
            return returnUrl;


        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private UserViewModel PrepRegisterationModel(UserViewModel model, string country = "", string state = "", string city = "")
        {
            string token = CountryStateCityHelper.getAccessToken();
            model.Countries = CountryStateCityHelper.getCountries(token);
            model.States = string.IsNullOrEmpty(country) ? new List<SelectListItem>() : CountryStateCityHelper.getStates(country, token);
            model.Cities = string.IsNullOrEmpty(state) ? new List<SelectListItem>() : CountryStateCityHelper.getcities(state, token);

            return model;
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult GetStates(string country)
        {
            string token = CountryStateCityHelper.getAccessToken();
            var states = CountryStateCityHelper.getStates(country, token);
            return Json(states, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult GetCities(string state)
        {
            string token = CountryStateCityHelper.getAccessToken();
            var cities = CountryStateCityHelper.getcities(state, token);
            return Json(cities, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
