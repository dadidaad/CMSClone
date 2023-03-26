using AutoMapper;
using CMSClone.Server.Models;
using CMSClone.Shared.DTOs;

namespace CMSClone.Server.Profiles
{
    public class UserProfle : Profile
    {
        public UserProfle()
        {
            CreateMap<ApplicationUser, Teacher>();
        }
    }
}
