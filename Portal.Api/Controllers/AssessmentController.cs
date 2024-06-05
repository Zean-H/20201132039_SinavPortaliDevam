using _20201132039_SinavPortali.Dtos;
using _20201132039_SinavPortali.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _20201132039_SinavPortali.Controllers
{
    [Authorize(Roles = "Admin, Teacher, Student")]
    [Route("api/Assessment/[Action]")]
    [ApiController]
    public class AssessmentController
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        Response _resultDto = new Response();

        public AssessmentController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public List<AssessmentDto> GetAll()
        {
            var assessments = _context.Assessment.ToList();
            var assessmentDtos = _mapper.Map<List<AssessmentDto>>(assessments);
            return assessmentDtos;
        }

        [HttpGet]
        [Route("{courseId}")]
        public List<AssessmentDto> GetAllFromCourse(int courseId)
        {
            var assessments = _context.Assessment.Where(e => e.CourseId == courseId);
            var assessmentDtos = _mapper.Map<List<AssessmentDto>>(assessments);
            return assessmentDtos;
        }

        [HttpPost]
        [Authorize(Roles = "Teacher, Admin")]
        public Response PostExam(AssessmentDto dto)
        {
            var assesment = _mapper.Map<Assessment>(dto);
            _context.Assessment.Add(assesment);
            _context.SaveChanges();

            _resultDto.Status = true;
            _resultDto.Message = " Sınav Eklendi";
            return _resultDto;
        }

        [HttpPut]
        [Authorize(Roles = "Teacher, Admin")]
        public Response Put(AssessmentDto dto)
        {
            var assessment = _context.Assessment.Where(a => a.Id == dto.Id).SingleOrDefault();
            if (assessment == null)
            {
                _resultDto.Status = false;
                _resultDto.Message = "Sınav Bulunamadı!";
                return _resultDto;
            }
            assessment.Title = dto.Title;
            assessment.Description = dto.Description;
            assessment.StartTime = dto.StartTime;
            assessment.EndTime = dto.EndTime;
            _context.Assessment.Update(assessment);
            _context.SaveChanges();

            _resultDto.Status = true;
            _resultDto.Message = "Sınav Düzenledi";
            return _resultDto;
        }

        [HttpDelete]
        [Authorize(Roles = "Teacher, Admin")]
        [Route("{id}")]
        public Response Delete(int id)
        {
            var assesment = _context.Assessment.Where(s => s.Id == id).SingleOrDefault();
            if (assesment == null)
            {
                _resultDto.Status = false;
                _resultDto.Message = "Sınav Bulunamadı!";
                return _resultDto;
            }

            _context.Assessment.Remove(assesment);
            _context.SaveChanges();
            _resultDto.Status = true;
            _resultDto.Message = " Sınav Silindi";
            return _resultDto;
        }
    }
}
