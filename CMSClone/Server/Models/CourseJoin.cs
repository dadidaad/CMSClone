namespace CMSClone.Server.Models
{
    public class CourseJoin
    {

        public string UserId { get; set; } 
        public virtual ApplicationUser User { get; set; }

        public Guid? CourseId { get; set; }
        public virtual Course? Course { get; set; }
    }
}
