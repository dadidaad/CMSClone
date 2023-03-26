using AutoMapper;
using CMSClone.Server.Models;
using CMSClone.Shared;
using CMSClone.Shared.Models;

namespace CMSClone.Server.Profiles
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseDTO>().ForMember(c => c.CreatorName,
                opt => opt.MapFrom(src => src.Creator.DisplayName));

            CreateMap<CourseDTO, Course>();
            CreateMap<PagedList<Course>, PagedList<CourseDTO>>().ConvertUsing<PagedListConverter<Course, CourseDTO>>();
        }
    }
}
