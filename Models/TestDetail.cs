using System;
using System.Collections.Generic;

#nullable disable

namespace PopIt.Models
{
    public partial class TestDetail
    {
        public TestDetail()
        {
            TestRules = new HashSet<TestRule>();
        }

        public int TestId { get; set; }
        public string TestName { get; set; }
        public string SubjectName { get; set; }
        public int GradeId { get; set; }

        public virtual ICollection<TestRule> TestRules { get; set; }
    }
}
