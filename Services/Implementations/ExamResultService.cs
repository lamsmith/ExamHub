using System;
using System.Linq;
using System.Threading.Tasks;
using ExamHub.Context;
using ExamHub.Entity;
using ExamHub.Repositories.Interface;
using ExamHub.Services.Inteface;
using Microsoft.EntityFrameworkCore;

public class ExamResultService : IExamResultService
{
    private readonly IExamResultRepository _examResultRepository;
    private readonly IGeneralExamResultRepository _generalExamResultRepository;
    private readonly ApplicationDbContext _context;

    public ExamResultService(
        IExamResultRepository examResultRepository,
        IGeneralExamResultRepository generalExamResultRepository,
        ApplicationDbContext context)
    {
        _examResultRepository = examResultRepository;
        _generalExamResultRepository = generalExamResultRepository;
        _context = context;
    }

    public async Task<GeneralExamResult> CalculateAndSaveGeneralExamResultAsync(int studentId, int examId)
    {
        // Get all the student's answers for the given exam
        var studentAnswers = await _context.StudentAnswers
            .Where(sa => sa.StudentId == studentId && sa.ExamQuestion.ExamId == examId)
            .Include(sa => sa.ExamQuestion)
            .ToListAsync(); 

        int totalQuestions = studentAnswers.Count;
        //int correctAnswers = studentAnswers.Count(sa => sa.SelectedOptionId == sa.ExamQuestion.CorrectAnswer);
        
        int correctAnswers = studentAnswers.Count(sa =>
            sa.SelectedOptionId == sa.ExamQuestion.Options.FirstOrDefault
            (o => o.Id == sa.ExamQuestion.CorrectAnswer).Id
        );
        double percentage = (double)correctAnswers / totalQuestions * 100;

        // Create and save the general exam result
        var generalExamResult = new GeneralExamResult
        {
            StudentId = studentId,
            Percentage = percentage
        };

        //await _generalExamResultRepository.AddGeneralExamResultAsync(generalExamResult);
        //await _generalExamResultRepository.SaveAsync();

        // Create and save individual exam results
        foreach (var answer in studentAnswers)
        {
            var examResult = new ExamResult
            {
                GeneralExamResultId = generalExamResult.Id,
                StudentId = studentId,
                ExamId = examId,
                Score = correctAnswers, // Assuming score is the number of correct answers
                Percentage = percentage,
                ExamDate = DateTime.Now
            };

            await _examResultRepository.AddExamResultAsync(examResult);
        }

        await _examResultRepository.SaveAsync();

        return generalExamResult;
    }
}
