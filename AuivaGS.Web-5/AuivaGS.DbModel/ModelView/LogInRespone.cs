using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuviaGS.DbModel.ModelView
{
    public class LogInRespone
    {
        public string Email { get; set; }

        public string Token { get; set; }

        public bool IsComplete { get; set; }

        public bool IsArchitect { get; set; }
    }
}
