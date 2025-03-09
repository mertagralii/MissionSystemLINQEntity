using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    public IActionResult Index(int? categoryId, int? workerId)
    {

        var query = _context.WorkerCategoryDuties
        .Include(w => w.Worker)
        .Include(c => c.Category)
        .Include(d => d.Duty)
        .Where(p => p.Duty.IsActive == true && p.Duty.IsCompleted == false)
        .AsQueryable(); // Sorguyu dinamik hale getir

        if (categoryId.HasValue) // Eðer categoryId deðeri varsa (HasValue ==> HasValue, nullable (int?, double?, DateTime? gibi) deðiþkenlerde kullanýlýr ve deðiþkenin içinde bir deðer olup olmadýðýný kontrol eder.)
        {
            query = query.Where(p => p.Category.Id == categoryId); // Kategori Id'sine göre filtrele (Yukardaki query'de tüm sorgu var, burada da o tüm sorguya where ekledik filtreledik.)
        }

        if (workerId.HasValue) // Eðer workerId deðeri varsa
        {
            query = query.Where(p => p.Worker.Id == workerId); // Worker Id'sine göre filtrele (Yukardaki query'de tüm sorgu var, burada da o tüm sorguya where ekledik filtreledik.)
        }

        var taskAssignmentsList = query.ToList(); // Sorguyu çalýþtýr ve listeye çevir

        // ViewBag ile kategorileri ve çalýþanlarý gönder
        ViewBag.Categories = _context.Categories.ToList();
        ViewBag.Workers = _context.Workers.ToList();

        return View(taskAssignmentsList);
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
    #region Duty CRUD Operations
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
        duty.UpdateDate = DateTime.Now;
        _context.Duties.Update(duty);
        _context.SaveChanges();
        TempData["UpdateDutySuccess"] = "Güncelleme Ýþlemi Baþarýlý";
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
    #endregion


    #region Category CRUD Operations
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
    #endregion

    #region Worker CRUD Operations
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
        return View(selectedWorker);
    }
    [HttpPost]
    public IActionResult UpdateWorker(Worker worker)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("UpdateWorker");
        }
        worker.UpdateDate = DateTime.Now;
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
    #endregion

    #region TaskAssignment CRUD Operations

    [HttpGet]
    public IActionResult TaskAssignment()
    {
        ViewBag.Workers = _context.Workers.ToList();
        ViewBag.Categories = _context.Categories.ToList();
        ViewBag.Duties = _context.Duties.ToList();

       var taskAssignments = _context.WorkerCategoryDuties
            .Include(w => w.Worker)
            .Include(c => c.Category)
            .Include(d => d.Duty)
            .ToList();

        return View(taskAssignments);
    }
    [HttpPost]
    public IActionResult AddTaskAssignment(WorkerCategoryDuty taskassigment)  // Ekleme
    {
        if (taskassigment.CategoryId == null)
        {
            return RedirectToAction("TaskAssignment");
        }
        if (taskassigment.WorkerId == null)
        {
            return RedirectToAction("TaskAssignment");
        }
        if (taskassigment.DutyId == null)
        {
            return RedirectToAction("TaskAssignment");
        }

        _context.WorkerCategoryDuties.Add(taskassigment);
        _context.SaveChanges();
        return RedirectToAction("TaskAssignment");
    }
    

    public IActionResult DeleteTaskAssignment(int Id) // Silme
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
    
    public IActionResult TaskAssignmentIsActiveted(int Id) 
    {
        var taskAssignment = _context.WorkerCategoryDuties
         .Include(wcd => wcd.Duty) // Duty iliþkisini dahil et
         .FirstOrDefault(wcd => wcd.Id == Id);
        taskAssignment.Duty.IsActive = true;
        _context.WorkerCategoryDuties.Update(taskAssignment);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    public IActionResult TaskAssignmentIsNotActiveted(int Id)
    {
        var taskAssignment = _context.WorkerCategoryDuties
         .Include(wcd => wcd.Duty) // Duty iliþkisini dahil et
         .FirstOrDefault(wcd => wcd.Id == Id);
        taskAssignment.Duty.IsActive = false;
        _context.WorkerCategoryDuties.Update(taskAssignment);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult TaskAssignmentIsCompleted(int Id) 
    {
        var taskAssignment = _context.WorkerCategoryDuties
         .Include(wcd => wcd.Duty) // Duty iliþkisini dahil et
         .FirstOrDefault(wcd => wcd.Id == Id);
        taskAssignment.Duty.IsCompleted = true;
        _context.WorkerCategoryDuties.Update(taskAssignment);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    public IActionResult TaskAssignmentIsNotCompleted(int Id)
    {
        var taskAssignment = _context.WorkerCategoryDuties
         .Include(wcd => wcd.Duty) // Duty iliþkisini dahil et
         .FirstOrDefault(wcd => wcd.Id == Id);
        taskAssignment.Duty.IsCompleted = false;
        _context.WorkerCategoryDuties.Update(taskAssignment);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    #endregion



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
