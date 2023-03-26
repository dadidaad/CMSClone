using CMSClone.Server.Data;
using CMSClone.Server.Models;
using CMSClone.Server.Repositories.Extensions;
using CMSClone.Shared;
using Microsoft.EntityFrameworkCore;

namespace CMSClone.Server.Repositories.Implements
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AssignmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(Assignment Assignment)
        {
            _context.Assignments.Remove(Assignment);
            await _context.SaveChangesAsync();
        }

        public Assignment GetAssignment(Guid assignmentId)
        {
            return _context.Assignments.FirstOrDefault(c => c.AssignmentId == assignmentId);
        }

        public IEnumerable<Assignment> GetAssignments(string courseId)
        {
            return _context.Assignments.Where(c => c.CourseId.Equals(courseId)).ToList();
        }


        public async Task Insert(Assignment Assignment)
        {
            _context.Assignments.Add(Assignment);
            await _context.SaveChangesAsync();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task Update(Assignment Assignment)
        {
            _context.Assignments.Update(Assignment);
            await _context.SaveChangesAsync();
        }
    }
}
