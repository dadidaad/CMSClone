using AutoMapper;
using CMSClone.Client.Pages;
using CMSClone.Server.Models;
using CMSClone.Server.Repositories;
using CMSClone.Shared;
using CMSClone.Shared.DTOs;
using CMSClone.Shared.Models;
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
    [AllowAnonymous]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseJoinRepository _courseStudentRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public CoursesController(ICourseRepository courseRepository, ICourseJoinRepository courseStudentRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _courseStudentRepository = courseStudentRepository;
            _userManager = userManager; 
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<Course> GetAll()
        {
            return _courseRepository.GetAllCourses();
        }

        [HttpGet]
        [Route("getcourses")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCourses([FromQuery] RequestParameters requestParameters)
        {
            var courses = await _courseRepository.GetCourses(requestParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(courses.MetaData));
            var coursesDTO = _mapper.Map<PagedList<CourseDTO>>(courses);
            return Ok(coursesDTO);
        }

        [HttpGet]
        [Route("getcoursesforcreator")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GetCoursesForCreator([FromQuery] RequestParameters requestParameters)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var courses = await _courseRepository.GetCoursesByCreator(currentUserId, requestParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(courses.MetaData));
            var coursesDTO = _mapper.Map<PagedList<CourseDTO>>(courses);
            return Ok(coursesDTO);
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Insert([FromBody]CourseDTO courseDTO)
        {
            if(courseDTO == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return StatusCode(420, "Invalid data");
            }
            var course = _mapper.Map<Course>(courseDTO);
            course.CourseId = Guid.NewGuid();
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            course.CreatorId = currentUserId;

            await _courseRepository.Insert(course);
            var courseJoinDTO = new CourseJoinDTO()
            {
                CourseId = course.CourseId,
                UserId = currentUserId
            };
            var studentCourse = _mapper.Map<CourseJoin>(courseJoinDTO);
            await _courseStudentRepository.Insert(studentCourse);
            return Created("Insert", courseDTO);
        }

        [HttpPut]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Update([FromBody] CourseDTO courseDTO)
        {
            if (courseDTO == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return StatusCode(420, "Invalid data");
            }
            var course = _mapper.Map<Course>(courseDTO);
            await _courseRepository.Update(course);
            return Ok(course);
        }
    }
}
