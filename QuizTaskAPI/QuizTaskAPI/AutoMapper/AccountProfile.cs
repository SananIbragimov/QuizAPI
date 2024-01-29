using AutoMapper;
using QuizTaskAPI.DTOs.Account;
using QuizTaskAPI.Entities;

namespace QuizTaskAPI.AutoMapper
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<RegisterDto, AppUser>();
        }
    }
}
