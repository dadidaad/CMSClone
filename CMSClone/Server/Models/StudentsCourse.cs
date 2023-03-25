namespace CMSClone.Server.Models
{
    public class StudentsCourse
    {

        public string? StudentId { get; set; } 
        public ApplicationUser? Student { get; set; }

        public Guid? CourseId { get; set; }
        public Course? Course { get; set; }
    }
}
