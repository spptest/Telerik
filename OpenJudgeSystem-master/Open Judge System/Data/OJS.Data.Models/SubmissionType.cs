﻿namespace OJS.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using OJS.Common;
    using OJS.Common.Extensions;
    using OJS.Common.Models;

    public class SubmissionType
    {
        private ICollection<Contest> contests;

        public SubmissionType()
        {
            this.contests = new HashSet<Contest>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(GlobalConstants.SubmissionTypeNameMaxLength)]
        [MinLength(GlobalConstants.SubmissionTypeNameMinLength)]
        public string Name { get; set; }

        [DefaultValue(false)]
        public bool IsSelectedByDefault { get; set; }

        public ExecutionStrategyType ExecutionStrategyType { get; set; }

        public CompilerType CompilerType { get; set; }

        public string AdditionalCompilerArguments { get; set; }

        public string Description { get; set; }

        [DefaultValue(false)]
        public bool AllowBinaryFilesUpload { get; set; }

        /// <summary>
        /// Comma-separated list of allowed file extensions.
        /// If the value is null or whitespace then only text values are allowed. If any extension is specified then no text input is allowed.
        /// </summary>
        public string AllowedFileExtensions { get; set; }

        [NotMapped]
        public IEnumerable<string> AllowedFileExtensionsList
        {
            get
            {
                var list =
                    this.AllowedFileExtensions.Split(new[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => x.Trim());
                return list.ToArray();
            }
        }

        public virtual ICollection<Contest> Contests
        {
            get { return this.contests; }
            set { this.contests = value; }
        }

        [NotMapped]
        public string FileNameExtension
        {
            get
            {
                string extension = (this.ExecutionStrategyType.GetFileExtension()
                                    ?? this.CompilerType.GetFileExtension()) ?? string.Empty;

                return extension;
            }
        }
    }
}
