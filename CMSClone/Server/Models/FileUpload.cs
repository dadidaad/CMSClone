﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSClone.Server.Models
{
    public class FileUpload
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid FileUploadID { get; set; }
        [Required]
        public Guid SubmitID { get; set; }
        [MaxLength(255)]
        public string? FileUploadName { get; set; }

        [Required]
        [MaxLength(255)]
        public string? FileUploadPath { get; set; }

        [Required]
        [MaxLength(10)]
        public string? FileUploadExtensions { get; set; }

        public virtual Submit? Submit { get; set; }
    }
}
