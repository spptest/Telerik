﻿namespace OJS.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using OJS.Data.Contracts;

    public class Tag : DeletableEntity
    {
        private ICollection<Problem> problems;

        public Tag()
        {
            this.problems = new HashSet<Problem>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ForegroundColor { get; set; }

        public string BackgroundColor { get; set; }

        public virtual ICollection<Problem> Problems
        {
            get { return this.problems; }
            set { this.problems = value; }
        }
    }
}
