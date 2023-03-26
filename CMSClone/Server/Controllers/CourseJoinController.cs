using AutoMapper;
using CMSClone.Client.Pages;
using CMSClone.Server.Models;
using CMSClone.Server.Repositories;
using CMSClone.Shared;
using CMSClone.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace CMSClone.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public class CourseJoinController : ControllerBase
    {
        private readonly ICourseStudentRepository _courseStudentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public CourseJoinController(ICourseStudentRepository courseStudentRepository, ICourseRepository courseRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _courseStudentRepository = courseStudentRepository;
            _courseRepository = courseRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getcoursesjoin")]
        public async Task<IActionResult> GetCoursesJoin([FromQuery]RequestParameters requestParameters)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var courseJoin = await _courseStudentRepository.GetCoursesJoin(currentUserId, requestParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(courseJoin.MetaData));
            return Ok(courseJoin);
        }
        [HttpGet]
        [Route("getcoursesjoinwithteacher")]
        public async Task<IActionResult> GetCoursesJoinWithTeacher(string courseCode)
        {
            var courseJoin = await _courseStudentRepository.GetTeacherInCourse(courseCode);
            return Ok(courseJoin);
        }
        [HttpPost]
        public async Task<IActionResult> Join([FromBody] Guid courseId)
        {
            var courseGet = _courseRepository.GetCourse(courseId);
            if(courseGet == null)
            {
                return NotFound();
            }
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var courseJoinDTO = new CourseJoinDTO()
            {
                CourseId = courseId,
                UserId = currentUserId
            };
            var studentCourse = _mapper.Map<StudentsCourse>(courseJoinDTO);
            await _courseStudentRepository.Insert(studentCourse);
            return Created("", courseJoinDTO);

        }
    }
}
