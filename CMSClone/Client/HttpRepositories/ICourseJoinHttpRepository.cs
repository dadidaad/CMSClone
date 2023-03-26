using CMSClone.Client.Features;
using CMSClone.Shared;
using CMSClone.Shared.DTOs;

namespace CMSClone.Client.HttpRepositories
{
    public interface ICourseJoinHttpRepository
    {
        public Task<List<CourseJoinDTO>> GetTeacherInCourse(Guid courseId);
        public Task<PagingRespone<CourseJoinDTO>> GetCourseJoin(RequestParameters requestParameters);
    }
}
