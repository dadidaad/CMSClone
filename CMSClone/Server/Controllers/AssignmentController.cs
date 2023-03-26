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
    public class AssignmentController : Controller
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public AssignmentController(IAssignmentRepository assignmentRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _assignmentRepository = assignmentRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<Assignment> GetAll(String courseId)
        {
            return _assignmentRepository.GetAssignments(courseId);
        }

        [HttpGet]
        public Assignment GetAssignment(Guid assignmentId)
        {
            return _assignmentRepository.GetAssignment(assignmentId);
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Insert([FromBody] Assignment assignment)
        {
            if (assignment == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return StatusCode(420, "Invalid data");
            }

            assignment = _mapper.Map<Assignment>(assignment);

            await _assignmentRepository.Insert(assignment);
 
            return Created("Insert", assignment);
        }

        [HttpPut]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Update([FromBody] Assignment assignment)
        {
            if (assignment == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return StatusCode(420, "Invalid data");
            }
            assignment = _mapper.Map<Assignment>(assignment);
            await _assignmentRepository.Update(assignment);
            return Ok(assignment);
        }
    }
}
