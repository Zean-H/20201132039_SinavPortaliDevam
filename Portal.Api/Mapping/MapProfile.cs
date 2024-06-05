using _20201132039_SinavPortali.Dtos;
using _20201132039_SinavPortali.Models;
using AutoMapper;

namespace _20201132039_SinavPortali.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<Option, OptionDto>().ReverseMap();
            CreateMap<Result, ResultDto>().ReverseMap();
            CreateMap<Assessment, AssessmentDto>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<AppUser, UserDto>().ReverseMap();
        }
    }
}
