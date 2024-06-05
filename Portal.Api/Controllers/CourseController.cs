using _20201132039_SinavPortali.Dtos;
using _20201132039_SinavPortali.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace _20201132039_SinavPortali.Controllers
{
    [Authorize]
    [Route("api/Course/[Action]")]
    [ApiController]
    public class CourseController
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        Response _resultDto = new Response();

        public CourseController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public List<CourseDto> GetAll()
        {
            var courses = _context.Course.ToList();
            var courseDtos = _mapper.Map<List<CourseDto>>(courses);
            return courseDtos;
        }

        [HttpGet("{id}")]
        public CourseDto GetCourseById(int id)
        {
            var course = _context.Course.Where(s => s.Id == id).SingleOrDefault();
            var courseDto = _mapper.Map<CourseDto>(course);
            return courseDto;
        }
        [HttpGet("{id}")]
        public List<CourseDto> GetCourseByStudent(string id)
        {
            var relations = _context.AppUserCourse.Where(s => s.AppUserId == id).ToArray();
            List<CourseDto> courses = new();
            foreach (var rel in relations) 
            {
                courses.Add(GetCourseById(rel.CourseId));
            }
            return courses;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public Response AddCourse(CourseDto dto)
        {
            if (_context.Course.Count(c => c.Title == dto.Title) > 0)
            {
                _resultDto.Status = false;
                _resultDto.Message = "Girilen Ders İsmi Kayıtlıdır!";
                return _resultDto;
            }

            var course = _mapper.Map<Course>(dto);
            _context.Course.Add(course);
            _context.SaveChanges();

            _resultDto.Status = true;
            _resultDto.Message = "Ders Eklendi";
            return _resultDto;
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public Response Put(CourseDto dto)
        {
            var course = _context.Course.Where(s => s.Id == dto.Id).SingleOrDefault();
            if (course == null)
            {
                _resultDto.Status = false;
                _resultDto.Message = "Ders Bulunamadı!";
                return _resultDto;
            }

            course.Title = dto.Title;
            course.Description = dto.Description;
            _context.Course.Update(course);
            _context.SaveChanges();

            _resultDto.Status = true;
            _resultDto.Message = " Ders Düzenledi";
            return _resultDto;
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        public Response Delete(int id)
        {
            var course = _context.Course.Where(s => s.Id == id).SingleOrDefault();
            if (course == null)
            {
                _resultDto.Status = false;
                _resultDto.Message = "Ders Bulunamadı!";
                return _resultDto;
            }

            _context.Course.Remove(course);
            _context.SaveChanges();
            _resultDto.Status = true;
            _resultDto.Message = " Ders Silindi";
            return _resultDto;
        }
    }
}
