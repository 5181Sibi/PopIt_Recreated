using System;
using System.Collections.Generic;

#nullable disable

namespace PopIt.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Results = new HashSet<Result>();
            StudentTeachers = new HashSet<StudentTeacher>();
        }

        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int GradeId { get; set; }

        public virtual ICollection<Result> Results { get; set; }
        public virtual ICollection<StudentTeacher> StudentTeachers { get; set; }
    }
}
