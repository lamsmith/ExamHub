using Microsoft.AspNetCore.Mvc;
using ExamHub.Entity;
using ExamHub.Services.Inteface;

namespace ExamHub.Controllers
{
    public class ExamController : Controller
    {
        private readonly IExamService _examService;

        public ExamController(IExamService examService)
        {
            _examService = examService;
        }

        public IActionResult Index()
        {
            var exams = _examService.GetAllExams();
            return View(exams);
        }

        [HttpPost]
        public IActionResult AddExam(Exam exam)
        {
            if (ModelState.IsValid)
            {
                _examService.AddExam(exam);
                return RedirectToAction("Index");
            }
            return View(exam);
        }

        public IActionResult GetExamsByTeacher(int teacherId)
        {
            var exams = _examService.GetExamsByTeacherId(teacherId);
            return View(exams);
        }

        public IActionResult Edit(int id)
        {
            var exam = _examService.GetExamById(id);
            if (exam == null)
            {
                return NotFound();
            }
            return View(exam);
        }

        [HttpPost]
        public IActionResult Edit(Exam exam)
        {
            if (ModelState.IsValid)
            {
                _examService.UpdateExam(exam);
                return RedirectToAction("Index");
            }
            return View(exam);
        }

        public IActionResult Delete(int id)
        {
            var exam = _examService.GetExamById(id);
            if (exam == null)
            {
                return NotFound();
            }
            return View(exam);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _examService.DeleteExam(id);
            return RedirectToAction("Index");
        }
    }
}
