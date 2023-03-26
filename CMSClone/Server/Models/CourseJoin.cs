using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMSClone.Server.Models
{
    public class CourseJoin
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; } 
        public virtual ApplicationUser User { get; set; }

        public Guid? CourseId { get; set; }
        public virtual Course? Course { get; set; }
    }
}
