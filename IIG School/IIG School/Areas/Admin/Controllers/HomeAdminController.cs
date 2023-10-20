using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IIG_School.Helpers;

namespace IIG_School.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    [Route("Admin/homeadmin")]
    [Authorize]
    [Authorize(Roles = Constants.Roles.Admin)]
    public class HomeAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
