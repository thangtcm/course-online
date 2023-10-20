using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IIG_School.Helpers;

namespace IIG_School.Areas.Admin.Controllers
{
    public class QuestionController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
