using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamHub.Context;
using ExamHub.Entity;
using ExamHub.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

public class ExamResultRepository : IExamResultRepository
{
    private readonly ApplicationDbContext _context;

    public ExamResultRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ExamResult> GetExamResultByIdAsync(int id)
    {
        return await _context.ExamResults.FindAsync(id);
    }

    public async Task<IEnumerable<ExamResult>> GetExamResultsByStudentIdAsync(int studentId)
    {
        return await _context.ExamResults.Where(er => er.StudentId == studentId).ToListAsync();
    }

    public async Task<IEnumerable<ExamResult>> GetExamResultsByExamIdAsync(int examId)
    {
        return await _context.ExamResults.Where(er => er.ExamId == examId).ToListAsync();
    }

    public async Task AddExamResultAsync(ExamResult examResult)
    {
        await _context.ExamResults.AddAsync(examResult);
    }
    public bool HasStudentTakenExam(int studentId, int examId)
    {
        return _context.ExamResults.Any(er => er.StudentId == studentId && er.ExamId == examId);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public Task<IEnumerable<ExamResult>> GetExamResultsBySubjectAsync(int subjectId)
    {
        throw new NotImplementedException();
    }
}
