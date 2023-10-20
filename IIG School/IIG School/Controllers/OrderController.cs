using IIG_School.Data;
using IIG_School.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace IIG_School.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public OrderController(
             ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Confirm(int id)
        {
            var orders = await _context.Orders!.Include(o => o.OrderLines!).ThenInclude(x => x.Product)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderID == id);

            if (orders == null)
            {
                return NotFound();
            }
            return View(orders);
        }
    }
}
