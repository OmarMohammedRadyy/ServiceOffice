using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceOffice.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceOffice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.TotalClients = await _context.Clients.CountAsync();
            ViewBag.ActiveTasks = await _context.WorkTasks.CountAsync(t => t.Status == "قيد التنفيذ");
            ViewBag.TasksCompletedToday = await _context.WorkTasks
                .CountAsync(t => t.Status == "مكتملة" && t.Date.Date == DateTime.Today);
            ViewBag.AvailableEmployees = await _context.Employees.CountAsync(); // يمكن استبدالها بحالة التوفر الفعلية

            return View();
        }
    }
}
