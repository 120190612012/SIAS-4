using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuivaGS.DbModel.ModelView.ArtDTOS
{
    public class AddArtDto
    {
        public string Name { get; set; } = null!;

        public List<string> ArtTyps { get; set; } = null!;

        public List<string> Images { get; set; } = null!;
    }
}
