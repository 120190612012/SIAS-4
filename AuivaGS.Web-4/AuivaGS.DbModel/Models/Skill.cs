using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class Skill
    {
        public Skill()
        {
            UserSkills = new HashSet<UserSkill>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<UserSkill> UserSkills { get; set; }
    }
}
