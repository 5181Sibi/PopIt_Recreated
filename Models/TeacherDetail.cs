using System;
using System.Collections.Generic;

#nullable disable

namespace PopIt.Models
{
    public partial class TeacherDetail
    {
        public TeacherDetail()
        {
            StudentTeachers = new HashSet<StudentTeacher>();
        }

        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string Emailid { get; set; }
        public string StaffNo { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int CategoryId { get; set; }
        public string SubjectName { get; set; }
        public string Experience { get; set; }
        public string PhoneNumber { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime Lastlogin { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual UserCategory Category { get; set; }
        public virtual ICollection<StudentTeacher> StudentTeachers { get; set; }
    }
}
