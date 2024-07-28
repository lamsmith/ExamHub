using ExamHub.DTO;
using ExamHub.Entity;

namespace ExamHub.ViewModel
{
    public class ViewExamScoresViewModel
    {
        public IEnumerable<ExamResponseModel> Exams { get; set; }
    }
}
