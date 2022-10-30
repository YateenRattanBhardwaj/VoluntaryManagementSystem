using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VMSApp.Models;
using VMSApp.ViewModels;

namespace VMSApp.Controllers
{
    [Authorize]
    public class OrganizationController : Controller
    {
        //
        // GET: /Organization/

        public ActionResult Index()
        {
            HttpCookie c = Request.Cookies["UserId"];
            if (c != null) 
            {
                int orgUserid = int.Parse(c.Value);
                using (DbVMSEntities entities = new DbVMSEntities())
                {
                    VolunteerOrganization vo=entities.VolunteerOrganizations.FirstOrDefault(o => o.UserId == orgUserid);
                    if (vo != null) 
                    {
                        OrganizationViewModel ovm = new OrganizationViewModel();
                        ovm.Activities = new List<ActivityViewModel>();
                        ovm.Status = vo.Status.Value;
                        foreach (VolunteerActivity act in vo.VolunteerActivities) 
                        {
                            ActivityViewModel avm = new ActivityViewModel();
                            avm.Name = act.Name;
                            avm.Description = act.Description;
                            avm.StartDateTime = act.StartDateTime.Value;
                            avm.EndDateTime = act.EndDateTime.Value;
                            avm.Jobs = new List<JobViewModel>();
                            ovm.Activities.Add(avm);
                        }
                        return View(ovm);
                    }
                }
            }
            return RedirectToAction("Login","Account");
        }

    }
}
