using System;
using System.Collections.Generic;

#nullable disable

namespace PopIt.Models
{
    public partial class TestRule
    {
        public TestRule()
        {
            Questions = new HashSet<Question>();
        }

        public int GradeId { get; set; }
        public string Rules { get; set; }
        public int TotalMark { get; set; }
        public double PassMark { get; set; }
        public int Examduration { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public int? TestId { get; set; }

        public virtual TestDetail Test { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
