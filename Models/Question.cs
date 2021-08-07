using System;
using System.Collections.Generic;

#nullable disable

namespace PopIt.Models
{
    public partial class Question
    {
        public int QuestionId { get; set; }
        public int QuestionSetNo { get; set; }
        public int RuleId { get; set; }
        public string Question1 { get; set; }
        public byte[] QuestionImage { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string CorrectAnswer { get; set; }
        public string Description { get; set; }
        public double Mark { get; set; }

        public virtual TestRule Rule { get; set; }
    }
}
