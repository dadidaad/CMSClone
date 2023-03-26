using CMSClone.Server.Data;
using CMSClone.Server.Models;
using CMSClone.Server.Repositories.Extensions;
using CMSClone.Shared;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace CMSClone.Server.Repositories.Implements
{
    public class CourseStudentRepository : ICourseStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseStudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PagedList<StudentsCourse>> GetCoursesJoin(string id, RequestParameters requestParameters)
        {
            var courseJoin = await _context.StudentCourses.Include(sc => sc.Course)
                .Include(sc => sc.Student).Where(sc => sc.StudentId == id)
                .Search(requestParameters.SearchTerm).ToListAsync();
            return PagedList<StudentsCourse>.ToPagedList(courseJoin, requestParameters.PageNumber, requestParameters.PageSize);
                
        }

        public async Task<List<StudentsCourse>> GetTeacherInCourse(string courseId)
        {
            var courseJoin = await _context.StudentCourses.Include(sc => sc.Course).Include(sc => sc.Student).ThenInclude(sc => sc.UserRoles).ThenInclude(ur => ur.Role)
                .Where(sc => sc.Student.UserRoles.Any(ur => ur.Role.Name == "Teacher" && sc.Course.CourseCode == courseId)).ToListAsync();
            return courseJoin;
        }


        public async Task Insert(StudentsCourse studentsCourse)
        {
            _context.StudentCourses.Add(studentsCourse);
            await _context.SaveChangesAsync();
        }
    }
}
