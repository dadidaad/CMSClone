using CMSClone.Server.Data;
using CMSClone.Server.Models;
using CMSClone.Server.Repositories.Extensions;
using CMSClone.Shared;
using Microsoft.EntityFrameworkCore;

namespace CMSClone.Server.Repositories.Implements
{
    public class FileUploadRepository : IFileUploadRepository
    {
        private readonly ApplicationDbContext _context;

        public FileUploadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(FileUpload FileUpload)
        {
            _context.FileUploads.Remove(FileUpload);
            await _context.SaveChangesAsync();
        }

        public FileUpload GetFileUpload(Guid FileUploadId)
        {
            return _context.FileUploads.FirstOrDefault(c => c.FileUploadID.Equals(FileUploadId));
        }

        public IEnumerable<FileUpload> GetFileUploads(Guid submitId)
        {
            return _context.FileUploads.Where(c => c.SubmitID.Equals(submitId));
        }

        public async Task Insert(FileUpload FileUpload)
        {
            _context.FileUploads.Add(FileUpload);
            await _context.SaveChangesAsync();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task Update(FileUpload FileUpload)
        {
            _context.FileUploads.Update(FileUpload);
            await _context.SaveChangesAsync();
        }
    }
}
