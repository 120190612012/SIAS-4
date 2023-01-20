using AuivaGS.Core.Mangers.MangerInterfaces;
using AuivaGS.DbModel.ModelView;
using AuviaGS.Controllers;
using AuviaGS.DbModel.ModelView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuivaGS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ApiBaseController
    {
        private readonly IProjectsManger _projectMager;

        public ProjectsController(IProjectsManger projectMager)
        {
            _projectMager = projectMager;
        }

        [Route("AddProject")]
        [HttpPost]
        public async Task<IActionResult> AddProject(AddProejctView addProjectView)
        {
            await _projectMager.AddProject(addProjectView, LoggedInUser);

            return Ok(new AddProejctView());
        }



        [HttpGet("GetProjectTools")]
        public IActionResult GetProjectTools()
        {
            var toolsDTOs = _projectMager.GetProjectTools();

            return Ok(toolsDTOs);
        }

        [HttpGet("GetProjectCategory")]
        public IActionResult GetProjectCategory()
        {
            var categoryDTOs = _projectMager.GetProjectCategory();

            return Ok(categoryDTOs);
        }

        [HttpGet("GetAllProjects")]
        public IActionResult GetAllProjects()
        {
            var getAllProjectsDTOs = _projectMager.GetAllProjects();

            return Ok(getAllProjectsDTOs);
        }


        [HttpDelete("DeleteProject")]
        public IActionResult DeleteProject(int Id)
        {
            _projectMager.DeleteProject(LoggedInUser, Id);

            return Ok(new { Message = "Done" });
        }

        [HttpGet("GetAllProjectsForUser")]
        public IActionResult GetAllProjectsForUser()
        {
            var res = _projectMager.GetAllProjectsForUser(LoggedInUser);

            return Ok(res);
        }
        [HttpGet("GetProjectDetalisById")]
        public IActionResult GetProjectDetalisById(int Id)
        {
            var res = _projectMager.GetProjectDetalisById(Id);

            return Ok(res);
        }

    }
}
