using AuivaGS.DbModel.ModelView.ArtDTOS;
using AuviaGS.DbModel.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuivaGS.Core.Mangers.MangerInterfaces
{
    public interface IArtManger
    {
        public Task AddArtAsync(AddArtDto newArt, UserModel loggedInUser);
    }
}
