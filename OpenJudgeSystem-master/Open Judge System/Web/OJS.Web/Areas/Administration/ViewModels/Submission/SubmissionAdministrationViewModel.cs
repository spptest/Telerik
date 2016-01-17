﻿namespace OJS.Web.Areas.Administration.ViewModels.Submission
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    using System.Web;
    using System.Web.Mvc;

    using OJS.Common.DataAnnotations;
    using OJS.Common.Extensions;
    using OJS.Data.Models;
    using OJS.Web.Areas.Administration.ViewModels.Common;

    using Resource = Resources.Areas.Administration.Submissions.ViewModels.SubmissionAdministration;

    public class SubmissionAdministrationViewModel : AdministrationViewModel<Submission>
    {
        private HttpPostedFileBase fileSubmission;

        [ExcludeFromExcel]
        public static Expression<Func<Submission, SubmissionAdministrationViewModel>> ViewModel
        {
            get
            {
                return sub => new SubmissionAdministrationViewModel
                {
                    Id = sub.Id,
                    ProblemId = sub.ProblemId,
                    ParticipantId = sub.ParticipantId,
                    SubmissionTypeId = sub.SubmissionTypeId,
                    AllowBinaryFilesUpload = sub.SubmissionType.AllowBinaryFilesUpload,
                    Content = sub.Content,
                    FileExtension = sub.FileExtension,
                    CreatedOn = sub.CreatedOn,
                    ModifiedOn = sub.ModifiedOn,
                };
            }
        }

        [DatabaseProperty]
        [Display(Name = "№")]
        [DefaultValue(null)]
        [HiddenInput(DisplayValue = false)]
        public int? Id { get; set; }

        [DatabaseProperty]
        [Display(Name = "Problem", ResourceType = typeof(Resource))]
        [Required(
            ErrorMessageResourceName = "Problem_required",
            ErrorMessageResourceType = typeof(Resource))]
        [UIHint("ProblemComboBox")]
        public int? ProblemId { get; set; }

        [DatabaseProperty]
        [Display(Name = "Participant", ResourceType = typeof(Resource))]
        [Required(
            ErrorMessageResourceName = "Participant_required",
            ErrorMessageResourceType = typeof(Resource))]
        [UIHint("ParticipantDropDownList")]
        public int? ParticipantId { get; set; }

        [DatabaseProperty]
        [Display(Name = "Type", ResourceType = typeof(Resource))]
        [Required(
            ErrorMessageResourceName = "Type_required",
            ErrorMessageResourceType = typeof(Resource))]
        [UIHint("SubmissionTypesDropDownList")]
        public int? SubmissionTypeId { get; set; }

        public bool? AllowBinaryFilesUpload { get; set; }

        [DatabaseProperty]
        [ScaffoldColumn(false)]
        [Required(
            ErrorMessageResourceName = "Content_required",
            ErrorMessageResourceType = typeof(Resource))]
        public byte[] Content { get; set; }

        [AllowHtml]
        [Display(Name = "Content_as_string", ResourceType = typeof(Resource))]
        [UIHint("MultiLineText")]
        public string ContentAsString
        {
            get
            {
                if (this.AllowBinaryFilesUpload.HasValue && !this.AllowBinaryFilesUpload.Value)
                {
                    return this.Content.Decompress();
                }

                return null;
            }

            set
            {
                this.Content = value.Compress();
            }
        }

        [Display(Name = "File_submission", ResourceType = typeof(Resource))]
        [ScaffoldColumn(false)]
        public HttpPostedFileBase FileSubmission
        {
            get
            {
                return this.fileSubmission;
            }

            set
            {
                this.fileSubmission = value;
                this.Content = value.InputStream.ToByteArray();
                this.FileExtension = value.FileName.GetFileExtension();
            }
        }

        [DatabaseProperty]
        public string FileExtension { get; set; }
    }
}