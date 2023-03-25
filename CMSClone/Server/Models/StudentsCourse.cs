namespace CMSClone.Server.Models
{
    public class StudentsCourse
    {

        public string StudentId { get; set; } 
        public virtual ApplicationUser Student { get; set; }

        public Guid? CourseId { get; set; }
        public virtual Course? Course { get; set; }
    }
}
