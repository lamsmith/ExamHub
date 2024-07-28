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

        [HttpGet("{id}")]
        public ActionResult<ExamQuestion> GetExamQuestionById(int id)
        {
            var examQuestion = _service.GetExamQuestionById(id);
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

            

            if (file != null && file.Length > 0)
            {
                var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                var filePath = Path.Combine(uploadDirectory, file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                var excelData = new List<CreateExamQuestionRequestModel>();

                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        bool isHeaderSkipped = false;

                        while (reader.Read())
                        {
                            if (!isHeaderSkipped)
                            {
                                isHeaderSkipped = true;
                                continue;
                            }

                            var requestModel = new CreateExamQuestionRequestModel
                            {
                                QuestionNo = Convert.ToInt32(reader.GetValue(0)),
                                QuestionText = reader.GetValue(1)?.ToString(),
                                Options = new List<string>
                            {
                                reader.GetValue(2)?.ToString(),
                                reader.GetValue(3)?.ToString(),
                                reader.GetValue(4)?.ToString(),
                                reader.GetValue(5)?.ToString()
                            },
                                CorrectAnswer = reader.GetValue(6)?.ToString(),
                                ExamId = examId
                            };

                            var responseModel = _service.CreateExamQuestion(requestModel);
                            excelData.Add(requestModel);

                        }

                        ViewBag.ExcelData = excelData;
                    }
                }
            }

            return View();
        }

        [HttpPost]
        public IActionResult SaveExamQuestions(List<DTO.UploadedQuestion> uploadedQuestions)
        {
            try
            {
                foreach (var question in uploadedQuestions)
                {
                    var examQuestion = new ExamQuestion
                    {
                        QuestionNo = question.QuestionNo,
                        QuestionText = question.QuestionText,
                        Options = new List<Option>
                {
                    new Option { OptionText = question.OptionA },
                    new Option { OptionText = question.OptionB },
                    new Option { OptionText = question.OptionC },
                    new Option { OptionText = question.OptionD }
                },
                        CorrectAnswer = question.CorrectAnswer
                    };

            
                    _context.ExamQuestions.Add(examQuestion);
                }

              
                _context.SaveChanges();

              
                return RedirectToAction("UploadExamQuestions"); 
            }
            catch (Exception ex)
            {
              
                ViewBag.ErrorMessage = "An error occurred while saving the data. Please try again later.";
                return View("Error"); 
            }
        }

        //[HttpPost]
        //public IActionResult SaveExamQuestions(List<UploadedQuestion> uploadedQuestions)
        //{
        //    foreach (var question in uploadedQuestions)
        //    {
        //        var examQuestion = new ExamQuestion
        //        {
        //            QuestionText = question.QuestionText,
        //            Options = new List<Option>
        //    {
        //        new Option { OptionText = question.OptionA },
        //        new Option { OptionText = question.OptionB },
        //        new Option { OptionText = question.OptionC },
        //        new Option { OptionText = question.OptionD }
        //    },
        //            CorrectAnswer = question.CorrectAnswer
        //        };

        //        // Assuming _context is properly initialized elsewhere in your code
        //        _context.ExamQuestions.Add(examQuestion);
        //    }

        //    // Save changes to the database
        //    _context.SaveChanges();

        //    // Redirect to the "UploadExamQuestions" action
        //    return RedirectToAction("UploadExamQuestions");
        //}

    }
}
