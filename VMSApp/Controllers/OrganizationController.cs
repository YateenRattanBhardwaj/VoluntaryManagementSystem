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
                    VolunteerOrganization vo = entities.VolunteerOrganizations.FirstOrDefault(o => o.UserId == orgUserid);
                    if (vo != null)
                    {
                        OrganizationViewModel ovm = new OrganizationViewModel();
                        ovm.Activities = new List<ActivityViewModel>();
                        ovm.Status = vo.Status.Value;
                        ovm.Jobs = new List<JobViewModel>();
                        ovm.Shifts = new List<ShiftViewModel>();
                        var orgJobs = vo.VolunteerActivities.Select(x => x.VolunteerActivityJobs);
                        foreach (VolunteerActivity act in vo.VolunteerActivities)
                        {
                            ActivityViewModel avm = new ActivityViewModel();
                            avm.Id = act.ActivityId;
                            avm.Name = act.Name;
                            avm.Description = act.Description;
                            avm.StartDateTime = act.StartDateTime.Value;
                            avm.EndDateTime = act.EndDateTime.Value;
                            avm.Jobs = new List<JobViewModel>();


                            foreach(VolunteerActivityJob ajob in act.VolunteerActivityJobs)
                            {
                                JobViewModel jvm = new JobViewModel();
                                jvm.Id = ajob.JobId;
                                jvm.Name = ajob.Name;
                                jvm.Description = ajob.Description;
                                jvm.Shifts = new List<ShiftViewModel>();
                                avm.Jobs.Add(jvm);

                                foreach (VolunteerShift ajs in ajob.VolunteerShifts) 
                                {
                                    ShiftViewModel svm = new ShiftViewModel();
                                    svm.Id = ajs.ShiftId;
                                    svm.Name=ajs.ShitName;
                                    svm.StartDateTime = ajs.StartDateTime;
                                    svm.StopDateTime = ajs.StopDateTime;
                                    svm.Description= ajs.Description;
                                    jvm.Shifts.Add(svm);
                                }
                            }
 
                            ovm.Activities.Add(avm);
                        }
                        
                        return View(ovm);
                    }
                }
            }
            return RedirectToAction("Login", "Account");
        }

    }
}
