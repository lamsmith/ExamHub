using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExamHub.Entity;
using ExamHub.ViewModel;
using ExamHub.DTO;
using ExamHub.Services.Inteface;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
public class GeneralExamResultController : Controller
{
    private readonly IGeneralExamResultService _generalExamResultService;

    public GeneralExamResultController(IGeneralExamResultService generalExamResultService)
    {
        _generalExamResultService = generalExamResultService;
    }

    //[HttpGet("{studentId}")]
    //public async Task<IActionResult> GetByStudentId(int studentId)
    //{
    //    var generalExamResult = await _generalExamResultService.GetGeneralExamResultByStudentIdAsync(studentId);
    //    if (generalExamResult == null)
    //    {
    //        return NotFound();
    //    }

    //    var resultViewModel = new GeneralExamResultResponseModel
    //    {
    //        Id = generalExamResult.Id,
    //        StudentId = generalExamResult.StudentId,
    //        StudentName = generalExamResult.Student.User.FirstName + generalExamResult.Student.User.LastName, 
    //        Percentage = generalExamResult.Percentage,
    //        ExamResults = generalExamResult.ExamResults.Select(er => new ExamResultResponseModel
    //        {
    //            Id = er.Id,
    //            StudentId = er.StudentId,
    //            StudentName = er.Student.User.FirstName + er.Student.User.LastName,
    //            ExamId = er.ExamId,
    //            Subject = er.Exam.Subject.SubjectName,
    //            Score = er.Score,
    //            Percentage = er.Percentage,
    //            ExamDate = er.ExamDate
    //        }).ToList()
    //    };

    //    return Ok(resultViewModel);
    //}

    // GET: GeneralExamResult/Index
    [Authorize(Roles = "Principal")]
    public async Task<IActionResult> Index()
    {
        var results = await _generalExamResultService.GetAllResultsForPrincipalAsync(User.Identity.Name);
        return View(results);
    }

    // GET: GeneralExamResult/TeacherResults
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> TeacherResults(int examId)
    {
        var results = await _generalExamResultService.GetResultsForTeacherAsync(User.Identity.Name, examId);
        return View(results);
    }

    // GET: GeneralExamResult/StudentResults
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> StudentResults()
    {
        var results = await _generalExamResultService.GetResultsForStudentAsync(User.Identity.Name);
        return View(results);
    }
}
