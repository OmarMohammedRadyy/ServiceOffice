using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceOffice.Data;
using ServiceOffice.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceOffice.Controllers
{
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var tasks = _context.WorkTasks
                .Include(t => t.Client)
                .Include(t => t.Service)
                .Include(t => t.AssignedEmployee)
                .AsNoTracking();
            return View(await tasks.ToListAsync());
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            PopulateLists();
            return View();
        }

        // POST: Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientId,ServiceId,AssignedEmployeeId,ReceivingEmployeeId,Date,Status,Notes")] Models.WorkTask task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateLists(task);
            return View(task);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var task = await _context.WorkTasks.FindAsync(id);
            if (task == null) return NotFound();
            PopulateLists(task);
            return View(task);
        }

        // POST: Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,ServiceId,AssignedEmployeeId,ReceivingEmployeeId,Date,Status,Notes")] Models.WorkTask task)
        {
            if (id != task.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.WorkTasks.Any(e => e.Id == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            PopulateLists(task);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var task = await _context.WorkTasks
                .Include(t => t.Client)
                .Include(t => t.Service)
                .Include(t => t.AssignedEmployee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null) return NotFound();
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.WorkTasks.FindAsync(id);
            _context.WorkTasks.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void PopulateLists(Models.WorkTask task = null)
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name", task?.ClientId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", task?.ServiceId);
            ViewData["AssignedEmployeeId"] = new SelectList(_context.Employees, "Id", "Name", task?.AssignedEmployeeId);
            ViewData["ReceivingEmployeeId"] = new SelectList(_context.Employees, "Id", "Name", task?.ReceivingEmployeeId);
            ViewData["Status"] = new SelectList(new[] { "مسجلة", "قيد التنفيذ", "منتظرة", "مكتملة" }, task?.Status);
        }
    }
}
