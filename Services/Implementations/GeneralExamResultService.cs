using System.Threading.Tasks;
using ExamHub.Entity;
using ExamHub.Repositories.Interface;
using ExamHub.Services.Inteface;
using ExamHub.ViewModel;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

public class GeneralExamResultService : IGeneralExamResultService
{
    private readonly IGeneralExamResultRepository _generalExamResultRepository;

    public GeneralExamResultService(IGeneralExamResultRepository generalExamResultRepository)
    {
        _generalExamResultRepository = generalExamResultRepository;
    }

    //public async Task<GeneralExamResult> GetGeneralExamResultByStudentIdAsync(int studentId)
    //{
    //    return await _generalExamResultRepository.GetGeneralExamResultByStudentIdAsync(studentId);
    //}
    public async Task<IEnumerable<GeneralExamResult>> GetAllResultsForPrincipalAsync(string principalName)
    {
        var results = await _generalExamResultRepository.GetResultsForPrincipalAsync(principalName);
        return results.ToList().Select(res => new GeneralExamResult
        {
            
        }) ;
    }

    public async Task<IEnumerable<GeneralExamResult>> GetResultsForTeacherAsync(string teacherName, int examId)
    {
        return await _generalExamResultRepository.GetResultsForTeacherAsync(teacherName, examId);
    }

    public async Task<IEnumerable<GeneralExamResult>> GetResultsForStudentAsync(string studentName)
    {
        return await _generalExamResultRepository.GetResultsForStudentAsync(studentName);
    }

    public GeneralExamResultViewModel GetGeneralExamResultForStudent(int studentId)
    {
        var generalExamResult = _generalExamResultRepository.GetGeneralExamResultByStudentId(studentId);

        if (generalExamResult == null)
            return null;

        var viewModel = new GeneralExamResultViewModel
        {
            TotalPercentage = generalExamResult.Percentage,
            ExamResults = generalExamResult.ExamResults.Select(er => new ExamResultViewModel
            {
                Subject = er.Exam.Subject.SubjectName, // Assuming Subject has a property 'SubjectName'
                Score = er.Score,
                Percentage = er.Percentage,
                ExamDate = er.ExamDate
            }).ToList()
        };

        return viewModel;
    }

    public async Task<IEnumerable<GeneralExamResultViewModel>> GetResultsByClassIdAsync(int classId)
    {
        var results = await _generalExamResultRepository.GetResultsByClassIdAsync(classId);

        // Map the results to GeneralExamResultViewModel
        return results.Select(g => new GeneralExamResultViewModel
        {
            StudentName = g.Student.User.FirstName  + " " + g.Student.User.LastName,
            ClassName = g.ExamResults.FirstOrDefault()?.Student.ClassStudents.FirstOrDefault(cs => cs.ClassId == classId)?.Class.ClassName,
            ExamTitle = g.ExamResults.FirstOrDefault()?.Exam.ExamName,
            Score = g.ExamResults.FirstOrDefault()?.Score,
           
        });
    }
}
