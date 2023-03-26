using CMSClone.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSClone.Shared.Models
{
    public class CourseDTO
    {
        public Guid CourseId;
        [Required]
        [MaxLength(10, ErrorMessage = "Name is too long.")]
        public string? CourseCode { get; set; }
        [Required]
        [MaxLength(255)]
        public string? CourseName { get; set; }  

        public string? CreatorName { get; set; }

        public virtual List<Teacher>? Teachers { get; set; }
    }
}
