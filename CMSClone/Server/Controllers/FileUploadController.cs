using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CMSClone.Server.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using CMSClone.Server.Models;
using System.Security.Claims;

namespace CMSClone.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class FileUploadController : Controller
    {
        private readonly IFileUploadRepository _FileUploadRepository;
        private readonly ISubmitRepository _submitRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public FileUploadController(IFileUploadRepository FileUploadRepository, ISubmitRepository submitRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _FileUploadRepository = FileUploadRepository;
            _userManager = userManager;
            _submitRepository = submitRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<FileUpload> GetAll(Guid submitId)
        {
            return _FileUploadRepository.GetFileUploads(submitId);
        }

        [HttpPost]
       /* [Authorize(Roles = "Student")]*/
        public async Task<IActionResult> Insert([FromBody] FileUpload fileUpload, Guid submitId)
        {
            if (submitId == Guid.Empty)
            {
                var submit = new Submit(){
                    StudentId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };

                await _submitRepository.Insert(submit);

                submitId = submit.SubmitId;
            }

            if (fileUpload == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return StatusCode(420, "Invalid data");
            }

            fileUpload = _mapper.Map<FileUpload>(fileUpload);
            fileUpload.SubmitID = submitId;
            await _FileUploadRepository.Insert(fileUpload);

            return Created("Insert", fileUpload);
        }

        [HttpPut]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Update([FromBody] FileUpload FileUpload)
        {
            if (FileUpload == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return StatusCode(420, "Invalid data");
            }
            FileUpload = _mapper.Map<FileUpload>(FileUpload);
            await _FileUploadRepository.Update(FileUpload);
            return Ok(FileUpload);
        }
        [HttpPut]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Remove([FromBody] FileUpload FileUpload)
        {
            if (FileUpload == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return StatusCode(420, "Invalid data");
            }
            FileUpload = _mapper.Map<FileUpload>(FileUpload);
            await _FileUploadRepository.Delete(FileUpload);
            return Ok(FileUpload);
        }
    }
}
