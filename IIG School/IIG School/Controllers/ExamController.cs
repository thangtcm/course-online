using IIG_School.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Formats.Asn1.AsnWriter;

namespace IIG_School.Controllers
{
    public class ExamController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ExamController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> StartExam()
        {
            var lstQuestion = await _context.Question!.Include(a => a.Answers).ToListAsync();
            if (HttpContext.Session.GetString("IsExam") == null)
            {
                HttpContext.Session.SetString("IsExam", "1");
            }
            else
            {
                HttpContext.Session.Remove("IsExam");
                return RedirectToAction("Index", "Exam");
            }    
            return View(lstQuestion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StartExam(Dictionary<int, int>? answer)
        {
            var lstAnswer = await _context.Answers!.ToListAsync();
            int correctAnswers = 0;
            foreach (var asw in lstAnswer)
            {
                if (answer!.ContainsKey(asw.QuestionId))
                {
                    int selectedAnswerId = answer[asw.QuestionId];
                    if (selectedAnswerId == asw.Id && asw.IsCorrect == true)
                    {
                        correctAnswers++;
                    }
                }
            }
            double Score = (double)(correctAnswers); 
            return RedirectToAction("ExamResult", "Exam", new {Score = Score});
        }

        public IActionResult ExamResult(double Score)
        {
            if (HttpContext.Session.GetString("IsExam") == null)
            {
                return RedirectToAction("Index", "Exam");
            }
            HttpContext.Session.Remove("IsExam");
            return View(Score);
        }
    }
}
