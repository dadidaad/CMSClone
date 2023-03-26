using Microsoft.AspNetCore.Identity;

namespace CMSClone.Server.Models
{
    public class Role : IdentityRole
    {
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
