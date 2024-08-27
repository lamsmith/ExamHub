﻿using System.Threading.Tasks;
using ExamHub.Context;
using ExamHub.Entity;
using ExamHub.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

public class GeneralExamResultRepository : IGeneralExamResultRepository
{
    private readonly ApplicationDbContext _context;

    public GeneralExamResultRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void SaveGeneralExamResult(int studentId, double percentage)
    {
        // Check if the student exists in the database
        var student = _context.Students.FirstOrDefault(s => s.Id == studentId);

        if (student == null)
        {
            // Handle the case where the student doesn't exist
            // You can throw an exception, return an error, or handle it as needed
            throw new Exception("Student not found.");
        }

        // Create and save the GeneralExamResult
        var generalExamResult = new GeneralExamResult
        {
            StudentId = student.Id,  // This ensures the foreign key is valid
            Percentage = percentage
        };

        _context.GeneralExamResults.Add(generalExamResult);
        _context.SaveChanges();
    }

    //public void GeneralExamResultexists ()
    //{
    //    var generalExamResult = _context.GeneralExamResults
    //            .FirstOrDefault(g => g.StudentId == studentExam.Id);
    //}


    //public async Task<GeneralExamResult> GetGeneralExamResultByIdAsync(int id)
    //{
    //    return await _context.GeneralExamResults.FindAsync(id);
    //}

    //public async Task<GeneralExamResult> GetGeneralExamResultByStudentIdAsync(int studentId)
    //{
    //    return await _context.GeneralExamResults
    //        .Include(ger => ger.ExamResults)
    //        .FirstOrDefaultAsync(ger => ger.StudentId == studentId);
    //}

    //public async Task AddGeneralExamResultAsync(GeneralExamResult generalExamResult)
    //{
    //    await _context.GeneralExamResults.AddAsync(generalExamResult);
    //}

    //public async Task SaveAsync()
    //{
    //    await _context.SaveChangesAsync();
    //}

    public async Task<IEnumerable<GeneralExamResult>> GetResultsForPrincipalAsync(string principalName)
    {
        return await _context.GeneralExamResults
                .Include(g => g.ExamResults)
                .ThenInclude(er => er.Student)
                .Where(g => g.ExamResults.Any(er => er.Exam.Subject.Principal.User.Username == principalName))
                .ToListAsync();
    }

        public async Task<IEnumerable<GeneralExamResult>> GetResultsForTeacherAsync(string teacherName, int examId)
        {
            return await _context.GeneralExamResults
                .Include(g => g.ExamResults)
                .ThenInclude(er => er.Student)
                .Where(g => g.ExamResults.Any(er => er.Exam.CreatedByTeacher.User.Username == teacherName && er.ExamId == examId))
                .ToListAsync();
        }

        public async Task<IEnumerable<GeneralExamResult>> GetResultsForStudentAsync(string studentName)
        {
            return await _context.GeneralExamResults
                .Include(g => g.ExamResults)
                .Where(g => g.Student.User.Username == studentName)
                .ToListAsync();
        }

        public GeneralExamResult GetGeneralExamResultByStudentId(int studentId)
        {
        //return _context.GeneralExamResults
        //    .Include(g => g.ExamResults)
        //    .Include(ex => ex.Exam.Subject) 
        //    .ThenInclude(er => er.Exam)
        //    .FirstOrDefault(g => g.StudentId == studentId);
                return _context.GeneralExamResults
            .Include(g => g.ExamResults) // Include ExamResults
            .ThenInclude(er => er.Exam) // Then include the Exam related to each ExamResult
            .ThenInclude(e => e.Subject) // Then include the Subject related to each Exam
            .FirstOrDefault(g => g.StudentId == studentId);
    }

    //return _context.GeneralExamResults

    //    .Include(g => g.ExamResults)
    //    .Include(ex => ex.Exam.Subject)
    //    .ThenInclude(er => er.Exam)
    //    .FirstOrDefault(g => g.StudentId == studentId);
}
