﻿namespace OJS.Web.Areas.Administration.ViewModels.ContestQuestion
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using OJS.Common;
    using OJS.Common.DataAnnotations;
    using OJS.Common.Models;
    using OJS.Data.Models;
    using OJS.Web.Areas.Administration.ViewModels.Common;

    using Resource = Resources.Areas.Administration.Contests.ViewModels.ContestQuestion;

    public class ContestQuestionViewModel : AdministrationViewModel<ContestQuestion>
    {
        [ExcludeFromExcel]
        public static Expression<Func<ContestQuestion, ContestQuestionViewModel>> ViewModel
        {
            get
            {
                return question => new ContestQuestionViewModel
                {
                    QuestionId = question.Id,
                    ContestId = question.ContestId,
                    Text = question.Text,
                    AskOfficialParticipants = question.AskOfficialParticipants,
                    AskPracticeParticipants = question.AskPracticeParticipants,
                    Type = question.Type,
                    RegularExpressionValidation = question.RegularExpressionValidation,
                    CreatedOn = question.CreatedOn,
                    ModifiedOn = question.ModifiedOn,
                };
            }
        }

        [DatabaseProperty(Name = "Id")]
        [DefaultValue(null)]
        [Display(Name = "№")]
        [HiddenInput(DisplayValue = false)]
        public int? QuestionId { get; set; }

        [DatabaseProperty]
        [HiddenInput(DisplayValue = false)]
        public int? ContestId { get; set; }

        [DatabaseProperty]
        [Display(Name = "Text", ResourceType = typeof(Resource))]
        [Required(
            AllowEmptyStrings = false,
            ErrorMessageResourceName = "Text_required",
            ErrorMessageResourceType = typeof(Resource))]
        [StringLength(
            GlobalConstants.ContestQuestionMaxLength,
            MinimumLength = GlobalConstants.ContestQuestionMinLength,
            ErrorMessageResourceName = "Text_length",
            ErrorMessageResourceType = typeof(Resource))]
        [UIHint("SingleLineText")]
        public string Text { get; set; }

        [DatabaseProperty]
        [Display(Name = "Question_type", ResourceType = typeof(Resource))]
        [UIHint("ContestQuestionType")]
        public ContestQuestionType Type { get; set; }

        [DatabaseProperty]
        [Display(Name = "Regex_validation", ResourceType = typeof(Resource))]
        [UIHint("SingleLineText")]
        public string RegularExpressionValidation { get; set; }

        [DatabaseProperty]
        [Display(Name = "Ask_in_contest", ResourceType = typeof(Resource))]
        [DefaultValue(true)]
        public bool AskOfficialParticipants { get; set; }

        [DatabaseProperty]
        [Display(Name = "Ask_in_practice", ResourceType = typeof(Resource))]
        [DefaultValue(true)]
        public bool AskPracticeParticipants { get; set; }
    }
}