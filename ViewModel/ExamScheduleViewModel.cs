using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamHub.DTO;
using ExamHub.Entity;

namespace ExamHub.ViewModel
{
    public class ExamScheduleViewModel
    {
        public string StudentName { get; set; }
        public IEnumerable<ExamResponseModel> UpcomingExams { get; set; }
    }
}