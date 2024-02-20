using AutoMapper;
using QuizTaskAPI.DTOs.Quiz;
using QuizTaskAPI.Entities;

namespace QuizTaskAPI.AutoMapper
{
    public class QuizProfile : Profile
    {
        public QuizProfile()
        {
            CreateMap<Quiz, QuizDetailedGetDto>()
            .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions));

            CreateMap<Quiz, QuizGetDto>();

            CreateMap<QuizPostDto, Quiz>();

        }
    }
}
