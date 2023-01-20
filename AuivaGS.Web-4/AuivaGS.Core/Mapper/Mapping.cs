using AuivaGS.DbModel.Models;
using AuivaGS.DbModel.ModelView;
using AutoMapper;
using AuviaGS.DbModel.ModelView;

namespace AuviaGS.Core.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, SignUpResponse>().ReverseMap();
            CreateMap<User, LogInRespone>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<User, ProfileViewResponse>().ReverseMap();
            CreateMap<User, ForgetCustmerView>().ReverseMap();
            CreateMap<User, UserProfileView>().ReverseMap();
            CreateMap<ProjectTool, GetProjectToolsDTO>().ReverseMap();
            CreateMap<ProjectCategory, GetProjectCategoryDTO>().ReverseMap();
        }

    }
}
