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
            ViewBag.ReturnUrl = returnUrl;
            return View();
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
                    //entities.sp
                    spValidateUserResult result = entities.spValidateUser(model.EmailId, model.Password).FirstOrDefault();
                    if (result != null)
                    {
                        User user = entities.Users.FirstOrDefault(u => u.Id == result.UserId);
                        HttpCookie c = new HttpCookie("AuthToken");
                        c.Value = result.TokenResult.Value.ToString();
                        c.Expires = DateTime.Now.AddHours(24);
                        Response.Cookies.Add(c);
                        if (user.usertype1.Id == int.Parse(ConfigurationManager.AppSettings["Organization"]))
                        {
                            returnUrl = "/Organization/Index";

                            FormsAuthentication.SetAuthCookie(user.VolunteerOrganizations.FirstOrDefault().Name, true);
                        }
                        else if (user.usertype1.Id == int.Parse(ConfigurationManager.AppSettings["Worker"]))
                        {
                            returnUrl = "/Worker/Index";
                            FormsAuthentication.SetAuthCookie(user.Workers.FirstOrDefault().FirstName, true);
                        }
                        else if (user.usertype1.Id == int.Parse(ConfigurationManager.AppSettings["Admin"]))
                        {
                            returnUrl = "/Admin/Index";
                            FormsAuthentication.SetAuthCookie(user.Admins.FirstOrDefault().FirstName, true);
                        }


                        return RedirectToLocal(returnUrl);

                    }
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

            return RedirectToAction("Index", "Home");
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
                    User user = new User();
                    user.EmailId = model.Email;
                    user.Password = model.Password;
                    user.Country = model.Country;
                    user.State = model.State;
                    user.City = string.IsNullOrEmpty(model.City) ? "" : model.City;
                    user.PinCode = model.PinCode;

                    VolunteerOrganization org = new VolunteerOrganization();
                    org.Description = model.Description;
                    org.Name = model.Name;
                    org.Phone = model.Phone;
                    org.Website = model.Website;
                    user.VolunteerOrganizations.Add(org);
                    entities.Users.Add(user);
                    entities.SaveChanges();
 
                }

            }

            PrepRegisterationModel(model, model.Country, model.State, model.City);

            ModelState.AddModelError("", "Please fill all the fields correctly");

            return View(model);

        }





        [Route("SignUp/Worker")]
        [AllowAnonymous]
        public ActionResult SignUpWorker()
        {
            OrganizationViewModel model = new OrganizationViewModel();

            return View(PrepRegisterationModel(model));
        }

        #region Helpers
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

        private OrganizationViewModel PrepRegisterationModel(OrganizationViewModel model, string country = "", string state = "", string city = "")
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
