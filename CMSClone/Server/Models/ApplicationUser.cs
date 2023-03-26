using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace CMSClone.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        public string? DisplayName { get; set; }

        public virtual ICollection<Course>? CoursesCreated { get; set; }
        public virtual ICollection<CourseJoin>? CoursesJoin { get; set; }
        public virtual ICollection<Submit>? AssignmentsSubmitted { get; set; }

    }
}