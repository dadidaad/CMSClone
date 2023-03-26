using CMSClone.Server.Models;
using CMSClone.Shared;

namespace CMSClone.Server.Repositories
{
    public interface ICourseRepository
    {
        public IEnumerable<Course> GetAllCourses();
        public Course GetCourse(Guid courseId);
        public IEnumerable<Course> GetCourses(string courseCode);
        public Task<PagedList<Course>> GetCoursesByCreator(string creatorId, RequestParameters requestParameters);
        public Task Insert(Course course);
        public Task Update(Course course);
        public Task Delete(Course course);
    }
}
