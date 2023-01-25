using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class UserEducation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UniversityName { get; set; } = null!;
        public string FacultyName { get; set; } = null!;
        public int GraduationYear { get; set; }
        public string AcademicDegree { get; set; } = null!;
        public string Descrpiction { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}
