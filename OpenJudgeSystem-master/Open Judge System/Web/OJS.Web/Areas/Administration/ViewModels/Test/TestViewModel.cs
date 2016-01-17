﻿namespace OJS.Web.Areas.Administration.ViewModels.Test
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;

    using OJS.Common;
    using OJS.Common.DataAnnotations;
    using OJS.Common.Extensions;
    using OJS.Data.Models;

    using Resource = Resources.Areas.Administration.Tests.ViewModels.TestAdministration;

    public class TestViewModel
    {
        [ExcludeFromExcel]
        public static Expression<Func<Test, TestViewModel>> FromTest
        {
            get
            {
                return test => new TestViewModel
                {
                    Id = test.Id,
                    InputData = test.InputData,
                    OutputData = test.OutputData,
                    IsTrialTest = test.IsTrialTest,
                    OrderBy = test.OrderBy,
                    ProblemId = test.Problem.Id,
                    ProblemName = test.Problem.Name,
                    TestRunsCount = test.TestRuns.Count
                };
            }
        }

        public int Id { get; set; }

        [Display(Name = "Problem_name", ResourceType = typeof(Resource))]
        public string ProblemName { get; set; }

        [Display(Name = "Input", ResourceType = typeof(Resource))]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Required(
            AllowEmptyStrings = false,
            ErrorMessageResourceName = "Input_required",
            ErrorMessageResourceType = typeof(Resource))]
        [StringLength(
            int.MaxValue,
            MinimumLength = GlobalConstants.TestInputMinLength)]
        public string Input
        {
            get
            {
                if (this.InputData == null)
                {
                    return string.Empty;
                }

                var result = this.InputData.Decompress();
                return result.Length > 20 ? result.Substring(0, 20) : result;
            }

            set
            {
                this.InputData = value.Compress();
            }
        }

        [Display(Name = "Input", ResourceType = typeof(Resource))]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Required(
            AllowEmptyStrings = false,
            ErrorMessageResourceName = "Input_required",
            ErrorMessageResourceType = typeof(Resource))]
        [ScriptIgnore]
        [StringLength(
            int.MaxValue,
            MinimumLength = GlobalConstants.TestInputMinLength)]
        public string InputFull
        {
            get
            {
                if (this.InputData == null)
                {
                    return string.Empty;
                }

                var result = this.InputData.Decompress();
                return result;
            }

            set
            {
                this.InputData = value.Compress();
            }
        }

        [Display(Name = "Output", ResourceType = typeof(Resource))]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Required(
            AllowEmptyStrings = false,
            ErrorMessageResourceName = "Output_required",
            ErrorMessageResourceType = typeof(Resource))]
        [StringLength(
            int.MaxValue,
            MinimumLength = GlobalConstants.TestOutputMinLength)]
        public string Output
        {
            get
            {
                if (this.OutputData == null)
                {
                    return string.Empty;
                }

                var result = this.OutputData.Decompress();
                return result.Length > 20 ? result.Substring(0, 20) : result;
            }

            set
            {
                this.OutputData = value.Compress();
            }
        }

        [Display(Name = "Output", ResourceType = typeof(Resource))]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Required(
            AllowEmptyStrings = false,
            ErrorMessageResourceName = "Output_required",
            ErrorMessageResourceType = typeof(Resource))]
        [ScriptIgnore]
        [StringLength(
            int.MaxValue,
            MinimumLength = GlobalConstants.TestOutputMinLength)]
        public string OutputFull
        {
            get
            {
                if (this.OutputData == null)
                {
                    return string.Empty;
                }

                var result = this.OutputData.Decompress();
                return result;
            }

            set
            {
                this.OutputData = value.Compress();
            }
        }

        [Display(Name = "Trial_test_name", ResourceType = typeof(Resource))]
        public string TrialTestName => this.IsTrialTest ? Resource.Practice : Resource.Contest;

        [Display(Name = "Trial_test_name", ResourceType = typeof(Resource))]
        public bool IsTrialTest { get; set; }

        [Display(Name = "Order", ResourceType = typeof(Resource))]
        [Required(
            ErrorMessageResourceName = "Order_required",
            ErrorMessageResourceType = typeof(Resource))]
        [RegularExpression(@"(0|[1-9]{1}[0-9]{0,8}|[1]{1}[0-9]{1,9}|[-]{1}[2]{1}([0]{1}[0-9]{8}|[1]{1}([0-3]{1}[0-9]{7}|[4]{1}([0-6]{1}[0-9]{6}|[7]{1}([0-3]{1}[0-9]{5}|[4]{1}([0-7]{1}[0-9]{4}|[8]{1}([0-2]{1}[0-9]{3}|[3]{1}([0-5]{1}[0-9]{2}|[6]{1}([0-3]{1}[0-9]{1}|[4]{1}[0-8]{1}))))))))|(\+)?[2]{1}([0]{1}[0-9]{8}|[1]{1}([0-3]{1}[0-9]{7}|[4]{1}([0-6]{1}[0-9]{6}|[7]{1}([0-3]{1}[0-9]{5}|[4]{1}([0-7]{1}[0-9]{4}|[8]{1}([0-2]{1}[0-9]{3}|[3]{1}([0-5]{1}[0-9]{2}|[6]{1}([0-3]{1}[0-9]{1}|[4]{1}[0-7]{1})))))))))")]
        public int OrderBy { get; set; }

        [Display(Name = "Problem_id", ResourceType = typeof(Resource))]
        public int ProblemId { get; set; }

        [Display(Name = "Test_runs_count", ResourceType = typeof(Resource))]
        public int TestRunsCount { get; set; }

        internal byte[] InputData { get; set; }

        internal byte[] OutputData { get; set; }
    }
}