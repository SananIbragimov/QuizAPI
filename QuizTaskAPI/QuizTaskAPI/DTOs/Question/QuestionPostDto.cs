using QuizTaskAPI.DTOs.Option;

namespace QuizTaskAPI.DTOs.Question
{
    public class QuestionPostDto
    {
        public string Name { get; set; }
        public decimal Points { get; set; }

        public int QuizId { get; set; }

        public List<OptionPostDto> Options { get; set; }
    }
}
