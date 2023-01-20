using AuivaGS.DbModel.Models;
using AuivaGS.DbModel.ModelView;
using AuviaGS.DbModel.ModelView;
using AuviaGS.DbModel.ModelView.ProfileGetEndPoints;

namespace AuviaGS.DbModel.Mangers.MangerInterfaces
{
    public interface IUserManger
    {
        public SignUpResponse SignUp(SignUpViews user);

        public LogInRespone LogIn(LogInRequset logInUser);

        public Task<ProfileViewResponse> CompleteProfile(UserModel loggedInUser, UserProfileView userProfileView);

        public void UserEducation(UserModel loggedInUser, List<UserEducationModelView> listEductions);

        public void UserExperience(UserModel loggedInUser, List<UserExperienceeView> listExperiencee);

        //
        public void UserPersonalSkills(UserModel loggedInUser, PersonalSkills userLanguges);

        public UserInfo GetUserInfo(UserModel loggedInUser);

        public string getUserImage(UserModel loggedInUser);

        public UserModel ConfiremPassword(ConfirmModel confirmation);

        public ForgetCustmerView ForgetPassword(FrogetPasswordModel payload);

        public UserModel ResetPassword(ResetPasswordView passwordView);

        public SignUpResponse SignUpMobile(SignUpViews signUpuser);
        public List<string> GetUserSkills(UserModel loggedInUser);

        public List<UserExperience> GetUserExperince(UserModel loggedInUser);

        public List<UserEducation> GetUserEduction(UserModel loggedInUser);

        public void ResetCurrentPassword(UserModel loggedInUser, ResetPasswordUserView rest);

        public void ResendConfirmationCode(FrogetPasswordModel paylod);


    }
}
