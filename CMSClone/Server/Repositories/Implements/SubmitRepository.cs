using CMSClone.Server.Data;
using CMSClone.Server.Models;
using CMSClone.Server.Repositories.Extensions;
using CMSClone.Shared;
using Microsoft.EntityFrameworkCore;

namespace CMSClone.Server.Repositories.Implements
{
    public class SubmitRepository : ISubmitRepository
    {
        private readonly ApplicationDbContext _context;

        public SubmitRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(Submit Submit)
        {
            _context.Submits.Remove(Submit);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Submit> GetAllSubmits(Guid assignmentID, Guid studentID)
        {
            var submits = _context.Submits.Where(c => c.AssignmentId.Equals(assignmentID));
            if (studentID != null)
            {
                submits.Where(c => c.StudentId.Equals(studentID));
            }
            return submits;
        }



        public async Task Insert(Submit Submit)
        {
            _context.Submits.Add(Submit);
            await _context.SaveChangesAsync();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task Update(Submit Submit)
        {
            _context.Submits.Update(Submit);
            await _context.SaveChangesAsync();
        }

        Submit ISubmitRepository.GetSubmit(Guid submitId)
        {
            return _context.Submits.FirstOrDefault(c => c.SubmitId.Equals(submitId));
        }

    }
}
