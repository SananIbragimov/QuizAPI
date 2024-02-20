using AutoMapper;
using QuizTaskAPI.DTOs.Question;
using QuizTaskAPI.Entities;

namespace QuizTaskAPI.AutoMapper
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionGetDto>()
           .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options));

            CreateMap<QuestionPostDto, Question>();
            CreateMap<QuestionPutDto, Question>().ReverseMap();
        }
    }
}
