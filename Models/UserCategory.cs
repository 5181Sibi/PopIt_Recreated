using System;
using System.Collections.Generic;

#nullable disable

namespace PopIt.Models
{
    public partial class UserCategory
    {
        public UserCategory()
        {
            AdminLogins = new HashSet<AdminLogin>();
            StudentDetails = new HashSet<StudentDetail>();
            TeacherDetails = new HashSet<TeacherDetail>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<AdminLogin> AdminLogins { get; set; }
        public virtual ICollection<StudentDetail> StudentDetails { get; set; }
        public virtual ICollection<TeacherDetail> TeacherDetails { get; set; }
    }
}
