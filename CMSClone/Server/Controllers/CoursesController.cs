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

namespace CMSClone.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseStudentRepository _courseStudentRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public CoursesController(ICourseRepository courseRepository, ICourseStudentRepository courseStudentRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
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
        public IEnumerable<Course> GetCoursesByCode(string courseCode)
        {
            return _courseRepository.GetCourses(courseCode);
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
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            course.CreatorId = currentUserId;
            await _courseRepository.Insert(course);
            var courseJoinDTO = new CourseJoinDTO()
            {
                CourseId = course.CourseId,
                UserId = currentUserId
            };
            var studentCourse = _mapper.Map<StudentsCourse>(courseJoinDTO);
            await _courseStudentRepository.Insert(studentCourse);
            return Created("Insert", course);
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
