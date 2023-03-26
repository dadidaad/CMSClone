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
using CourseJoin = CMSClone.Server.Models.CourseJoin;

namespace CMSClone.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public class CourseJoinController : ControllerBase
    {
        private readonly ICourseJoinRepository _courseJoinRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public CourseJoinController(ICourseJoinRepository courseJoinRepository, ICourseRepository courseRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _courseJoinRepository = courseJoinRepository;
            _courseRepository = courseRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getallcoursesjoin")]
        public async Task<IActionResult> GetAllCoursesJoin()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var courseJoin = await _courseJoinRepository.GetAllCoursesJoinOfUser(currentUserId);
            return Ok(courseJoin);
        }
        [HttpGet]
        [Route("getcoursesjoin")]
        public async Task<IActionResult> GetCoursesJoin([FromQuery]RequestParameters requestParameters)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var courseJoin = await _courseJoinRepository.GetCoursesJoin(currentUserId, requestParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(courseJoin.MetaData));
            var courseJoinDTO = _mapper.Map<PagedList<CourseJoinDTO>>(courseJoin);
            return Ok(courseJoinDTO);
        }
        [HttpGet]
        [Route("getcoursesjoinwithteacher")]
        public async Task<IActionResult> GetCoursesJoinWithTeacher(Guid courseId)
        {
            var teacher = await _userManager.GetUsersInRoleAsync("Teacher");
            var courseJoin = await _courseJoinRepository.GetTeacherInCourse(courseId, teacher.ToList());
            var courseJoinDT0 = _mapper.Map<List<CourseJoinDTO>>(courseJoin);
            return Ok(courseJoinDT0);
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
            var studentCourse = _mapper.Map<CourseJoin>(courseJoinDTO);
            await _courseJoinRepository.Insert(studentCourse);
            return Created("", courseJoinDTO);

        }
    }
}
