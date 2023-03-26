using CMSClone.Server.Data;
using CMSClone.Server.Models;
using CMSClone.Server.Repositories.Extensions;
using CMSClone.Shared;
using Microsoft.EntityFrameworkCore;

namespace CMSClone.Server.Repositories.Implements
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(Course course)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return _context.Courses.ToList();
        }

        public Course GetCourse(Guid courseId)
        {
            return _context.Courses.FirstOrDefault(c => c.CourseId == courseId) ;
        }

        public IEnumerable<Course> GetCourses(string courseCode)
        {
            return _context.Courses.Where(c => c.CourseCode.Contains(courseCode)).ToList();
        }

        public async Task<PagedList<Course>> GetCoursesByCreator(string creatorId, RequestParameters requestParameters)
        {
            var courses = await _context.Courses.Include(c => c.Creator)
                .Search(requestParameters.SearchTerm)
                .Sort(requestParameters.OrderBy).ToListAsync();
            return PagedList<Course>.ToPagedList(courses, requestParameters.PageNumber, requestParameters.PageSize);
        }

        public async Task Insert(Course course)
        {
             _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task Update(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }
    }
}
