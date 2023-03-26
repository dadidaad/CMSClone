using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSClone.Shared.DTOs
{
    public class CourseJoinDTO
    {
        public string UserId { get; set; }
        public Guid CourseId { get; set; }
    }
}
