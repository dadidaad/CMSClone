using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSClone.Server.Models
{
    public class Assignment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AssignmentId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string? SubmissionType { get; set; }


        public Guid CourseId { get; set; }
        public virtual Course? Course { get; set; }

        [Range(0.0d, 10.0d)]
        public double GivenGrade { get; set; }

        public virtual ICollection<Submit>? Submits { get; set; }
    }
}
