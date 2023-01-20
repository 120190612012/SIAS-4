using AuivaGS.Core.Mangers.MangerInterfaces;
using AuivaGS.DbModel.ModelView.ArtDTOS;
using AuviaGS.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuivaGS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ArtController : ApiBaseController
    {
        private readonly IArtManger _artManger;

        public ArtController(IArtManger artManger)
        {
            _artManger = artManger;
        }
        [Route("AddArt")]
        [HttpPost]
        public IActionResult AddArt(AddArtDto newArt)
        {
            _artManger.AddArtAsync(newArt, LoggedInUser);

            return Ok(new { Message = "Done" });
        }


    }
}
