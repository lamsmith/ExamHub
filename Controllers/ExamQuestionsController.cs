using ExamHub.Context;
using ExamHub.DTO;
using ExamHub.Entity;
using ExamHub.Repositories.Implementation;
using ExamHub.Repositories.Interface;
using ExamHub.Services.Implementations;
using ExamHub.Services.Inteface;

using ExamHub.ViewModel;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ExamHub.Controllers
{
    public class ExamQuestionsController : Controller
    {
        private readonly IExamQuestionService _service;
        private readonly ApplicationDbContext _context;
        private readonly IExamRepository _repository;

        public ExamQuestionsController(IExamQuestionService service, ApplicationDbContext context )
        {
            _service = service;
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ExamQuestion>> GetAllExamQuestions()
        {
            return Ok(_service.GetAllExamQuestions());
        }

        //[HttpGet("{id}")]
        public ActionResult<ExamQuestion> GetExamQuestionById(int id)
        {
            var examQuestion = _service.GetExamQuestionById(id);
           // var examQuestion = "";
            if (examQuestion == null)
            {
                return NotFound();
            }
            return Ok(examQuestion);
        }

        [HttpPost]
        public ActionResult AddExamQuestion(ExamQuestion examQuestion)
        {
            _service.AddExamQuestion(examQuestion);
            return CreatedAtAction(nameof(GetExamQuestionById), new { id = examQuestion.Id }, examQuestion);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var examQuestions = _service.GetAllExamQuestions();
            return View(examQuestions);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateExamQuestion(int id, [FromBody] ExamQuestion examQuestion)
        {
            if (id != examQuestion.Id)
            {
                return BadRequest();
            }

            _service.UpdateExamQuestion(examQuestion);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteExamQuestion(int id)
        {
            _service.DeleteExamQuestion(id);
            return NoContent();
        }

        [HttpPost]
        public IActionResult CreateExamQuestion(CreateExamQuestionRequestModel request)
        {
            if (ModelState.IsValid)
            {
                var createdQuestion = _service.CreateExamQuestion(request);

                var response = new CreateExamQuestionRequestModel
                {
                    QuestionText = createdQuestion.QuestionText,
                    Options = createdQuestion.Options,
                    CorrectAnswer = createdQuestion.CorrectAnswer
                };

                return View(response);
            }

            return View(request);
        }

        [HttpGet]
        public IActionResult UploadExamQuestions([FromQuery] int examId )
        {
            ViewBag.ExamId = examId;
            return View();
        }

        

        public IActionResult UploadExamQuestions(IFormFile file, int examId)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("", "No file uploaded or file is empty.");
                return View();
            }

            var errors = new List<string>();
            var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");

            try
            {
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                var filePath = Path.Combine(uploadDirectory, Path.GetFileName(file.FileName));

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                var excelData = new List<CreateExamQuestionRequestModel>();

                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        int rowIndex = 0;

                        while (reader.Read())
                        {
                            rowIndex++;

                            // Skip the header row
                            if (rowIndex == 1) continue;

                            // Skip rows that are completely empty
                            if (reader.FieldCount == 0 || reader.IsDBNull(0))
                            {
                                continue;
                            }

                            // Try to parse the question number
                            if (!int.TryParse(reader.GetValue(0)?.ToString(), out int questionNo) || questionNo <= 0)
                            {
                                errors.Add($"Invalid question number at row {rowIndex}.");
                                continue;
                            }

                            var questionText = reader.GetValue(1)?.ToString();
                            if (string.IsNullOrWhiteSpace(questionText))
                            {
                                errors.Add($"Question {questionNo}: Question text is missing or invalid at row {rowIndex}.");
                                continue;
                            }

                            var options = new List<string>
                            {
                                reader.GetValue(2)?.ToString(),
                                reader.GetValue(3)?.ToString(),
                                reader.GetValue(4)?.ToString(),
                                reader.GetValue(5)?.ToString()
                            };

                            if (options.Any(opt => string.IsNullOrWhiteSpace(opt)))
                            {
                                errors.Add($"Question {questionNo}: One or more options are missing at row {rowIndex}.");
                                continue;
                            }

                            var correctAnswer = reader.GetValue(6)?.ToString();
                            
                            var requestModel = new CreateExamQuestionRequestModel
                            {
                                QuestionNo = questionNo,
                                QuestionText = questionText,
                                Options = options,
                                CorrectAnswer = correctAnswer,  
                                ExamId = examId
                            };

                            excelData.Add(requestModel);
                        }
                    }
                }

                if (errors.Any())
                {
                    ViewBag.Errors = errors;
                    return View();
                }

                
                foreach (var request in excelData)
                {
                    _service.CreateExamQuestion(request);
                }

                ViewBag.SuccessMessage = "Questions uploaded successfully!";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while processing the file. Please try again.");
                // Optionally log the exception here
                return View();
            }

            return View();
        }






    }
}
