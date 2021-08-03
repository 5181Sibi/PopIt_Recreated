using System;
using System.Collections.Generic;

#nullable disable

namespace PopIt.Models
{
    public partial class StudentTeacher
    {
        public int StudentTeacherId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }

        public virtual StudentDetail Student { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual TeacherDetail Teacher { get; set; }
    }
}
