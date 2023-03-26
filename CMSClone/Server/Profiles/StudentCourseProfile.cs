using AutoMapper;
using CMSClone.Server.Models;
using CMSClone.Shared.DTOs;

namespace CMSClone.Server.Profiles
{
    public class StudentCourseProfile : Profile
    {
        public StudentCourseProfile()
        {
            CreateMap<CourseJoinDTO, StudentsCourse>().ForMember(c => c.CourseId,
                opt => opt.MapFrom(src => src.CourseId))
                .ForMember(c => c.StudentId,
                opt => opt.MapFrom(src => src.UserId));
        }
    }
}
