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
    public class SubmitController : Controller
    {
        private readonly ISubmitRepository _submitRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public SubmitController(ISubmitRepository SubmitRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _submitRepository = SubmitRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<Submit> GetAll(Guid assignmentId, Guid studentId)
        {
            return _submitRepository.GetAllSubmits(assignmentId, studentId);
        }

        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Insert([FromBody] Submit submit)
        {
            if (submit == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return StatusCode(420, "Invalid data");
            }

            submit = _mapper.Map<Submit>(submit);
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            submit.StudentId = currentUserId;
            await _submitRepository.Insert(submit);
 
            return Created("Insert", submit);
        }

        [HttpPut]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Update([FromBody] Submit submit)
        {
            if (submit == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return StatusCode(420, "Invalid data");
            }
            submit = _mapper.Map<Submit>(submit);
            await _submitRepository.Update(submit);
            return Ok(submit);
        }
    }
}
