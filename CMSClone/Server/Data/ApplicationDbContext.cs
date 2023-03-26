using CMSClone.Server.Models;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        public DbSet<Course>? Courses { get; set; }
        public DbSet<Assignment>? Assignments { get; set; }
        public DbSet<FileUpload>? FileUploads { get; set; }
        public DbSet<CourseJoin>? CourseJoins { get; set; }
        public DbSet<Submit>? Submits  { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.Entity<Course>().HasAlternateKey(c => c.CourseCode).HasName("AlternateKey_CourseCode");
            builder.Entity<Course>().HasOne(c => c.Creator)
                .WithMany(g => g.CoursesCreated).HasForeignKey(s => s.CreatorId).IsRequired();
            builder.Entity<FileUpload>().HasOne(c => c.AssignmentBelongTo)
                .WithMany(g => g.FilesUploadGiven).HasForeignKey(s => s.AssignmentBelongToId).IsRequired();
            builder.Entity<Assignment>().HasOne(c => c.Course)
                .WithMany(g => g.Assignments).HasForeignKey(s => s.CourseId).IsRequired();
            builder.Entity<Submit>().HasKey(x => new { x.StudentId, x.AssignmentId, x.FileUploadId });
            builder.Entity<Submit>().HasOne(c => c.Student)
                .WithMany(c => c.AssignmentsSubmitted)
                .HasForeignKey(s => s.StudentId).IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Submit>().HasOne(c => c.Assignment)
                .WithMany(c => c.Submits)
                .HasForeignKey(s => s.AssignmentId).IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Submit>().HasOne(c => c.FileUploadSubmitted)
                .WithMany(c => c.SubmitFiles)
                .HasForeignKey(s => s.FileUploadId).IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<CourseJoin>().HasKey(x => new { x.UserId, x.CourseId});

            builder.Entity<CourseJoin>().HasOne(s => s.User)
                .WithMany(c => c.CoursesJoin)
                .HasForeignKey(s => s.UserId).IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<CourseJoin>().HasOne(s => s.Course)
                .WithMany(c => c.UsersJoin)
                .HasForeignKey(s => s.CourseId).IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}