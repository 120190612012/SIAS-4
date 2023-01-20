using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AuivaGS.DbModel.Models
{
    public partial class AuivaGSDbContext : DbContext
    {
        public AuivaGSDbContext()
        {
        }

        public AuivaGSDbContext(DbContextOptions<AuivaGSDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Architecture> Architectures { get; set; } = null!;
        public virtual DbSet<Art> Arts { get; set; } = null!;
        public virtual DbSet<ArtConntionPhotoPath> ArtConntionPhotoPaths { get; set; } = null!;
        public virtual DbSet<ArtConntionType> ArtConntionTypes { get; set; } = null!;
        public virtual DbSet<ArtPhotoPath> ArtPhotoPaths { get; set; } = null!;
        public virtual DbSet<ArtType> ArtTypes { get; set; } = null!;
        public virtual DbSet<Booth> Booths { get; set; } = null!;
        public virtual DbSet<BoothConntionPhoto> BoothConntionPhotos { get; set; } = null!;
        public virtual DbSet<BoothConntionTool> BoothConntionTools { get; set; } = null!;
        public virtual DbSet<BoothPhotoPath> BoothPhotoPaths { get; set; } = null!;
        public virtual DbSet<GallaryArt> GallaryArts { get; set; } = null!;
        public virtual DbSet<GallaryBooth> GallaryBooths { get; set; } = null!;
        public virtual DbSet<GallaryMoodBoard> GallaryMoodBoards { get; set; } = null!;
        public virtual DbSet<Gallery> Galleries { get; set; } = null!;
        public virtual DbSet<GalleryProject> GalleryProjects { get; set; } = null!;
        public virtual DbSet<MoodBoard> MoodBoards { get; set; } = null!;
        public virtual DbSet<MoodBoardConntionPhoto> MoodBoardConntionPhotos { get; set; } = null!;
        public virtual DbSet<MoodBoardConntionTemplate> MoodBoardConntionTemplates { get; set; } = null!;
        public virtual DbSet<MoodBoardPhotoPath> MoodBoardPhotoPaths { get; set; } = null!;
        public virtual DbSet<MoodBoardTemplate> MoodBoardTemplates { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<ProjectCategory> ProjectCategories { get; set; } = null!;
        public virtual DbSet<ProjectConntionPhotoProjectPath> ProjectConntionPhotoProjectPaths { get; set; } = null!;
        public virtual DbSet<ProjectConntionProjectCategory> ProjectConntionProjectCategories { get; set; } = null!;
        public virtual DbSet<ProjectConntionTool> ProjectConntionTools { get; set; } = null!;
        public virtual DbSet<ProjectPhotoPath> ProjectPhotoPaths { get; set; } = null!;
        public virtual DbSet<ProjectTool> ProjectTools { get; set; } = null!;
        public virtual DbSet<Skill> Skills { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserEducation> UserEducations { get; set; } = null!;
        public virtual DbSet<UserExperience> UserExperiences { get; set; } = null!;
        public virtual DbSet<UserLanguage> UserLanguages { get; set; } = null!;
        public virtual DbSet<UserSkill> UserSkills { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=MOHMMAD2001;Initial Catalog=SIAS-Graduation-Project-4;Integrated Security=True ; TrustServerCertificate=True; uid=sa; Password=123"    );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Architecture>(entity =>
            {
                entity.ToTable("architecture");

                entity.Property(e => e.ArchitectureId).HasColumnName("architecture _ID");

                entity.Property(e => e.GalleryId).HasColumnName("Gallery_ID");

                entity.Property(e => e.TypeArchitecture).HasColumnName("Type_architecture");
            });

            modelBuilder.Entity<Art>(entity =>
            {
                entity.ToTable("Art");

                entity.Property(e => e.Id)                   
                    .HasColumnName("ID");

                entity.Property(e => e.NameArt)
                    .HasMaxLength(10)
                    .HasColumnName("Name_Art")
                    .IsFixedLength();
            });

            modelBuilder.Entity<ArtConntionPhotoPath>(entity =>
            {
                entity.ToTable("ArtConntionPhotoPath");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ArtId).HasColumnName("Art_ID");

                entity.Property(e => e.PhotoPathId).HasColumnName("Photo_Path_ID");

                entity.HasOne(d => d.Art)
                    .WithMany(p => p.ArtConntionPhotoPaths)
                    .HasForeignKey(d => d.ArtId)
                    .HasConstraintName("FK_ArtConntionPhotoPath_Art");

                entity.HasOne(d => d.PhotoPath)
                    .WithMany(p => p.ArtConntionPhotoPaths)
                    .HasForeignKey(d => d.PhotoPathId)
                    .HasConstraintName("FK_ArtConntionPhotoPath_ArtPhotoPath");
            });

            modelBuilder.Entity<ArtConntionType>(entity =>
            {
                entity.ToTable("ArtConntionType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ArtId).HasColumnName("Art_ID");

                entity.Property(e => e.TypeId).HasColumnName("Type_ID");

                entity.HasOne(d => d.Art)
                    .WithMany(p => p.ArtConntionTypes)
                    .HasForeignKey(d => d.ArtId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArtConntionType_Art");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.ArtConntionTypes)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArtConntionType_ArtType");
            });

            modelBuilder.Entity<ArtPhotoPath>(entity =>
            {
                entity.ToTable("ArtPhotoPath");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PhotoPath).HasColumnName("Photo_Path");
            });

            modelBuilder.Entity<ArtType>(entity =>
            {
                entity.ToTable("ArtType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.NameTypeArt)
                    .HasMaxLength(255)
                    .HasColumnName("Name_Type_Art");
            });

            modelBuilder.Entity<Booth>(entity =>
            {
                entity.ToTable("Booth");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.NameBooth)
                    .HasMaxLength(255)
                    .HasColumnName("Name_Booth");

                entity.Property(e => e.SpaceHeight)
                    .HasColumnType("decimal(6, 3)")
                    .HasColumnName("Space_height");

                entity.Property(e => e.SpaceWidth)
                    .HasColumnType("decimal(6, 3)")
                    .HasColumnName("Space_width");
            });

            modelBuilder.Entity<BoothConntionPhoto>(entity =>
            {
                entity.ToTable("BoothConntionPhoto");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.BoothId).HasColumnName("Booth_ID");

                entity.Property(e => e.BoothPhotoPathId).HasColumnName("Booth_Photo_Path_ID");

                entity.HasOne(d => d.Booth)
                    .WithMany(p => p.BoothConntionPhotos)
                    .HasForeignKey(d => d.BoothId)
                    .HasConstraintName("FK_BoothConntionPhoto_Booth");

                entity.HasOne(d => d.BoothPhotoPath)
                    .WithMany(p => p.BoothConntionPhotos)
                    .HasForeignKey(d => d.BoothPhotoPathId)
                    .HasConstraintName("FK_BoothConntionPhoto_BoothPhotoPath");
            });

            modelBuilder.Entity<BoothConntionTool>(entity =>
            {
                entity.ToTable("BoothConntionTool");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BoothId).HasColumnName("Booth_ID");

                entity.Property(e => e.ProjectToolId).HasColumnName("ProjectTool_ID");

                entity.HasOne(d => d.Booth)
                    .WithMany(p => p.BoothConntionTools)
                    .HasForeignKey(d => d.BoothId)
                    .HasConstraintName("FK_BoothConntionTool_Booth");

                entity.HasOne(d => d.ProjectTool)
                    .WithMany(p => p.BoothConntionTools)
                    .HasForeignKey(d => d.ProjectToolId)
                    .HasConstraintName("FK_BoothConntionTool_ProjectTool");
            });

            modelBuilder.Entity<BoothPhotoPath>(entity =>
            {
                entity.ToTable("BoothPhotoPath");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PhotoPath).HasColumnName("Photo_Path");
            });

            modelBuilder.Entity<GallaryArt>(entity =>
            {
                entity.ToTable("GallaryArt");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ArtId).HasColumnName("Art_ID");

                entity.Property(e => e.GallaryId).HasColumnName("Gallary_ID");

                entity.HasOne(d => d.Art)
                    .WithMany(p => p.GallaryArts)
                    .HasForeignKey(d => d.ArtId)
                    .HasConstraintName("FK_GallaryArt_Art");

                entity.HasOne(d => d.Gallary)
                    .WithMany(p => p.GallaryArts)
                    .HasForeignKey(d => d.GallaryId)
                    .HasConstraintName("FK_GallaryArt_Gallery");
            });

            modelBuilder.Entity<GallaryBooth>(entity =>
            {
                entity.ToTable("GallaryBooth");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.BoothId).HasColumnName("Booth_ID");

                entity.Property(e => e.GallaryId).HasColumnName("Gallary_ID");

                entity.HasOne(d => d.Booth)
                    .WithMany(p => p.GallaryBooths)
                    .HasForeignKey(d => d.BoothId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GallaryBooth_Booth");

                entity.HasOne(d => d.Gallary)
                    .WithMany(p => p.GallaryBooths)
                    .HasForeignKey(d => d.GallaryId)
                    .HasConstraintName("FK_GallaryBooth_Gallery");
            });

            modelBuilder.Entity<GallaryMoodBoard>(entity =>
            {
                entity.ToTable("GallaryMoodBoard");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.GallaryId).HasColumnName("Gallary_ID");

                entity.Property(e => e.MoodBoardId).HasColumnName("MoodBoard_ID");

                entity.HasOne(d => d.Gallary)
                    .WithMany(p => p.GallaryMoodBoards)
                    .HasForeignKey(d => d.GallaryId)
                    .HasConstraintName("FK_GallaryMoodBoard_Gallery");

                entity.HasOne(d => d.MoodBoard)
                    .WithMany(p => p.GallaryMoodBoards)
                    .HasForeignKey(d => d.MoodBoardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GallaryMoodBoard_MoodBoard");
            });

            modelBuilder.Entity<Gallery>(entity =>
            {
                entity.ToTable("Gallery");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Galleries)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Gallery_Users");
            });

            modelBuilder.Entity<GalleryProject>(entity =>
            {
                entity.ToTable("GalleryProject");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.GallaryId).HasColumnName("Gallary_ID");

                entity.Property(e => e.ProjectId).HasColumnName("Project_ID");

                entity.HasOne(d => d.Gallary)
                    .WithMany(p => p.GalleryProjects)
                    .HasForeignKey(d => d.GallaryId)
                    .HasConstraintName("FK_GalleryProject_Gallery");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.GalleryProjects)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GalleryProject_Project");
            });

            modelBuilder.Entity<MoodBoard>(entity =>
            {
                entity.ToTable("MoodBoard");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Color).HasMaxLength(255);

                entity.Property(e => e.MoodBoardName)
                    .HasMaxLength(255)
                    .HasColumnName("Mood_Board_Name");
            });

            modelBuilder.Entity<MoodBoardConntionPhoto>(entity =>
            {
                entity.ToTable("MoodBoardConntionPhoto");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MoodBoardId).HasColumnName("Mood_Board_ID");

                entity.Property(e => e.MoodBoardPhotoPathId).HasColumnName("Mood_Board_Photo_Path_ID");

                entity.HasOne(d => d.MoodBoard)
                    .WithMany(p => p.MoodBoardConntionPhotos)
                    .HasForeignKey(d => d.MoodBoardId)
                    .HasConstraintName("FK_MoodBoardConntionPhoto_MoodBoard");

                entity.HasOne(d => d.MoodBoardPhotoPath)
                    .WithMany(p => p.MoodBoardConntionPhotos)
                    .HasForeignKey(d => d.MoodBoardPhotoPathId)
                    .HasConstraintName("FK_MoodBoardConntionPhoto_MoodBoardPhotoPath");
            });

            modelBuilder.Entity<MoodBoardConntionTemplate>(entity =>
            {
                entity.ToTable("MoodBoardConntionTemplate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MoodBoardId).HasColumnName("Mood_Board_ID");

                entity.Property(e => e.MoodBoardTemplateId).HasColumnName("Mood_Board_Template_ID");

                entity.HasOne(d => d.MoodBoard)
                    .WithMany(p => p.MoodBoardConntionTemplates)
                    .HasForeignKey(d => d.MoodBoardId)
                    .HasConstraintName("FK_MoodBoardConntionTemplate_MoodBoard");

                entity.HasOne(d => d.MoodBoardTemplate)
                    .WithMany(p => p.MoodBoardConntionTemplates)
                    .HasForeignKey(d => d.MoodBoardTemplateId)
                    .HasConstraintName("FK_MoodBoardConntionTemplate_MoodBoardTemplate");
            });

            modelBuilder.Entity<MoodBoardPhotoPath>(entity =>
            {
                entity.ToTable("MoodBoardPhotoPath");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MoodBoardPhotoPath1).HasColumnName("MoodBoard_Photo_Path");
            });

            modelBuilder.Entity<MoodBoardTemplate>(entity =>
            {
                entity.ToTable("MoodBoardTemplate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.NameTemplate)
                    .HasMaxLength(255)
                    .HasColumnName("Name_Template");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.SpaceHeight)
                    .HasColumnType("decimal(6, 3)")
                    .HasColumnName("Space_height");

                entity.Property(e => e.SpaceWidth)
                    .HasColumnType("decimal(6, 3)")
                    .HasColumnName("Space_width");


                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.CreatedDateUTC)
                      .HasColumnType("datetime")
                      .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<ProjectCategory>(entity =>
            {
                entity.ToTable("ProjectCategory");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CategoryName).HasMaxLength(255);
            });

            modelBuilder.Entity<ProjectConntionPhotoProjectPath>(entity =>
            {
                entity.ToTable("ProjectConntionPhotoProjectPath");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PhotoProjectId).HasColumnName("Photo_Project_ID");

                entity.Property(e => e.ProjectId).HasColumnName("Project_ID");

                entity.HasOne(d => d.PhotoProject)
                    .WithMany(p => p.ProjectConntionPhotoProjectPaths)
                    .HasForeignKey(d => d.PhotoProjectId)
                    .HasConstraintName("FK_ProjectConntionPhotoProjectPath_ProjectPhotoPath");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectConntionPhotoProjectPaths)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_ProjectConntionPhotoProjectPath_Project");
            });

            modelBuilder.Entity<ProjectConntionProjectCategory>(entity =>
            {
                entity.ToTable("ProjectConntionProjectCategory");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ProjectCategoryId).HasColumnName("ProjectCategory_ID");

                entity.Property(e => e.ProjectId).HasColumnName("Project_ID");

                entity.HasOne(d => d.ProjectCategory)
                    .WithMany(p => p.ProjectConntionProjectCategories)
                    .HasForeignKey(d => d.ProjectCategoryId)
                    .HasConstraintName("FK_ProjectConntionProjectCategory_ProjectCategory");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectConntionProjectCategories)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_ProjectConntionProjectCategory_Project");
            });

            modelBuilder.Entity<ProjectConntionTool>(entity =>
            {
                entity.ToTable("ProjectConntionTool");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ProjectId).HasColumnName("Project_ID");

                entity.Property(e => e.ProjectToolId).HasColumnName("ProjectTool_ID");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectConntionTools)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_ProjectConntionTool_Project");

                entity.HasOne(d => d.ProjectTool)
                    .WithMany(p => p.ProjectConntionTools)
                    .HasForeignKey(d => d.ProjectToolId)
                    .HasConstraintName("FK_ProjectConntionTool_ProjectTool");
            });

            modelBuilder.Entity<ProjectPhotoPath>(entity =>
            {
                entity.ToTable("ProjectPhotoPath");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ProjectPhotoPath1).HasColumnName("Project_Photo_Path");
            });

            modelBuilder.Entity<ProjectTool>(entity =>
            {
                entity.ToTable("ProjectTool");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.NameTool).HasMaxLength(255);
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.ArchitectureID)
                    .HasColumnName("architecture_iD")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.Birthday)
                    .HasColumnType("datetime")
                    .HasColumnName("birthday")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.City)
                    .HasMaxLength(255)
                    .HasColumnName("city")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ConfirmaitonDateDue).HasColumnType("datetime");

                entity.Property(e => e.ConfirmationCode)
                    .HasMaxLength(255)
                    .HasColumnName("confirmationCode")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Country)
                    .HasMaxLength(255)
                    .HasColumnName("country")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Descirption).HasDefaultValueSql("('')");

                entity.Property(e => e.FirstNameUser).HasDefaultValueSql("('')");

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .HasColumnName("image");

                entity.Property(e => e.IsArchitect)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.IsCompleted)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.IsConfirmed)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.LastNameUser).HasDefaultValueSql("('')");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(255)
                    .HasColumnName("phoneNumber")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<UserEducation>(entity =>
            {
                entity.ToTable("UserEducation");

                entity.Property(e => e.AcademicDegree).HasMaxLength(255);

                entity.Property(e => e.Descrpiction).HasMaxLength(255);

                entity.Property(e => e.FacultyName).HasMaxLength(255);

                entity.Property(e => e.UniversityName).HasMaxLength(255);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserEducations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("User_UserEduction");
            });

            modelBuilder.Entity<UserExperience>(entity =>
            {
                entity.ToTable("UserExperience");

                entity.Property(e => e.Activity).HasColumnName("activity");

                entity.Property(e => e.City)
                    .HasMaxLength(255)
                    .HasColumnName("city");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(255)
                    .HasColumnName("companyName");

                entity.Property(e => e.Country)
                    .HasMaxLength(255)
                    .HasColumnName("country");

                entity.Property(e => e.FromDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fromDate");

                entity.Property(e => e.Specialization)
                    .HasMaxLength(255)
                    .HasColumnName("specialization");

                entity.Property(e => e.ToDate)
                    .HasColumnType("datetime")
                    .HasColumnName("toDate");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserExperiences)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_userExperince");
            });

            modelBuilder.Entity<UserLanguage>(entity =>
            {
                entity.ToTable("UserLanguage");

                entity.Property(e => e.Language)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLanguages)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userLanguage_userID");
            });

            modelBuilder.Entity<UserSkill>(entity =>
            {
                entity.Property(e => e.SkillId).HasColumnName("skillId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.UserSkills)
                    .HasForeignKey(d => d.SkillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserSkills_Skills");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserSkills)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserSkills_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
