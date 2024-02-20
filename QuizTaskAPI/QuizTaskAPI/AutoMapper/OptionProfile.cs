using AutoMapper;
using QuizTaskAPI.DTOs.Option;
using QuizTaskAPI.Entities;

namespace QuizTaskAPI.AutoMapper
{
    public class OptionProfile : Profile
    {
        public OptionProfile()
        {
            CreateMap<Option, OptionGetDto>();
            CreateMap<OptionPostDto, Option>();
            CreateMap<OptionPutDto, Option>().ReverseMap();
        }
    }
}
