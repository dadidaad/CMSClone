using CMSClone.Server.Models;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection.Emit;

namespace CMSClone.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<FileUpload> FileUploads { get; set; }
        public DbSet<StudentsCourse> StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.Entity<Course>().HasAlternateKey(c => c.CourseCode).HasName("AlternateKey_CourseCode");
            builder.Entity<Course>().HasOne<ApplicationUser>(c => c.Creator)
                .WithMany(g => g.CoursesCreated).HasForeignKey(s => s.CreatorId).IsRequired();
            builder.Entity<StudentsCourse>().HasKey(x => new { x.StudentId, x.CourseId });
        }
    }
}