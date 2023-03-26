using CMSClone.Server.Models;

namespace CMSClone.Server.Repositories
{
    public interface IAssignmentRepository
    {
        public Assignment GetAssignment(Guid assignmentId);
        public IEnumerable<Assignment> GetAssignments(string courseId);
        public Task Insert(Assignment assignment);
        public Task Update(Assignment assignment);
        public Task Delete(Assignment assignment);
    }
}
