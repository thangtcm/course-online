using IIG_School.Data;
using IIG_School.Models;
using IIG_School.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace IIG_School.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var lst = await _context.Product!.ToListAsync();
            return View(lst);
        }

        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> ExamResult(int? page, int? examId = 0, int? addressExamId = 0, DateTime? dateFirst = null, DateTime? dateEnd = null)
        {
            int pagesize = 50;
            int pagenumber = page == null || page < 0 ? 1 : page.Value;
            var query = _context.ExcelDataModels!.AsQueryable();
            if (examId != 0)
            {
                var exam = await _context.Exam!.FirstOrDefaultAsync(x => x.Id== examId);
                query = query.Where(e => e.ExamName!.Contains(exam!.Name!));
            }
            if (addressExamId != 0)
            {
                var addressExam = await _context.AddressExams!.FirstOrDefaultAsync(x => x.Id == addressExamId);
                query = query.Where(e => e.Location!.Contains(addressExam!.Name!));
            }
            if (dateFirst.HasValue)
            {
                query = query.Where(e => e.ExamDate >= dateFirst.Value);
            }
            if (dateEnd.HasValue)
            {
                query = query.Where(e => e.ExamDate <= dateEnd.Value);
            }
            ViewBag.ExamId = examId;
            ViewBag.AddressExamId = addressExamId;
            ViewBag.DateFirst = dateFirst;
            ViewBag.DateEnd = dateEnd;
            PagedList<ExcelDataModel> lst = new(query, pagenumber, pagesize);
            ViewData["Exam"] = new SelectList(await _context.Exam!.ToListAsync(), "Id", "Name", examId);
            ViewData["AddressExam"] = new SelectList(await _context.AddressExams!.ToListAsync(), "Id", "Name", addressExamId);
            return View(lst);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}