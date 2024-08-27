using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExamHub.Entity;
using ExamHub.ViewModel;
using ExamHub.DTO;
using ExamHub.Services.Inteface;

[Route("api/[controller]")]
[ApiController]
public class ExamResultController : Controller
{
    private readonly IExamResultService _examResultService;

    public ExamResultController(IExamResultService examResultService)
    {
        _examResultService = examResultService;
    }

    [HttpPost("CalculateAndSave")]
    public async Task<IActionResult> CalculateAndSave([FromBody] ExamResultRequestModel requestModel)
    {
        var generalExamResult = await _examResultService.CalculateAndSaveGeneralExamResultAsync(requestModel.StudentId, requestModel.ExamId);
        var resultViewModel = new GeneralExamResultResponseModel
        {
            Id = generalExamResult.Id,
            StudentId = generalExamResult.StudentId,
            StudentName = generalExamResult.Student.User.FirstName,
            Percentage = generalExamResult.Percentage,
            ExamResults = new List<ExamResultResponseModel>()
        };

        foreach (var examResult in generalExamResult.ExamResults)
        {
            resultViewModel.ExamResults.Add(new ExamResultResponseModel
            {
                Id = examResult.Id,
                StudentId = examResult.StudentId,
                StudentName = examResult.Student.User.FirstName, 
                ExamId = examResult.ExamId,
                Subject = examResult.Exam.Subject.SubjectName,
                Percentage = examResult.Percentage,
                ExamDate = examResult.ExamDate      
            });
        }

        return Ok(resultViewModel);
    }
}
