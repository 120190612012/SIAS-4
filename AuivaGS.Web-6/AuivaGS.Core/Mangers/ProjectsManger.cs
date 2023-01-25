using AuivaGS.AWSServies.Servies;
using AuivaGS.Core.Mangers.MangerInterfaces;
using AuivaGS.DbModel.Models;
using AuivaGS.DbModel.ModelView;
using AutoMapper;
using AuviaGS.Common;
using AuviaGS.Common.Helper;
using AuviaGS.DbModel.ModelView;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace AuivaGS.Core.Mangers
{
    public class ProjectsManger : IProjectsManger
    {
        private readonly AuivaGSDbContext _auivaGSDbContext;
        private IMapper _mapper;
        private IStorageServies _storageServies;
        private IWebHostEnvironment _environment;


        public ProjectsManger(AuivaGSDbContext auivaGSDbContext, IMapper mapper, IStorageServies storageServies, IWebHostEnvironment webHostEnvironment)
        {
            _auivaGSDbContext = auivaGSDbContext;
            _mapper = mapper;
            _storageServies = storageServies;
            _environment = webHostEnvironment;
        }

        public async Task AddProject(AddProejctView addProjectView, UserModel loggedInUser)
        {
            var toolsList = addProjectView.projectTools.ToList();

            var choosenTools = _auivaGSDbContext.ProjectTools.Where(a => toolsList.Contains(a.NameTool)).ToList();

            var projectCatoregoy = addProjectView.projectCategories.ToList();

            var choosenCatoregoy = _auivaGSDbContext.ProjectCategories.Where(a => projectCatoregoy.Contains(a.CategoryName)).ToList();

            if (choosenTools.Count < 1 || choosenCatoregoy.Count < 1)
            {
                throw new AuviaGSException("Please Choose At least One Valid Tool and One Valid Catoregoy");
            }

            if (addProjectView.Images.Count < 1)
            {
                throw new AuviaGSException("Please Add at least one image");

            }
            var user = _auivaGSDbContext.Users.Find(loggedInUser.Id);


            using (var scope = await _auivaGSDbContext.Database.BeginTransactionAsync())
            {

                try
                {
                    var project = _auivaGSDbContext.Projects.Add(new Project
                    {
                        Title = addProjectView.Title,
                        SpaceWidth = (decimal?)addProjectView.SpaceWidth,
                        SpaceHeight = (decimal?)addProjectView.SpaceHeight,
                        Descirotion = addProjectView.Descrption
                    });
                    _auivaGSDbContext.SaveChanges();

                    var userGallary = _auivaGSDbContext.Galleries.FirstOrDefault(a => a.UserId == loggedInUser.Id);

                    if (userGallary == null)
                    {
                        userGallary = _auivaGSDbContext.Galleries.Add(new Gallery
                        {
                            UserId = loggedInUser.Id,
                        }).Entity;
                    }
                    _auivaGSDbContext.SaveChanges();
                    var GalleryProject = await _auivaGSDbContext.GalleryProjects.AddAsync(new GalleryProject
                    {
                        ProjectId = project.Entity.Id,
                        GallaryId = userGallary.Id
                    });
                    _auivaGSDbContext.SaveChanges();

                    foreach (var items in choosenCatoregoy)
                    {
                        await _auivaGSDbContext.ProjectConntionProjectCategories.AddAsync(new ProjectConntionProjectCategory
                        {
                            ProjectId = project.Entity.Id,
                            ProjectCategoryId = items.Id
                        });
                    }
                    _auivaGSDbContext.SaveChanges();

                    foreach (var items in choosenTools)
                    {
                        await _auivaGSDbContext.ProjectConntionTools.AddAsync(new ProjectConntionTool
                        {
                            ProjectId = project.Entity.Id,
                            ProjectToolId = items.Id
                        });
                    }
                    _auivaGSDbContext.SaveChanges();


                    string url = "";

                    foreach (var item in addProjectView.Images)
                    {
                        if (!string.IsNullOrWhiteSpace(item))
                        {
                            //     url = await Helper.SaveImage1(item, "auvia-images", _storageServies);
                            url = Helper.SaveImage(item, "projects-images");
                        }
                        var image = "";
                        if (!string.IsNullOrWhiteSpace(url))
                        {
                            var baseURL = "https://localhost:44366/";
                            image = @$"{baseURL}{url}";
                            // var baseURL1 = "https://auvia-images.s3.eu-west-3.amazonaws.com/";
                            // image = @$"{url}";
                            var projectPhath = _auivaGSDbContext.ProjectPhotoPaths.Add(new ProjectPhotoPath
                            {
                                ProjectPhotoPath1 = image
                            });
                            _auivaGSDbContext.SaveChanges();
                            _auivaGSDbContext.ProjectConntionPhotoProjectPaths.Add(new ProjectConntionPhotoProjectPath
                            {
                                PhotoProjectId = projectPhath.Entity.Id,
                                ProjectId = project.Entity.Id
                            });
                            _auivaGSDbContext.SaveChanges();
                        }

                    }
                    _auivaGSDbContext.SaveChanges();

                    scope.Commit();
                }
                catch (Exception e)
                {
                    scope.Rollback();
                    throw new AuviaGSException(e + "");
                }
            }

        }

        public List<GetProjectToolsDTO> GetProjectTools()
        {
            var tools = _auivaGSDbContext.ProjectTools.ToList();

            var toolsDto = _mapper.Map<List<GetProjectToolsDTO>>(tools);

            return toolsDto;
        }

        public List<GetProjectCategoryDTO> GetProjectCategory()
        {
            var categories = _auivaGSDbContext.ProjectCategories.ToList();

            var categoriesDto = _mapper.Map<List<GetProjectCategoryDTO>>(categories);

            return categoriesDto;
        }

        public List<GetAllProjectsDTO> GetAllProjects()
        {
            var gallary = _auivaGSDbContext.Galleries.Include(a => a.User).ToList();

            var projects = _auivaGSDbContext.Projects.ToList();

            var GallaryProjects = _auivaGSDbContext.GalleryProjects.Include(a => a.Project)
                                                                   .Include(a => a.Gallary)
                                                                   .ToList();

            var photos = _auivaGSDbContext.ProjectConntionPhotoProjectPaths.Include(a => a.PhotoProject).ToList();

            List<GetAllProjectsDTO> getAllProjectsDTO = new List<GetAllProjectsDTO>();

            foreach (var items in GallaryProjects)
            {
                var userGallary = gallary.Where(a => a.Id == items.GallaryId)
                                         .Select(a => $"{a.User.FirstNameUser} {a.User.LastNameUser}")
                                         .First();
                var userid = gallary.Where(a => a.Id == items.GallaryId)
                                         .Select(a => a.User.Id)
                                         .First();
                var userimageProject = gallary.Where(a => a.Id == items.GallaryId)
                                         .Select(a => a.User.Image)
                                         .First();
                var userProject = projects.Where(a => a.Id == items.ProjectId)
                                         .Select(a => a.Title)
                                         .First();
                var userProjectDisc = projects.Where(a => a.Id == items.ProjectId)
                                         .Select(a => a.Descirotion)
                                         .First();

                var createDate = projects.Where(a => a.Id == items.ProjectId)
                                         .Select(a => a.CreatedDateUTC)
                                         .First();
                var userImage = photos.Where(a => a.ProjectId == items.ProjectId).Select(a => a.PhotoProject.ProjectPhotoPath1).First();

               /* var filename = userImage.Split("filename=").Last();
                var folderPath = _environment.WebRootPath;
                folderPath = $@"{folderPath}\{filename}";*/


                getAllProjectsDTO.Add(new GetAllProjectsDTO()
                {
                    ProjectID = (int)items.ProjectId,
                    ImageString = userImage,
                    CreatorName = userGallary,
                    idUser = userid,
                    ProjectDescption = userProjectDisc,
                    ProjectName = userProject,
                    CreateDate = createDate,
                    UserImage = userimageProject
                });

            }
            getAllProjectsDTO = getAllProjectsDTO.OrderByDescending(a => a.CreateDate).ToList();

            return getAllProjectsDTO;
        }

        public void DeleteProject(UserModel loggedInUser, int Id)
        {
            var userGallary = _auivaGSDbContext.Galleries.FirstOrDefault(a => a.UserId == loggedInUser.Id);

            var userProject = _auivaGSDbContext.GalleryProjects.FirstOrDefault(a => a.GallaryId == userGallary.Id && a.ProjectId == Id);

            if (userProject == null)
            {
                throw new AuviaGSException("Invalid Delete!!");
            }

            foreach (var items in _auivaGSDbContext.ProjectConntionTools)
            {
                if (items.ProjectId == Id)
                {
                    _auivaGSDbContext.ProjectConntionTools.Remove(items);
                }
            }

            foreach (var items in _auivaGSDbContext.ProjectConntionProjectCategories)
            {
                if (items.ProjectId == Id)
                {
                    _auivaGSDbContext.ProjectConntionProjectCategories.Remove(items);
                }
            }
            List<int> Ids = new List<int>();
            foreach (var items in _auivaGSDbContext.ProjectConntionPhotoProjectPaths)
            {
                if (items.ProjectId == Id)
                {
                    Ids.Add((int)items.PhotoProjectId);
                    _auivaGSDbContext.ProjectConntionPhotoProjectPaths.Remove(items);
                }
            }

            foreach (var items in _auivaGSDbContext.ProjectPhotoPaths)
            {
                if (Ids.Contains(items.Id))
                {
                    _auivaGSDbContext.ProjectPhotoPaths.Remove(items);
                }
            }
            _auivaGSDbContext.GalleryProjects.Remove(userProject);

            _auivaGSDbContext.SaveChanges();
        }

        public List<GetAllProjectsDTO> GetAllProjectsForUser(UserModel loggedInUser)
        {
            var gallary = _auivaGSDbContext.Galleries.Include(a => a.User)
                                                     .Where(a => a.UserId == loggedInUser.Id)
                                                     .ToList();

            var projects = _auivaGSDbContext.Projects.ToList();

            var GallaryProjects = _auivaGSDbContext.GalleryProjects.Include(a => a.Project)
                                                                   .Include(a => a.Gallary)
                                                                   .Where(a => a.GallaryId == gallary.First().Id)
                                                                   .ToList();

            var photos = _auivaGSDbContext.ProjectConntionPhotoProjectPaths.Include(a => a.PhotoProject).ToList();

            List<GetAllProjectsDTO> getAllProjectsDTO = new List<GetAllProjectsDTO>();

            var userGallary = gallary.Select(a => $"{a.User.FirstNameUser} {a.User.LastNameUser}")
                                     .First();

            foreach (var items in GallaryProjects)
            {

                var userProject = projects.Where(a => a.Id == items.ProjectId)
                                          .Select(a => a.Title)
                                          .First();

                var createDate = projects.Where(a => a.Id == items.ProjectId)
                                         .Select(a => a.CreatedDateUTC)
                                         .First();
                var userImage = photos.Where(a => a.ProjectId == items.ProjectId).Select(a => a.PhotoProject.ProjectPhotoPath1).First();

                /* var filename = userImage.Split("filename=").Last();
                 var folderPath = _environment.WebRootPath;
                 folderPath = $@"{folderPath}\{filename}";*/


                getAllProjectsDTO.Add(new GetAllProjectsDTO()
                {
                    ProjectID = (int)items.ProjectId,
                    ImageString = userImage,
                    CreatorName = userGallary,
                    ProjectName = userProject,
                    CreateDate = createDate
                });

            }
            getAllProjectsDTO = getAllProjectsDTO.OrderByDescending(a => a.CreateDate).ToList();

            return getAllProjectsDTO;
        }

        public GetProjectDetalisById GetProjectDetalisById(int Id)
        {
            var project = _auivaGSDbContext.Projects.Find(Id)
                                                     ?? throw new AuviaGSException("Invalid Id!!");
            
            //var GalaryID = _auivaGSDbContext.GalleryProjects.Where(a => a.ProjectId == project.Id).Select(a => a.GallaryId).First()
            //    ?? throw new AuviaGSException("Invalid Id!!");
            //var GalaryUserId = _auivaGSDbContext.Galleries.Where(a => a.Id == GalaryID).Select(a => a.UserId)
            //    ?? throw new AuviaGSException("Invalid Id!!");
            //Its working but no need
            /*var projectCatorgoy = (from ProjectConntionProjectCategory in _auivaGSDbContext.ProjectConntionProjectCategories
                                   join ProjectCategory in _auivaGSDbContext.ProjectCategories
                                   on ProjectConntionProjectCategory.ProjectCategoryId equals ProjectCategory.Id
                                   where ProjectConntionProjectCategory.ProjectId == Id
                                   select ProjectCategory.CategoryName).ToList();*/

            var projectCatorgoy = _auivaGSDbContext.ProjectConntionProjectCategories.Include(c => c.ProjectCategory)
                                                                                    .Where(a => a.ProjectId == Id)
                                                                                    .Select(a => a.ProjectCategory.CategoryName)
                                                                                    .ToList();

            var projectTools = _auivaGSDbContext.ProjectConntionTools.Include(c => c.ProjectTool)
                                                                     .Where(a => a.ProjectId == Id)
                                                                     .Select(a => a.ProjectTool.NameTool)
                                                                     .ToList();

            var projectImages = _auivaGSDbContext.ProjectConntionPhotoProjectPaths.Include(c => c.PhotoProject)
                                                                                  .Where(a => a.ProjectId == Id)
                                                                                  .Select(a => a.PhotoProject.ProjectPhotoPath1)
                                                                                 .ToList();
            //var GallaryId = project.GalleryProjects.Select(a=>a.GallaryId).First();

            //var UserId = _auivaGSDbContext.Galleries.Include(c => c.Id)
            //                                                         .Where(a => a.Id == GallaryId)
            //                                                         .Select(a => a.UserId)
            //                                                         .ToList();
            //var User = _auivaGSDbContext.Users.Find(UserId);

            List<string> Images = new List<string>();
            foreach (var items in projectImages)    
            {
                /*  var filename = items.Split("filename=").Last();
                  var folderPath = _environment.WebRootPath;
                  folderPath = $@"{folderPath}\{filename}";*/

                Images.Add(items);
            }



            return new GetProjectDetalisById()
            {
                Id = project.Id,
                Title = project.Title,
                //UserCreate = User.Username,
                //ImageUserCreate = User.Image,
                SpaceWidth = project.SpaceWidth,
                SpaceHeight = project.SpaceHeight,
                ProjectTools = projectTools,
                ProejctCatoraoy = projectCatorgoy,
                Images = Images,
                CreatedDateUTC = project.CreatedDateUTC,
                Descirotion = project.Descirotion
            };
        }

    }
}

