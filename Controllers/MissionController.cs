using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Calendar.Models;
using Calendar.Data;

public class MissionController : Controller
{
    private readonly ApplicationDbContext _context;

    public MissionController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Calendar()
    {
        if (User.Identity.IsAuthenticated)
        {
            var missions = await _context.Missions
                .Include(m => m.Employees) // Ensure Employees are included
                .ToListAsync();
            return View(missions);
        }
        else
        {
            return RedirectToAction("Login", "Account");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] Mission mission, [FromForm] List<int> employees)
    {
        if (User.Identity.IsAuthenticated)
        {
            if (ModelState.IsValid)
            {
                var selectedEmployees = await _context.Employees
                    .Where(e => employees.Contains(e.Id))
                    .ToListAsync();

                if (mission.Id > 0)
                {
                    var existingMission = await _context.Missions.Include(m => m.Employees)
                        .FirstOrDefaultAsync(m => m.Id == mission.Id);

                    if (existingMission != null)
                    {
                        existingMission.Title = mission.Title;
                        existingMission.StartDate = mission.StartDate;
                        existingMission.Site = mission.Site;
                        existingMission.EndDate = mission.EndDate;
                        existingMission.Description = mission.Description;

                        existingMission.Employees.Clear();
                        existingMission.Employees.AddRange(selectedEmployees);

                        await _context.SaveChangesAsync();
                        return Json(new { success = true, employees = selectedEmployees });
                    }
                }
                else
                {
                    // Assign selected employees to the new mission
                    mission.Employees = selectedEmployees;
                    _context.Add(mission);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, id = mission.Id, employees = selectedEmployees });
                }
            }
            return Json(new { success = false });
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }
    }

    [HttpPost]
    public async Task<IActionResult> DeleteMission(int id)
    {
        if (User.Identity.IsAuthenticated)
        {
            var mission = await _context.Missions.FindAsync(id);
            if (mission != null)
            {
                _context.Missions.Remove(mission);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Mission not found." });
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }
    }
    [HttpGet]
    public async Task<IActionResult> GetAllEmployees()
    {
        var employees = await _context.Employees
            .Select(e => new { id = e.Id, text = e.Name })
            .ToListAsync();
        return Json(employees);
    }
}
