using AuivaGS.AWSServies.Servies;
using AuivaGS.Core.Mangers.MangerInterfaces;
using AuivaGS.DbModel.Models;
using AuivaGS.DbModel.ModelView;
using AuivaGS.DbModel.ModelView.ArtDTOS;
using AutoMapper;
using AuviaGS.Common.Helper;
using AuviaGS.Common;
using AuviaGS.DbModel.ModelView;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuivaGS.Core.Mangers
{
    public class ArtManger : IArtManger
    {
        private readonly AuivaGSDbContext _auivaGSDbContext;
        private IMapper _mapper;
        private IStorageServies _storageServies;
        private IWebHostEnvironment _environment;


        public ArtManger(AuivaGSDbContext auivaGSDbContext, IMapper mapper, IStorageServies storageServies, IWebHostEnvironment webHostEnvironment)
        {
            _auivaGSDbContext = auivaGSDbContext;
            _mapper = mapper;
            _storageServies = storageServies;
            _environment = webHostEnvironment;
        }

        public async Task AddArtAsync(AddArtDto newArt, UserModel loggedInUser)
        {
            var typeList = newArt.ArtTyps.ToList();

            var choosenTools = _auivaGSDbContext.ArtTypes.Where(a => typeList.Contains(a.NameTypeArt)).ToList();


            if (choosenTools.Count < 1)
            {
                throw new AuviaGSException("Please Choose At least One Valid Tool");
            }

            if (newArt.Images.Count < 1)
            {
                throw new AuviaGSException("Please Add at least one image");

            }
            var user = _auivaGSDbContext.Users.Find(loggedInUser.Id);


            using (var scope = await _auivaGSDbContext.Database.BeginTransactionAsync())
            {

                try
                {
                    var art = _auivaGSDbContext.Arts.Add(new Art
                    {
                        NameArt = newArt.Name
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
                    var GalleryProject = await _auivaGSDbContext.GallaryArts.AddAsync(new GallaryArt
                    {
                        ArtId = art.Entity.Id,
                        GallaryId = userGallary.Id
                    });
                    _auivaGSDbContext.SaveChanges();


                    foreach (var items in choosenTools)
                    {
                        await _auivaGSDbContext.ArtConntionTypes.AddAsync(new ArtConntionType
                        {
                            ArtId = art.Entity.Id,
                            TypeId = items.Id
                        });
                    }
                    _auivaGSDbContext.SaveChanges();


                    string url = "";

                    foreach (var item in newArt.Images)
                    {
                        if (!string.IsNullOrWhiteSpace(item))
                        {
                            //     url = await Helper.SaveImage1(item, "auvia-images", _storageServies);
                            url = Helper.SaveImage(item, "arts-images");
                        }
                        var image = "";
                        if (!string.IsNullOrWhiteSpace(url))
                        {
                            var baseURL = "https://localhost:44309/";
                            image = @$"{baseURL}/api/v1/user/fileretrive/arts-images?filename={url}";
                            // var baseURL1 = "https://auvia-images.s3.eu-west-3.amazonaws.com/";
                            // image = @$"{url}";
                            var projectPhath = _auivaGSDbContext.ArtPhotoPaths.Add(new ArtPhotoPath
                            {
                                PhotoPath = image
                            });
                            _auivaGSDbContext.SaveChanges();
                            _auivaGSDbContext.ArtConntionPhotoPaths.Add(new ArtConntionPhotoPath
                            {
                                PhotoPathId = projectPhath.Entity.Id,
                                ArtId = art.Entity.Id
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
    }
}
