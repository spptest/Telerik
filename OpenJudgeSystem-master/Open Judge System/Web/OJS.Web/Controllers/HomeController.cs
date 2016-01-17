﻿namespace OJS.Web.Controllers
{
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;

    using OJS.Data;
    using OJS.Web.ViewModels.Home.Index;

    public class HomeController : BaseController
    {
        public HomeController(IOjsData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var indexViewModel = new IndexViewModel
            {
                ActiveContests = this.Data.Contests.AllActive()
                    .OrderByDescending(x => x.StartTime)
                    .Select(HomeContestViewModel.FromContest)
                    .ToList(),
                FutureContests = this.Data.Contests.AllFuture()
                    .OrderBy(x => x.StartTime)
                    .Select(HomeContestViewModel.FromContest)
                    .ToList(),
                PastContests = this.Data.Contests.AllPast()
                    .OrderByDescending(x => x.StartTime)
                    .Select(HomeContestViewModel.FromContest)
                    .Take(5)
                    .ToList()
            };

            return this.View(indexViewModel);
        }

        /// <summary>
        /// Gets the robots.txt file.
        /// </summary>
        /// <returns>Returns a robots.txt file.</returns>
        [HttpGet]
        [OutputCache(Duration = 3600)]
        public FileResult RobotsTxt()
        {
            var robotsTxtContent = new StringBuilder();
            robotsTxtContent.AppendLine("User-Agent: *");
            robotsTxtContent.AppendLine("Allow: /");

            return this.File(Encoding.ASCII.GetBytes(robotsTxtContent.ToString()), "text/plain");
        }
    }
}