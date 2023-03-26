using CMSClone.Server.Models;
using CMSClone.Shared;
using CMSClone.Shared.DTOs;

namespace CMSClone.Server.Repositories
{
    public interface ICourseJoinRepository
    {
        public Task<List<CourseJoin>> GetAllCoursesJoinOfUser(string id);
        public Task<PagedList<CourseJoin>> GetCoursesJoin(string id, RequestParameters requestParameters);
        public Task<List<CourseJoin>> GetTeacherInCourse(Guid courseId, List<ApplicationUser> teachers);
        public Task Insert(CourseJoin courseJoin);
    }
}
