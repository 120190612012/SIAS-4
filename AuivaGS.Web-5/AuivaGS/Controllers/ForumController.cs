using AuivaGS.Core.Mangers.MangerInterfaces;
using AuivaGS.DbModel.MangerSP;
using AuivaGS.DbModel.ModelSP;
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
    public class ForumController : ApiBaseController
    {
        private readonly IStoredP _storedP;

        public ForumController(IStoredP IstoredP)
        {
            _storedP = IstoredP;
        }

        [Route("PostFourm")]
        [HttpPost]
        public async Task<IActionResult> PostFourm(PostForumRespones postForumRespones)
        {
            var m = await _storedP.PostFourmAsync(LoggedInUser.Id, postForumRespones.TitleForum , postForumRespones.DescriptionForum);

            return Ok(m);
        }
        [Route("GetForumIsID")]
        [HttpPost]
        public async Task<IActionResult> GetForumIsID(ResponeseFroumID responeseFroumID)
        {
            var m = await _storedP.GetForumIsIDAsync(LoggedInUser.Id, responeseFroumID.ForumID);

            return Ok(m);
        }
        [Route("GetForumIDUser")]
        [HttpGet]
        public async Task<IActionResult> GetForumIDUser()
        {
            var m = await _storedP.GetForumIDUserAsync(LoggedInUser.Id);

            return Ok(m);
        }

        [Route("GetCommentToPost")]
        [HttpPost]
        public async Task<IActionResult> GetCommentToPost(ResponeseFroumID responeseFroumID)
        {
            var m = await _storedP.GetCommentToPostAsync(LoggedInUser.Id, responeseFroumID.ForumID);

            return Ok(m);
        }
        [Route("GetAllForum")]
        [HttpGet]
        public async Task<IActionResult> GetAllForum()
        {
            var m = await _storedP.GetAllForumAsync(LoggedInUser.Id);

            return Ok(m);
        }
        [Route("AddCommitForum")]
        [HttpPost]
        public async Task<IActionResult> AddCommitForum(AddCommitForumResponce addCommitForumResponce)
        {
            var m = await _storedP.AddCommitForumAsync(LoggedInUser.Id , addCommitForumResponce.ForumID, addCommitForumResponce.CommitDescriptionForum);

            return Ok(m);
        }

        [Route("DeletePostForum")]
        [HttpDelete]
        public async Task<IActionResult> DeletePostForum(ResponeseFroumID responeseFroumID)
        {
            var m = await _storedP.DeletePostForumAsync(LoggedInUser.Id, responeseFroumID.ForumID);

            return Ok(m);
        }

        [Route("DeleteCommentID")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCommentID(DeleteCommentIDRespones deleteCommentIDRespones)
        {
            var m = await _storedP.DeleteCommentIDAsync(LoggedInUser.Id, deleteCommentIDRespones.CommentID, deleteCommentIDRespones.ForumID);

            return Ok(m);
        }

        [Route("PostCommentProject")]
        [HttpPost]
        public async Task<IActionResult> PostCommentProject(AddCommentProject addCommentProject)
        {
            var m = await _storedP.AddCommitProjectAsync(LoggedInUser.Id, addCommentProject.ProjectID, addCommentProject.CommentProject);

            return Ok(m);
        }
        [Route("GetAllConmmentProject")]
        [HttpPost]
        public async Task<IActionResult> GetAllConmmentProject(GetCommentProjectRespone getCommentProject)
        {
            var m = await _storedP.GetCommenttoProjectAsync(LoggedInUser.Id, getCommentProject.ProjectID);

            return Ok(m);
        }

    }
}