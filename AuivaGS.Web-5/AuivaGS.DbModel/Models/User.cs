using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class User
    {
        public User()
        {
            Galleries = new HashSet<Gallery>();
            UserEducations = new HashSet<UserEducation>();
            UserExperiences = new HashSet<UserExperience>();
            UserLanguages = new HashSet<UserLanguage>();
            UserSkills = new HashSet<UserSkill>();
        }

        public int Id { get; set; }
        public string LastNameUser { get; set; } = null!;
        public string FirstNameUser { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Descirption { get; set; } = null!;
        public string? Image { get; set; }
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public bool IsArchitect { get; set; }
        public int ArchitectureID { get; set; }
        public string ConfirmationCode { get; set; } = null!;
        public bool IsConfirmed { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? ConfirmaitonDateDue { get; set; }

        public virtual ICollection<Gallery> Galleries { get; set; }
        public virtual ICollection<UserEducation> UserEducations { get; set; }
        public virtual ICollection<UserExperience> UserExperiences { get; set; }
        public virtual ICollection<UserLanguage> UserLanguages { get; set; }
        public virtual ICollection<UserSkill> UserSkills { get; set; }
    }
}
