using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuivaGS.DbModel.ModelView
{
    public class GetProjectDetalisById
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public decimal? SpaceWidth { get; set; }
        public decimal? SpaceHeight { get; set; }
        public string? Descirotion { get; set; }
        public DateTime? CreatedDateUTC { get; set; }

        public List<string> ProjectTools { get; set; } = null!;
        public List<string> ProejctCatoraoy { get; set; } = null!;
        public List<string> Images { get; set; } = null!;
    }
}
