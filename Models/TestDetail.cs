using System;
using System.Collections.Generic;

#nullable disable

namespace PopIt.Models
{
    public partial class TestDetail
    {
        public TestDetail()
        {
            Questions = new HashSet<Question>();
            Results = new HashSet<Result>();
            TestRules = new HashSet<TestRule>();
        }

        public int TestId { get; set; }
        public string TestName { get; set; }
        public int SubjectId { get; set; }
        public int GradeId { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Result> Results { get; set; }
        public virtual ICollection<TestRule> TestRules { get; set; }
    }
}
