using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSClone.Shared.Models
{
    public class Assignment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AssignmentId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime DueDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string? SubmissionType { get; set; }
        [Required]
        [ForeignKey("Course")]
        public Guid CourseId { get; set; }
        public Course? Course { get; set; }

        public virtual ICollection<FileUpload>? FilesUploadGiven { get; set; }
    }
}
