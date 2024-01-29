using QuizTaskAPI.DTOs.Question;

namespace QuizTaskAPI.DTOs.Quiz
{
    public class QuizDetailedGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }

        public List<QuestionGetDto> Questions { get; set; }
    }
}
