using IIG_School.Data;
using IIG_School.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using  IIG_School.Helpers;
using System.Globalization;

namespace IIG_School.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [Authorize(Roles = Constants.Roles.Admin)]
    public class ExamResultController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ExamResultController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile file)
        {
            using (var package = new ExcelPackage(file.OpenReadStream()))
            {
                var worksheet = package.Workbook.Worksheets[0]; // assuming your data is on the first worksheet
                List<ExcelDataModel> lst = new();
                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    var data = new ExcelDataModel
                    {
                        ExamName = worksheet.Cells[row, 2].GetValue<string>(),
                        ExamDate = DateTime.ParseExact(worksheet.Cells[row, 3].GetValue<string>(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        ExamTime = worksheet.Cells[row, 4].GetValue<string>(),
                        Location = worksheet.Cells[row, 5].GetValue<string>(),
                        Note = worksheet.Cells[row, 6].GetValue<string>()
                    };

                    lst.Add(data);
                }
                await _context.ExcelDataModels!.AddRangeAsync(lst);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "HomeAdmin");
        }
    } 
}
