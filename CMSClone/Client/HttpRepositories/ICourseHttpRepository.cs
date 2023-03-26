using CMSClone.Client.Features;
using CMSClone.Shared;
using CMSClone.Shared.Models;

namespace CMSClone.Client.HttpRepositories
{
    public interface ICourseHttpRepository
    {
        Task CreateCourse(CourseDTO courseDTO);
        Task<PagingRespone<CourseDTO>> GetCourseByCreator(RequestParameters requestParameters);
    }
}
