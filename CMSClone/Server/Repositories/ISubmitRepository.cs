using CMSClone.Server.Models;
using CMSClone.Shared;

namespace CMSClone.Server.Repositories
{
    public interface ISubmitRepository
    {
        public IEnumerable<Submit> GetAllSubmits(Guid assignmentID, Guid studentID);
        public Submit GetSubmit(Guid submitId);
        public Task Insert(Submit Submit);
        public Task Update(Submit Submit);
        public Task Delete(Submit Submit);
    }
}
