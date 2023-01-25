using AuivaGS.DbModel.Models;
using AuivaGS.DbModel.ModelView;
using AuviaGS.DbModel.ModelView;

namespace AuivaGS.Core.Mangers.MangerInterfaces
{
    public interface IProjectsManger
    {
        public List<GetProjectToolsDTO> GetProjectTools();
        public List<GetProjectCategoryDTO> GetProjectCategory();

        public Task AddProject(AddProejctView addProjectView, UserModel loggedInUser);

        public List<GetAllProjectsDTO> GetAllProjects();

        public void DeleteProject(UserModel loggedInUser, int Id);

        public List<GetAllProjectsDTO> GetAllProjectsForUser(UserModel loggedInUser);


        public GetProjectDetalisById GetProjectDetalisById(int id);
    }
}
