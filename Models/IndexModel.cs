using ExamHub.DTO;
using ExamHub.Entity;
using ExamHub.Services.Inteface;

namespace ExamHub.Models
{
    public class IndexModel
    {
        private readonly IExamQuestionService _service;

        public IndexModel(IExamQuestionService service)
        {
            _service = service;
        }

        public IEnumerable<ExamQuestionReponseModel> ExamQuestions { get; set; }

        public void OnGet()
        {
            ExamQuestions = _service.GetAllExamQuestions();
        }
    }
}
