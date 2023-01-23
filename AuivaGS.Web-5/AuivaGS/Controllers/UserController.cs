using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuviaGS.Controllers;
using AuviaGS.DbModel.Mangers.MangerInterfaces;
using AuviaGS.DbModel.ModelView;
using AuivaGS.DbModel.ModelView;

namespace AuivaGS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ApiBaseController
    {
        private IUserManger _userManger;

        public UserController(IUserManger userManger)
        {
            _userManger = userManger;
        }


        [Route("SignUp")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult SignUp(SignUpViews signUpuser)
        {
            var res = _userManger.SignUp(signUpuser);

            return Ok(res);
        }

        [Route("SignUpMobile")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult SignUpMobile(SignUpViews signUpuser)
        {
            var res = _userManger.SignUpMobile(signUpuser);

            return Ok(res);
        }

        [Route("ConfirmEmail")]
        [HttpPut]
        [AllowAnonymous]
        public IActionResult ConfirmEmail(ConfirmModel code)
        {
            var res = _userManger.ConfiremPassword(code);
            return Ok(res);
        }

        [Route("ResendConfirmationCode")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult ResendConfirmationCode(FrogetPasswordModel Email)
        {
            _userManger.ResendConfirmationCode(Email);

            return Ok("Done");
        }

        [Route("LogIn")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult LogIn(LogInRequset logInuser)
        {
            var res = _userManger.LogIn(logInuser);

            return Ok(res);
        }

        [Route("CompleteProfile")]
        [HttpPut]
        public async Task<IActionResult> CompleteProfile(UserProfileView profile)
        {
            var res = await _userManger.CompleteProfile(LoggedInUser, profile);

            return Ok(res);
        }
        [Route("UserEducation")]
        [HttpPut]
        public IActionResult UserEducation(List<UserEducationModelView> UserEducationModelView)
        {
            _userManger.UserEducation(LoggedInUser, UserEducationModelView);

            return Ok(new { Message = "Done" });
        }
        [Route("UserExperience")]
        [HttpPost]
        public IActionResult UserExperience(List<UserExperienceeView> UserExperienceeView)
        {
            _userManger.UserExperience(LoggedInUser, UserExperienceeView);

            return Ok(new { Message = "Done" });
        }

        [Route("UserPersonalSkills")]
        [HttpPost]
        public IActionResult UserPersonalSkills([FromBody] PersonalSkills userLanguges)
        {
            _userManger.UserPersonalSkills(LoggedInUser, userLanguges);

            return Ok(new { Message = "Done" });
        }

        [Route("GetUserInfo")]
        [HttpGet]
        public IActionResult GetUserInfo()
        {
            var res = _userManger.GetUserInfo(LoggedInUser);

            return Ok(res);
        }

        [Route("GetUserSkills")]
        [HttpGet]
        public IActionResult GetUserSkills()
        {
            var res = _userManger.GetUserSkills(LoggedInUser);

            return Ok(res);
        }

        [Route("GetUserExperince")]
        [HttpGet]
        public IActionResult GetUserExperince()
        {
            var res = _userManger.GetUserExperince(LoggedInUser);

            return Ok(res);
        }

        [Route("GetUserEduction")]
        [HttpGet]
        public IActionResult GetUserEduction()
        {
            var res = _userManger.GetUserEduction(LoggedInUser);

            return Ok(res);
        }


        [Route("RetriveImage")]
        [HttpGet]
        public IActionResult RetriveImage()
        {
            var image = _userManger.getUserImage(LoggedInUser);
            var filename = image.Split("filename=").Last();
            var folderPath = Directory.GetCurrentDirectory();
            folderPath = $@"{folderPath}\{filename}";
            var byteArray = System.IO.File.ReadAllBytes(folderPath);
            return File(byteArray, "image/jpeg", filename);
        }

        [Route("ForgetPassword")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult ForgetPassword(FrogetPasswordModel payload)
        {
            var res = _userManger.ForgetPassword(payload);
            return Ok(res);
        }

        [Route("ResetPassword")]
        [HttpPut]
        [AllowAnonymous]
        public IActionResult ResetPassword(ResetPasswordView passwordView)
        {
            var res = _userManger.ResetPassword(passwordView);
            return Ok(res);
        }

        [Route("ResetCurrentPassword")]
        [HttpPut]
        public IActionResult ResetCurrentPassword(ResetPasswordUserView passwordView)
        {
            _userManger.ResetCurrentPassword(LoggedInUser, passwordView);
            return Ok(new { Message = "Done" });
        }

        /*   [Route("SendSMS")]
           [HttpGet]
           public IActionResult SendSMS(string phoneNumber)
           {
               _userManger.ResetCurrentPassword(LoggedInUser, passwordView);
               return Ok("Done");
           }*/
    }
}
