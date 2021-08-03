using System;
using System.Collections.Generic;

#nullable disable

namespace PopIt.Models
{
    public partial class Result
    {
        public int ResultId { get; set; }
        public int TestId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public double TotalMarks { get; set; }

        public virtual StudentDetail Student { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual TestDetail Test { get; set; }
    }
}
