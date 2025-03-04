using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MissionSystem.Data;
using MissionSystem.Models;

namespace MissionSystem.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Category()
    {
        ViewBag.Categories = _context.Categories.ToList();
        return View();
    }

    public IActionResult Worker()
    {
        ViewBag.Workers = _context.Workers.ToList();
        return View();
    }

    public IActionResult Duty()
    {
        ViewBag.Duties = _context.Duties.ToList();
        return View();
    }
    [HttpPost]
    public IActionResult AddDuty(Duty duty) 
    {
        if (!ModelState.IsValid)
        {
            TempData["AddDutyIsValidError"] = "Ekleme Ýþlemi Baþarýsýz! Lütfen tüm alanlarý doldurunuz.";
            return RedirectToAction("Duty");
        }
        _context.Duties.Add(duty);
        _context.SaveChanges();
        TempData["AddDutySuccess"] = "Ekleme Ýþlemi Baþarýlý!";
        return RedirectToAction("Duty"); 
    }
    [HttpGet]
    public IActionResult UpdateDuty(int Id ) 
    {
        var selectedDuty = _context.Duties.Find(Id);
        if (selectedDuty == null)
        {
            TempData["UpdateDutyFindError"] = "Güncellemek istediðiniz görev bulunamadý.";
            return RedirectToAction("Duty");
        }
        return View(selectedDuty); 
    }

    [HttpPost]
    public IActionResult UpdateDuty(Duty duty)
    {
        if (!ModelState.IsValid)
        {
            TempData["UpdateDutyIsValidError"] = "Lütfen tüm alanlarý doldurunuz.";
            return RedirectToAction("Duty");
        }
        _context.Duties.Update(duty);
        _context.SaveChanges();
        TempData["UpdateDutySuccess"] = "Ekleme Ýþlemi Baþarýlý";
        return RedirectToAction("Duty");
    }

    
    public IActionResult DeleteDuty(int Id)
    {
        var deletedDuty = _context.Duties.Find(Id);
        if (deletedDuty == null)
        {
            TempData["DeleteDutyFindError"] = "Silmek istediðiniz görev bulunamadý.";
            return RedirectToAction("Duty");
        }
        _context.Duties.Remove(deletedDuty);
        _context.SaveChanges();
        TempData["DeleteDutySuccess"] = "Silme Ýþlemi Baþarýlý";
        return RedirectToAction("Duty");
    }


    [HttpPost]
    public IActionResult AddCategory(Category category)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("AddCategory");
        }
        _context.Categories.Add(category);
        _context.SaveChanges();
        return RedirectToAction("Category");
    }
    [HttpGet]
    public IActionResult UpdateCategory(int Id) 
    {
        var selectedCategory = _context.Categories.Find(Id);
        if (selectedCategory == null)
        {
            return RedirectToAction("UpdateCategory");
        }
        return View(selectedCategory);
    }
    [HttpPost]
    public IActionResult UpdateCategory(Category category)
    {
        if (!ModelState.IsValid)
        {
            
            return RedirectToAction("UpdateCategory");
        }
        _context.Categories.Update(category);
        _context.SaveChanges();
        return RedirectToAction("Category");
    }

    public IActionResult DeleteCategory(int Id)
    {
        var deletedCategory = _context.Categories.Find(Id);
        if (deletedCategory == null)
        {
            return RedirectToAction("DeleteCategory");
        }
        _context.Categories.Remove(deletedCategory);
        _context.SaveChanges();
        return RedirectToAction("Category");
    }

    [HttpPost]
    public IActionResult AddWorker(Worker worker)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("AddWorker");
        }

        _context.Workers.Add(worker);
        _context.SaveChanges();
        return RedirectToAction("Worker");

    }

    [HttpGet]
    public IActionResult UpdateWorker(int Id) 
    {
        var selectedWorker = _context.Workers.Find(Id);
        if (selectedWorker == null)
        {
            return RedirectToAction("UpdateWorker");
        }
        return View();
    }
    [HttpPost]
    public IActionResult UpdateWorker(Worker worker)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("UpdateWorker");
        }
        _context.Workers.Update(worker);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult DeleteWorker(int Id) 
    {
        var deletedWorker = _context.Workers.Find(Id);
        if (deletedWorker == null)
        {
            return RedirectToAction("DeleteWorker");
        }
        _context.Workers.Remove(deletedWorker);
        _context.SaveChanges();
        return RedirectToAction("Worker");
    }

    [HttpPost]
    public IActionResult TaskAssignment(WorkerCategoryDuty taskassigment) 
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("TaskAssignment");
        }

        _context.WorkerCategoryDuties.Add(taskassigment);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult UpdateTaskAssignment(int Id)
    {
        var selectedTaskAssignment = _context.WorkerCategoryDuties.Find(Id);
        if (selectedTaskAssignment == null)
        {
            return RedirectToAction("UpdateTaskAssignment");
        }
        return View(selectedTaskAssignment);
    }
    [HttpPost]
    public IActionResult UpdateTaskAssignment(WorkerCategoryDuty taskassigment)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("UpdateTaskAssignment");
        }
        _context.WorkerCategoryDuties.Update(taskassigment);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult DeleteTaskAssignment(int Id) 
    {
        var deletedTaskAssignment = _context.WorkerCategoryDuties.Find(Id);
        if (deletedTaskAssignment == null)
        {
            return RedirectToAction("DeleteTaskAssignment");
        }
        _context.WorkerCategoryDuties.Remove(deletedTaskAssignment);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
