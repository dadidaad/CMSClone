using AutoMapper;
using CMSClone.Server.Models;
using CMSClone.Shared;
using CMSClone.Shared.DTOs;
using CMSClone.Shared.Models;

namespace CMSClone.Server.Profiles
{
    public class CourseJoinProfile : Profile
    {
        public CourseJoinProfile()
        {
            CreateMap<CourseJoinDTO, CourseJoin>().ForMember(c => c.CourseId,
                opt => opt.MapFrom(src => src.CourseId))
                .ForMember(c => c.UserId,
                opt => opt.MapFrom(src => src.UserId));
            CreateMap<CourseJoin, CourseJoinDTO>().ForMember(c => c.Teacher,
                opt => opt.MapFrom(src => src.User))
                .ForPath(dest => dest.Teacher.Id, opt => opt.MapFrom(src => src.User.Id))
                .ForPath(dest => dest.Teacher.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForPath(dest => dest.Teacher.DisplayName, opt => opt.MapFrom(src => src.User.DisplayName))
                .ForMember(c => c.CourseDTO,
                opt => opt.MapFrom(src => src.Course));
            CreateMap<PagedList<CourseJoin>, PagedList<CourseJoinDTO>>().ConvertUsing<PagedListConverter<CourseJoin, CourseJoinDTO>>();
        }
    }
}
