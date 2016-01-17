﻿namespace OJS.Web.Areas.Contests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using OJS.Data;
    using OJS.Web.Areas.Contests.ViewModels.Contests;
    using OJS.Web.Areas.Contests.ViewModels.Submissions;
    using OJS.Web.Controllers;

    using Resource = Resources.Areas.Contests.ContestsGeneral;

    public class ListController : BaseController
    {
        public ListController(IOjsData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var contests = this.Data.Contests.All().Select(ContestViewModel.FromContest).ToList();
            return this.View(contests);
        }

        public ActionResult ReadCategories(int? id)
        {
            var categories =
                this.Data.ContestCategories.All()
                    .Where(x => x.IsVisible)
                    .Where(x => id.HasValue ? x.ParentId == id : x.ParentId == null)
                    .OrderBy(x => x.OrderBy)
                    .Select(ContestCategoryListViewModel.FromCategory);

            return this.Json(categories, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetParents(int id)
        {
            var categoryIds = new List<int>();
            var category = this.Data.ContestCategories.GetById(id);

            categoryIds.Add(category.Id);
            var parent = category.Parent;

            while (parent != null)
            {
                categoryIds.Add(parent.Id);
                parent = parent.Parent;
            }

            categoryIds.Reverse();

            return this.Json(categoryIds, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ByCategory(int? id)
        {
            ContestCategoryViewModel contestCategory;
            if (id.HasValue)
            {
                contestCategory =
                    this.Data.ContestCategories.All()
                        .Where(x => x.Id == id && !x.IsDeleted && x.IsVisible)
                        .OrderBy(x => x.OrderBy)
                        .Select(ContestCategoryViewModel.FromContestCategory)
                        .FirstOrDefault();
            }
            else
            {
                contestCategory = new ContestCategoryViewModel
                {
                    CategoryName = Resource.Main_categories,
                    Contests = new HashSet<ContestViewModel>(),
                    SubCategories = this.Data.ContestCategories.All()
                                        .Where(x => x.IsVisible && !x.IsDeleted && x.Parent == null)
                                        .Select(ContestCategoryListViewModel.FromCategory)
                };
            }

            if (contestCategory == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Resource.Category_not_found);
            }

            if (this.Request.IsAjaxRequest())
            {
                this.ViewBag.IsAjax = true;
                return this.PartialView(contestCategory);
            }

            this.ViewBag.IsAjax = false;
            return this.View(contestCategory);
        }

        public ActionResult BySubmissionType(int? id, string submissionTypeName)
        {
            SubmissionTypeViewModel submissionType;
            if (id.HasValue)
            {
                submissionType = this.Data.SubmissionTypes.All()
                                                  .Where(x => x.Id == id.Value)
                                                  .Select(SubmissionTypeViewModel.FromSubmissionType)
                                                  .FirstOrDefault();
            }
            else
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, Resource.Invalid_request);
            }

            if (submissionType == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Resource.Submission_type_not_found);
            }

            var contests =
                this.Data.Contests
                                .All()
                                .Where(c => !c.IsDeleted &&
                                            c.IsVisible &&
                                            c.SubmissionTypes.Any(s => s.Id == submissionType.Id))
                                .OrderBy(x => x.OrderBy)
                                .Select(ContestViewModel.FromContest);

            this.ViewBag.SubmissionType = submissionType.Name;
            return this.View(contests);
        }
    }
}