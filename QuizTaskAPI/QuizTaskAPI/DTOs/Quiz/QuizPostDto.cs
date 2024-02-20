using QuizTaskAPI.DTOs.Question;

namespace QuizTaskAPI.DTOs.Quiz
{
    public class QuizPostDto
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }

        public List<QuestionPostDto> Questions { get; set; }
    }
}
