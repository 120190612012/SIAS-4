using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuivaGS.DbModel.ModelView
{
    public class GetAllProjectsDTO
    {
        public int ProjectID { get; set; }
        public string ImageString { get; set; } = null!;
        public string ProjectName { get; set; } = null!;
        public string ProjectDescption { get; set; } = null!;

        public int idUser { get; set; }
        public string CreatorName { get; set; } = null!;

        public string UserImage { get; set; } = null!;

        public DateTime? CreateDate { get; set; }
    }
}
