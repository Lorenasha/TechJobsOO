using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.ViewModels;
using TechJobs.Models;
using System.Collections.Generic;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            // TODO #1 - get the Job with the given ID and pass it into the view
            JobData theJobs = JobData.GetInstance();
            Job aJob = theJobs.Find(id);
            return View(aJob);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            // TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.

            if (ModelState.IsValid)
            {
                JobData theJobs = JobData.GetInstance();
                Job myJob = new Job
                {
                    Name = newJobViewModel.Name,
                    Employer = theJobs.Employers.Find(newJobViewModel.EmployerID),
                    Location = theJobs.Locations.Find(newJobViewModel.LocationID),
                    CoreCompetency = theJobs.CoreCompetencies.Find(newJobViewModel.CoreCompetencyID),
                    PositionType = theJobs.PositionTypes.Find(newJobViewModel.PositionTypeID)
                };

                jobData.Jobs.Add(myJob);

                string url = "/Job?id=" + myJob.ID;
                return Redirect(url);

            }

            return View(newJobViewModel);

        }
    }
}
