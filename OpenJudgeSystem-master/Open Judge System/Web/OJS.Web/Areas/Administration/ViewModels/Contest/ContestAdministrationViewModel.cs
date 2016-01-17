﻿namespace OJS.Web.Areas.Administration.ViewModels.Contest
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using OJS.Common;
    using OJS.Common.DataAnnotations;
    using OJS.Data.Models;
    using OJS.Web.Areas.Administration.ViewModels.Common;
    using OJS.Web.Areas.Administration.ViewModels.SubmissionType;

    using Resource = Resources.Areas.Administration.Contests.ViewModels.ContestAdministration;

    public class ContestAdministrationViewModel : AdministrationViewModel<Contest>
    {
        public ContestAdministrationViewModel()
        {
            this.SubmisstionTypes = new List<SubmissionTypeViewModel>();
        }

        [ExcludeFromExcel]
        public static Expression<Func<Contest, ContestAdministrationViewModel>> ViewModel
        {
            get
            {
                return contest => new ContestAdministrationViewModel
                {
                    Id = contest.Id,
                    Name = contest.Name,
                    StartTime = contest.StartTime,
                    EndTime = contest.EndTime,
                    PracticeStartTime = contest.PracticeStartTime,
                    PracticeEndTime = contest.PracticeEndTime,
                    IsVisible = contest.IsVisible,
                    CategoryId = contest.CategoryId.Value,
                    ContestPassword = contest.ContestPassword,
                    PracticePassword = contest.PracticePassword,
                    Description = contest.Description,
                    LimitBetweenSubmissions = contest.LimitBetweenSubmissions,
                    OrderBy = contest.OrderBy,
                    SelectedSubmissionTypes = contest.SubmissionTypes.AsQueryable().Select(SubmissionTypeViewModel.ViewModel),
                    CreatedOn = contest.CreatedOn,
                    ModifiedOn = contest.ModifiedOn,
                };
            }
        }

        [DatabaseProperty]
        [Display(Name = "№")]
        [DefaultValue(null)]
        [HiddenInput(DisplayValue = false)]
        public int? Id { get; set; }

        [DatabaseProperty]
        [Display(Name = "Name", ResourceType = typeof(Resource))]
        [Required(
            ErrorMessageResourceName = "Name_required",
            ErrorMessageResourceType = typeof(Resource))]
        [StringLength(
            GlobalConstants.ContestNameMaxLength,
            MinimumLength = GlobalConstants.ContestNameMinLength,
            ErrorMessageResourceName = "Name_length",
            ErrorMessageResourceType = typeof(Resource))]
        [UIHint("SingleLineText")]
        public string Name { get; set; }

        [DatabaseProperty]
        [Display(Name = "Start_time", ResourceType = typeof(Resource))]
        [UIHint("DateAndTime")]
        public DateTime? StartTime { get; set; }

        [DatabaseProperty]
        [Display(Name = "End_time", ResourceType = typeof(Resource))]
        [UIHint("DateAndTime")]
        public DateTime? EndTime { get; set; }

        [DatabaseProperty]
        [Display(Name = "Practice_start_time", ResourceType = typeof(Resource))]
        [UIHint("DateAndTime")]
        public DateTime? PracticeStartTime { get; set; }

        [DatabaseProperty]
        [Display(Name = "Practice_end_time", ResourceType = typeof(Resource))]
        [UIHint("DateAndTime")]
        public DateTime? PracticeEndTime { get; set; }

        [DatabaseProperty]
        [Display(Name = "Contest_password", ResourceType = typeof(Resource))]
        [UIHint("SingleLineText")]
        public string ContestPassword { get; set; }

        [DatabaseProperty]
        [Display(Name = "Practice_password", ResourceType = typeof(Resource))]
        [UIHint("SingleLineText")]
        public string PracticePassword { get; set; }

        [DatabaseProperty]
        [Display(Name = "Description", ResourceType = typeof(Resource))]
        [UIHint("MultiLineText")]
        public string Description { get; set; }

        [DatabaseProperty]
        [Display(Name = "Submissions_limit", ResourceType = typeof(Resource))]
        [UIHint("PositiveInteger")]
        [DefaultValue(0)]
        public int LimitBetweenSubmissions { get; set; }

        [DatabaseProperty]
        [Display(Name = "Order", ResourceType = typeof(Resource))]
        [Required(
            ErrorMessageResourceName = "Order_required",
            ErrorMessageResourceType = typeof(Resource))]
        [UIHint("Integer")]
        public int OrderBy { get; set; }

        [DatabaseProperty]
        [Display(Name = "Visibility", ResourceType = typeof(Resource))]
        public bool IsVisible { get; set; }

        [DatabaseProperty]
        [Display(Name = "Category", ResourceType = typeof(Resource))]
        [Required(
            ErrorMessageResourceName = "Category_required",
            ErrorMessageResourceType = typeof(Resource))]
        [UIHint("CategoryDropDown")]
        [DefaultValue(null)]
        public int? CategoryId { get; set; }

        [Display(Name = "Submision_types", ResourceType = typeof(Resource))]
        [ExcludeFromExcel]
        [UIHint("SubmissionTypeCheckBoxes")]
        public IList<SubmissionTypeViewModel> SubmisstionTypes { get; set; }

        [ExcludeFromExcel]
        public IEnumerable<SubmissionTypeViewModel> SelectedSubmissionTypes { get; set; }
    }
}