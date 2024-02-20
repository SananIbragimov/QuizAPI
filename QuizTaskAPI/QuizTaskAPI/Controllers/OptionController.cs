using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizTaskAPI.Data;
using QuizTaskAPI.DTOs.Option;
using QuizTaskAPI.DTOs.Question;
using QuizTaskAPI.DTOs.Quiz;
using QuizTaskAPI.Entities;

namespace QuizTaskAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class OptionController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public OptionController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Post(OptionPostDto dto)
        {
            var option = _mapper.Map<Option>(dto);
            _dbContext.Options.Add(option);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int? id, OptionPutDto dto)
        {
            var option = _dbContext.Options.FirstOrDefault(x => x.Id == id);
            if (option == null) return NotFound();

            option.Name = dto.Name;
            option.IsCorrect = dto.IsCorrect;

            _dbContext.SaveChanges();

            var updatedOptionDto = _mapper.Map<OptionPutDto>(option);


            return Ok(updatedOptionDto);
        }
    }
}

