using System;
using System.Collections.Generic;

#nullable disable

namespace PopIt.Models
{
    public partial class Question
    {
        public int QuestionNumber { get; set; }
        public int TestId { get; set; }
        public int GradeId { get; set; }
        public string Question1 { get; set; }
        public byte[] QuestionImage { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public int Answer { get; set; }
        public string Description { get; set; }
        public double Mark { get; set; }

        public virtual TestRule Grade { get; set; }
        public virtual TestDetail Test { get; set; }
    }
}
