using Microsoft.AspNetCore.Mvc;

namespace ExamHub.Controllers
{
    public class BaseController : Controller
    {
        protected string UserRole => HttpContext.Items["Role"]?.ToString();
    }
}
