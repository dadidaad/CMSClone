using CMSClone.Server.Data;
using CMSClone.Server.Models;
using CMSClone.Server.Repositories.Extensions;
using CMSClone.Shared;
using CMSClone.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace CMSClone.Server.Repositories.Implements
{
    public class CourseJoinRepository : ICourseJoinRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseJoinRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CourseJoin>> GetAllCoursesJoinOfUser(string id)
        {
            var courseJoin = await _context.CourseJoins.Include(sc => sc.Course)
                .Include(sc => sc.User).Where(sc => sc.UserId == id).ToListAsync();
            return courseJoin;
        }

        public async Task<PagedList<CourseJoin>> GetCoursesJoin(string id, RequestParameters requestParameters)
        {
            var courseJoin = await _context.CourseJoins.Include(sc => sc.Course)
                .Include(sc => sc.User).Where(sc => sc.UserId == id)
                .Search(requestParameters.SearchTerm).ToListAsync();
            return PagedList<CourseJoin>.ToPagedList(courseJoin, requestParameters.PageNumber, requestParameters.PageSize);

        }

        public async Task<List<CourseJoin>> GetTeacherInCourse(Guid courseId, List<ApplicationUser> teachers)
        {

            var teachersInCourse = await _context.CourseJoins.Include(cj => cj.User)
            .Where(cj => teachers.Contains(cj.User))
            .Where(cj => cj.CourseId == courseId)
            .ToListAsync();
            return teachersInCourse;
        }


        public async Task Insert(CourseJoin courseJoin)
        {
            try
            {
                _context.CourseJoins.Add(courseJoin);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
