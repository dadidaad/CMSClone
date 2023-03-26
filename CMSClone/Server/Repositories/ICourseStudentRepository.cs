using CMSClone.Server.Models;
using CMSClone.Shared;

namespace CMSClone.Server.Repositories
{
    public interface ICourseStudentRepository
    {

        public Task<PagedList<StudentsCourse>> GetCoursesJoin(string id, RequestParameters requestParameters);
        public Task<List<StudentsCourse>> GetTeacherInCourse(string courseId);
        public Task Insert(StudentsCourse studentsCourse);
    }
}
