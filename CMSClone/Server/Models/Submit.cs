using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMSClone.Server.Models
{
    public partial class Submit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SubmitId { get; set; }
     
        public string? StudentId { get; set; }

        public Guid? AssignmentId { get; set; }
        public DateTime? SubmittedTime { get; set; }

        public virtual ApplicationUser? Student { get; set; }
        public virtual Assignment? Assignment { get; set; }
        public virtual ICollection<FileUpload>? FileUploadSubmitted { get; set; }

    }
}
