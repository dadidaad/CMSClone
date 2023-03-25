﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSClone.Shared.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CourseId { get; set; }
        [Required]
        [MaxLength(10)]
        public string? CourseCode { get; set; }
        [Required]
        [MaxLength(255)]
        public string? CourseName { get; set; }  

    }
}
