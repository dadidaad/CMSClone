using CMSClone.Server.Models;

namespace CMSClone.Server.Repositories
{
    public interface IFileUploadRepository
    {
        public FileUpload GetFileUpload(Guid FileUploadId);
        public IEnumerable<FileUpload> GetFileUploads(Guid SubmitId);
        public Task Insert(FileUpload FileUpload);
        public Task Update(FileUpload FileUpload);
        public Task Delete(FileUpload FileUpload);
    }
}
