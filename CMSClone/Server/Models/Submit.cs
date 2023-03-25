using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMSClone.Server.Models
{
    public partial class Submit
    {
        [Key, Column(Order = 1)]
        public string? StudentId { get; set; }
        [Key, Column(Order = 2)]
        public Guid FileUploadId { get; set; }
        [Key, Column(Order = 3)]
        public Guid? AssignmentId { get; set; }
        public DateTime? SubmittedTime { get; set; }

        public virtual ApplicationUser? Student { get; set; }
        public virtual Assignment? Assignment { get; set; }
        public virtual FileUpload? FileUploadSubmitted { get; set; }

    }
}
