using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using AuviaGS.EmailService;
using AuviaGS.Common;
using AuviaGS.Common.Helper;
using AuviaGS.DbModel.Mangers.MangerInterfaces;
using AuviaGS.DbModel.ModelView;
using AuviaGS.DbModel.ModelView.ProfileGetEndPoints;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuviaGS.Models.Static;
using AuviaGS.ModelViews.Enums;
using Twilio.Clients;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;
using AuivaGS.DbModel.Models;
using AuivaGS.DbModel.ModelView;
using AuviaGS.Notifications;
using AuivaGS.AWSServies.Servies;

namespace AuviaGS.Core.Mangers
{
    public class UserManger : IUserManger
    {
        private readonly AuivaGSDbContext _auivaGsDbContext;
        private IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private ITwilioRestClient _client;
        private readonly IStorageServies _storageServies;



        public UserManger(AuivaGSDbContext auivaGsDbContext, IMapper mapper, IEmailSender emailSender, ITwilioRestClient client, IStorageServies storageServies)
        {
            _auivaGsDbContext = auivaGsDbContext;
            _mapper = mapper;
            _emailSender = emailSender;
            _client = client;
            _storageServies = storageServies;
        }

        public SignUpResponse SignUp(SignUpViews signUpuser)
        {
            var user = _auivaGsDbContext.Users.FirstOrDefault(a => a.Email.Equals(signUpuser.Email) || a.Username.Equals(signUpuser.UserName));
            if (user != null)
            {
                throw new AuviaGSException(100, "Already Exectied");
            }
            if (signUpuser.Password != signUpuser.ConfiremPassword)
            {
                throw new AuviaGSException(101, "Not Matched");
            }
            string password = HashPassword(signUpuser.Password);
            var newUser = _auivaGsDbContext.Users.Add(new User
            {
                Username = signUpuser.UserName,
                Email = signUpuser.Email,
                Password = password,
                ConfirmationCode = Guid.NewGuid().ToString().Replace("-", "").ToString(),
                ConfirmaitonDateDue = DateTime.Now
            }).Entity;

            var builder = new EmailBuilder(ActionInvocationTypeEnum.EmailConfirmation,
                    new Dictionary<string, string>
                    {
                                    { "AssigneeName", $"{newUser.FirstNameUser} {newUser.LastNameUser}" },
                                    { "Link", $"{newUser.ConfirmationCode}" }
                    }, "http://localhost:4200/LandingPage/confirmCode");

            var message = new Message(new string[] { newUser.Email }, builder.GetTitle(), builder.GetBody(""));
            _emailSender.SendEmail(message);
            _auivaGsDbContext.SaveChanges();
            /*   var message2 = MessageResource.Create(
                             to: new PhoneNumber("972567290126"),
                             from: new PhoneNumber("13854628195"),
                             body: "Hallo Man",
                             client: _client);*/

            var res = _mapper.Map<SignUpResponse>(newUser);

            return res;

        }

        public SignUpResponse SignUpMobile(SignUpViews signUpuser)
        {
            var user = _auivaGsDbContext.Users.FirstOrDefault(a => a.Email.Equals(signUpuser.Email) || a.Username.Equals(signUpuser.UserName));
            if (user != null)
            {
                throw new AuviaGSException(100, "Already Exectied");
            }
            if (signUpuser.Password != signUpuser.ConfiremPassword)
            {
                throw new AuviaGSException(101, "Not Matched");
            }
            string password = HashPassword(signUpuser.Password);
            var newUser = _auivaGsDbContext.Users.Add(new User
            {
                Username = signUpuser.UserName,
                Email = signUpuser.Email,
                Password = password,
                ConfirmationCode = CreateRandomPassword(4),
                ConfirmaitonDateDue = DateTime.Now
            }).Entity;

            var builder = new EmailBuilder(ActionInvocationTypeEnum.EmailConfirmationMobile,
                    new Dictionary<string, string>
                    {
                                    { "AssigneeName", $"{newUser.FirstNameUser} {newUser.LastNameUser}" },
                                    { "Link", $"{newUser.ConfirmationCode}" }
                    }, "");

            var message = new Message(new string[] { newUser.Email }, builder.GetTitle(), builder.GetBody(newUser.ConfirmationCode));
            _emailSender.SendEmail(message);
            _auivaGsDbContext.SaveChanges();

            var res = _mapper.Map<SignUpResponse>(newUser);

            return res;

        }
        public void ResendConfirmationCode(FrogetPasswordModel paylod)
        {
            var user = _auivaGsDbContext.Users.FirstOrDefault(a => a.Email.Equals(paylod.Email));
            if (user == null)
            {
                throw new AuviaGSException("Not Found");
            }
            if (user.IsConfirmed)
            {
                throw new AuviaGSException("User Already Confimed");

            }
            user.ConfirmationCode = CreateRandomPassword(4);
            var builder = new EmailBuilder(ActionInvocationTypeEnum.EmailConfirmationMobile,
              new Dictionary<string, string>
              {
                                    { "AssigneeName", $"{user.FirstNameUser} {user.LastNameUser}" },
                                    { "Link", $"{user.ConfirmationCode}" }
               }, "");

            var message = new Message(new string[] { user.Email }, builder.GetTitle(), builder.GetBody(user.ConfirmationCode));
            _emailSender.SendEmail(message);
            _auivaGsDbContext.Users.Update(user);
            _auivaGsDbContext.SaveChanges();
        }



        public LogInRespone LogIn(LogInRequset logInUser)
        {
            var user = _auivaGsDbContext.Users.FirstOrDefault(a => a.Email.Equals(logInUser.Email));

            if (user == null)
            {
                throw new AuviaGSException(101, "User Not Found");
            }

            if (!user.IsConfirmed)
            {
                throw new ServiceValidationException(101, "Please Confirm your Email First");
            }


            if (!VerifyHashPassword(logInUser.Password, user.Password))
            {
                throw new AuviaGSException("Invlied Email or Password");
            }

            var res = _mapper.Map<LogInRespone>(user);

            res.Token = $"Bearer {GenerateJWTToken(user)}";
            res.IsComplete = user.IsCompleted;
            res.IsArchitect = user.IsArchitect;

            return res;
        }

        public async Task<ProfileViewResponse> CompleteProfile(UserModel loggedInUser, UserProfileView userProfileView)
        {
            var logged = _auivaGsDbContext.Users.FirstOrDefault(a => a.Email.Equals(loggedInUser.Email))
                                                ?? throw new AuviaGSException("Undfined User!");



            logged.FirstNameUser = userProfileView.FirstName;
            logged.LastNameUser = userProfileView.LastName;
            logged.Country = userProfileView.Country;
            logged.City = userProfileView.City;
            logged.PhoneNumber = userProfileView.PhoneNumber;
            logged.Birthday = userProfileView.BirthDay;
            logged.IsArchitect = userProfileView.IsArchitect;
            logged.IsCompleted = true;



            var url = "";

            if (!string.IsNullOrWhiteSpace(userProfileView.ImageString))
            {
                url = Helper.SaveImage(userProfileView.ImageString, "profileimages");
                //   url = await Helper.SaveImage1(userProfileView.ImageString, "auvia-images", _storageServies);
            }

            if (!string.IsNullOrWhiteSpace(url))
            {
                var baseURL = "https://localhost:44366/";
                logged.Image = @$"{baseURL}{url}";
                //  var baseURL1 = "https://auvia-images.s3.eu-west-3.amazonaws.com/";
                //  logged.Image = @$"{baseURL}/{url}";
            }
            if (logged.Image == null)
            {
                logged.Image = "";
            }
            _auivaGsDbContext.Users.Update(logged);
            await _auivaGsDbContext.SaveChangesAsync();

            var res = _mapper.Map<ProfileViewResponse>(logged);
            res.IsArticher = userProfileView.IsArchitect;
            return res;
        }

        public void UserEducation(UserModel loggedInUser, List<UserEducationModelView> listEductions)
        {

            foreach (var item in listEductions)
            {
                if (item.Id == 0)
                {
                    _auivaGsDbContext.UserEducations.Add(new UserEducation
                    {
                        UserId = loggedInUser.Id,
                        UniversityName = item.University,
                        FacultyName = item.Faculty,
                        GraduationYear = item.GraduationYear,
                        AcademicDegree = item.AcademicDegree,
                        Descrpiction = item.Description,
                    });
                }
                else
                {
                    var userEdution = _auivaGsDbContext.UserEducations.Find(item.Id)
                                                    ?? throw new AuviaGSException("Not Found");
                    if (userEdution.UserId == loggedInUser.Id)
                    {
                        userEdution.UserId = loggedInUser.Id;
                        userEdution.UniversityName = item.University;
                        userEdution.FacultyName = item.Faculty;
                        userEdution.GraduationYear = item.GraduationYear;
                        userEdution.AcademicDegree = item.AcademicDegree;
                        userEdution.Descrpiction = item.Description;
                        _auivaGsDbContext.UserEducations.Update(userEdution);
                    }
                    else
                    {
                        throw new AuviaGSException("Not Authorized to Edit This..");
                    }

                }

            }
            _auivaGsDbContext.SaveChanges();

        }
        public void UserExperience(UserModel loggedInUser, List<UserExperienceeView> listExperiencee)
        {
            foreach (var item in listExperiencee)
            {
                if (item.Id == 0)
                {
                    _auivaGsDbContext.UserExperiences.Add(new UserExperience
                    {
                        UserId = loggedInUser.Id,
                        CompanyName = item.Company,
                        Specialization = item.Specializtion,
                        Country = item.Country,
                        City = item.City,
                        FromDate = item.FromDate,
                        ToDate = item.ToDate,
                        Activity = item.Descrption
                    });
                }
                else
                {
                    var userExperience = _auivaGsDbContext.UserExperiences.Find(item.Id)
                                                    ?? throw new AuviaGSException("Not Found");
                    if (userExperience.UserId == loggedInUser.Id)
                    {
                        userExperience.UserId = loggedInUser.Id;
                        userExperience.CompanyName = item.Company;
                        userExperience.Specialization = item.Specializtion;
                        userExperience.Country = item.Country;
                        userExperience.City = item.City;
                        userExperience.FromDate = item.FromDate;
                        userExperience.ToDate = item.ToDate;
                        userExperience.Activity = item.Descrption;
                        _auivaGsDbContext.UserExperiences.Update(userExperience);
                    }
                    else
                    {
                        throw new AuviaGSException("Not Authorized to Edit This..");
                    }

                }

                _auivaGsDbContext.SaveChanges();
            }

        }
        //
        //public void UserPersonalSkills(UserModel loggedInUser, PersonalSkills personalSkills)
        //{
        //    var serachUser = _siasDbContext.UserLanguages.FirstOrDefault(a => a.UserId == loggedInUser.UserId);
        //    if (serachUser == null)
        //    {
        //        _siasDbContext.UserLanguages.Add(new UserLanguage
        //        {
        //            UserId = loggedInUser.UserId,
        //            Language = personalSkills.MotherLanguage,
        //            IsMotherLanguage = true
        //        });

        //        if (personalSkills.Languages.Count > 0)
        //        {
        //            foreach (var item in personalSkills.Languages)
        //            {
        //                if (string.IsNullOrEmpty(item))
        //                {
        //                    continue;
        //                }
        //                else
        //                {
        //                    _siasDbContext.UserLanguages.Add(new UserLanguage
        //                    {
        //                        UserId = loggedInUser.UserId,
        //                        Language = item,
        //                        IsMotherLanguage = false
        //                    });

        //                }
        //            }
        //        }

        //    }
        //    else
        //    {
        //        var updateMother = _siasDbContext.UserLanguages.FirstOrDefault(a => a.IsMotherLanguage == true && a.UserId == loggedInUser.UserId);
        //        updateMother.Language = personalSkills.MotherLanguage;
        //        _siasDbContext.UserLanguages.Update(updateMother);

        //        var forginLangues = _siasDbContext.UserLanguages.Where(a => a.IsMotherLanguage == false && a.UserId == loggedInUser.UserId)
        //                                                        .ToList();



        //        for (var i = 0; i < personalSkills.Languages.Count; i++)
        //        {
        //            if (!forginLangues[i].Language.Equals(personalSkills.Languages[i]))
        //            {
        //                var find = _siasDbContext.UserLanguages.FirstOrDefault(a => a.Language.Equals(forginLangues[i].Language)
        //                                                        && a.UserId == loggedInUser.UserId );
        //                find.Language = personalSkills.Languages[i];
        //                _siasDbContext.UserLanguages.Update(find);
        //            }
        //        }

        //        var newLanguges = personalSkills.Languages.Except(forginLangues.Select(a => a.Language)).ToList();

        //        foreach (var item in newLanguges)
        //        {
        //            _siasDbContext.UserLanguages.Add(new UserLanguage
        //            {
        //                UserId = loggedInUser.UserId,
        //                Language = item,
        //                IsMotherLanguage = false
        //            });
        //        }

        //        var userSkill = _siasDbContext.UserSkills.Where(a => a.UserId == loggedInUser.UserId).Include(a => a.Skill).ToList();

        //        var databaseSkills = _siasDbContext.Skills.ToList();



        //        var list3 = personalSkills.Skills.Except(userSkill.Select(a => a.Skill.Name)).ToList();


        //        foreach (var item in list3)
        //        {
        //            var skill = _siasDbContext.Skills.FirstOrDefault(a => string.Equals(item.ToLower(), a.Name.ToLower()))
        //                                                ?? throw new SIASException("Not Found");

        //            _siasDbContext.UserSkills.Add(new UserSkill
        //            {
        //                SkillId = skill.Id,
        //                UserId = loggedInUser.UserId

        //            });

        //        }

        //    }


        //    var user = _siasDbContext.Users.Find(loggedInUser.UserId);
        //    user.IsCompleted = true;
        //    _siasDbContext.Users.Update(user);

        //    _siasDbContext.SaveChanges();
        //}

        public void UserPersonalSkills(UserModel loggedInUser, PersonalSkills personalSkills)
        {
            var use = _auivaGsDbContext.UserLanguages.Where(a => a.IsMotherLanguage == true && a.UserId == loggedInUser.Id)
                                                  .FirstOrDefault();

            if (use != null && !string.IsNullOrEmpty(personalSkills.MotherLanguage))
            {
                throw new AuviaGSException("Already Have Mother Languge!");
            }

            if (string.IsNullOrEmpty(personalSkills.MotherLanguage))
            {
                throw new AuviaGSException("Invlaid!");

            }
            _auivaGsDbContext.UserLanguages.Add(new UserLanguage
            {
                UserId = loggedInUser.Id,
                Language = personalSkills.MotherLanguage,
                IsMotherLanguage = true
            });



            if (personalSkills.Languages.Count > 0)
            {
                var use1 = _auivaGsDbContext.UserLanguages.Where(a => a.UserId == loggedInUser.Id).Select(a => a.Language).ToList();
                foreach (var item in personalSkills.Languages)
                {
                    if (use1.Contains(item) || use1.Contains(personalSkills.MotherLanguage))
                    {
                        throw new AuviaGSException("Already Exstied");
                    }
                }
                foreach (var item in personalSkills.Languages)
                {
                    if (string.IsNullOrEmpty(item))
                    {
                        continue;
                    }
                    else
                    {
                        _auivaGsDbContext.UserLanguages.Add(new UserLanguage
                        {
                            UserId = loggedInUser.Id,
                            Language = item,
                            IsMotherLanguage = false
                        });
                    }

                }
            }
            if (personalSkills.Skills.Count > 0)
            {
                foreach (var item in personalSkills.Skills)
                {
                    var skill = _auivaGsDbContext.Skills.FirstOrDefault(a => string.Equals(item.ToLower(), a.Name.ToLower()))
                                                        ?? throw new AuviaGSException("Not Found");

                    _auivaGsDbContext.UserSkills.Add(new UserSkill
                    {
                        UserId = loggedInUser.Id,
                        SkillId = skill.Id
                    });
                }
            }

            var user = _auivaGsDbContext.Users.Find(loggedInUser.Id);
            user.IsCompleted = true;
            _auivaGsDbContext.Users.Update(user);

            _auivaGsDbContext.SaveChanges();
        }


        public UserInfo GetUserInfo(UserModel loggedInUser)
        {
            var user = _auivaGsDbContext.Users.Find(loggedInUser.Id);

        /*    var filename = user.Image.Split("filename=").Last();
            var folderPath = Directory.GetCurrentDirectory();
            folderPath = $@"{folderPath}\{filename}";*/

            var userInfo = new UserInfo
            {
                FullName = $"{user.FirstNameUser} {user.LastNameUser}",
                Username = user.Username,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Description = user.Descirption,
                City = user.City,
                Country = user.Country,
                Image = user.Image
            };
            return userInfo;
        }

        public string getUserImage(UserModel loggedInUser)
        {
            var image = _auivaGsDbContext.Users.Where(a => a.Id == loggedInUser.Id).Select(a => a.Image).FirstOrDefault();
            if (image == null)
            {
                return "";
            }
            return image;
        }

        public List<string> GetUserSkills(UserModel loggedInUser)
        {
            var userSkills = _auivaGsDbContext.UserSkills.Include(a => a.Skill)
                                                      .Where(a => a.UserId == loggedInUser.Id)
                                                      .Select(a => a.Skill.Name).ToList();

            return userSkills;
        }
        public List<UserExperience> GetUserExperince(UserModel loggedInUser)
        {
            var userExperience = _auivaGsDbContext.UserExperiences.Where(a => a.UserId == loggedInUser.Id)
                                                               .ToList();

            return userExperience;
        }

        public List<UserEducation> GetUserEduction(UserModel loggedInUser)
        {
            var userEducations = _auivaGsDbContext.UserEducations.Where(a => a.UserId == loggedInUser.Id)
                                                                 .ToList();

            return userEducations;
        }


        public UserModel ConfiremPassword(ConfirmModel confirmation)
        {
            if (string.IsNullOrEmpty(confirmation.Code))
            {
                throw new ServiceValidationException("Invalid or expired confirmation link received");
            }

            var user = _auivaGsDbContext.Users.FirstOrDefault(a => a.ConfirmationCode.Equals(confirmation.Code))
                                             ?? throw new ServiceValidationException("Invalid or expired confirmation link received");

            TimeSpan time = (TimeSpan)(DateTime.Now - user.ConfirmaitonDateDue);

            if ((int)time.TotalHours > 1)
            {
                throw new ServiceValidationException("Invalid or expired confirmation link received");
            }

            if (confirmation.Code.Length == 6)
            {
                user = _auivaGsDbContext.Users
                          .FirstOrDefault(a => a.ConfirmationCode
                          .Equals(confirmation.Code)
                           )
                            ?? throw new ServiceValidationException("Invalid or expired confirmation link received");

            }
            else
            {
                user = _auivaGsDbContext.Users
                                          .FirstOrDefault(a => a.ConfirmationCode
                                          .Equals(confirmation.Code)
                                           && !a.IsConfirmed)
               ?? throw new ServiceValidationException("Invalid or expired confirmation link received");
            }


            user.IsConfirmed = true;
            user.ConfirmationCode = string.Empty;
            _auivaGsDbContext.Users.Update(user);
            _auivaGsDbContext.SaveChanges();
            return _mapper.Map<UserModel>(user);
        }




        public ForgetCustmerView ForgetPassword(FrogetPasswordModel payload)
        {
            var user = _auivaGsDbContext.Users.FirstOrDefault(a => a.Email.Equals(payload.Email))
                                                    ?? throw new AuviaGSException("Not Found");

            user.ConfirmationCode = Guid.NewGuid().ToString().Replace("-", "").ToString();

            var builder = new EmailBuilder(ActionInvocationTypeEnum.ResetPassword,
             new Dictionary<string, string>
             {
                                    { "AssigneeName", $"{user.FirstNameUser} {user.LastNameUser}" },
                                    { "Link", $"{user.ConfirmationCode}" }
             }, "http://127.0.0.1:5500/newpassword.html");

            string code = CreateRandomPassword(6);
            var message = new Message(new string[] { user.Email }, builder.GetTitle(), builder.GetBody(code));
            _emailSender.SendEmail(message);
            user.ConfirmationCode = code;
            user.ConfirmaitonDateDue = DateTime.Now;
            var mapped = _mapper.Map<ForgetCustmerView>(user);
            _auivaGsDbContext.Update(user);
            _auivaGsDbContext.SaveChanges();
            return mapped;
        }

        public UserModel ResetPassword(ResetPasswordView passwordView)
        {
            var user = _auivaGsDbContext.Users.FirstOrDefault(a => a.Email == passwordView.Email)
                                                 ?? throw new AuviaGSException("Not found");
            if (!user.IsConfirmed)
            {
                throw new AuviaGSException("Not Confirmed");
            }
            if (passwordView.NewPassword == passwordView.ConfirmPassword)
            {
                user.Password = HashPassword(passwordView.ConfirmPassword);
                _auivaGsDbContext.Update(user);
                _auivaGsDbContext.SaveChanges();
            }

            return _mapper.Map<UserModel>(user);

        }

        public void ResetCurrentPassword(UserModel loggedInUser, ResetPasswordUserView rest)
        {
            var user = _auivaGsDbContext.Users.Find(loggedInUser.Id);
            if (string.IsNullOrEmpty(rest.CurrentPassword) || string.IsNullOrEmpty(rest.ConfirmPassword) || string.IsNullOrEmpty(rest.NewPassword))
            {
                throw new AuviaGSException("Empty Fildes!!");
            }
            if (VerifyHashPassword(rest.CurrentPassword, user.Password))
            {
                if (rest.NewPassword.Equals(rest.ConfirmPassword))
                {
                    user.Password = HashPassword(rest.NewPassword);
                }
            }
            else
            {
                throw new AuviaGSException("Not Valied");
            }
            _auivaGsDbContext.Users.Update(user);
            _auivaGsDbContext.SaveChanges();

        }

        #region privet


        private static string CreateRandomPassword(int length = 6)
        {
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();

            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }
        private static string HashPassword(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            return hashedPassword;
        }

        private static bool VerifyHashPassword(string password, string HashedPasword)
        {
            return BCrypt.Net.BCrypt.Verify(password, HashedPasword);
        }

        private string GenerateJWTToken(User user)
        {
            var jwtKey = "#test.key*&^vanthis%$^&*()$%^@#$@!@#%$#^%&*%^*";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                  new Claim(JwtRegisteredClaimNames.Sub, $"{user.FirstNameUser} {user.LastNameUser}"),
                  new Claim(JwtRegisteredClaimNames.Email, user.Email),
                  new Claim("Id", user.Id.ToString()),
                  new Claim("FirstName", user.FirstNameUser),
                  new Claim("Email", user.Email),
                  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
              };

            var issuer = "test.com";

            var token = new JwtSecurityToken(
                        issuer,
                        issuer,
                        claims,
                        expires: DateTime.Now.AddDays(20),
                        signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion privet

    }

}




