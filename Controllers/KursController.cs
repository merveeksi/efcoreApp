using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using efcoreApp.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace efcoreApp.Controllers;

public class KursController : Controller
{
    private readonly DataContext _context;

    public KursController(DataContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var kurslar = await _context.Kurslar.ToListAsync();
        return View(kurslar);
    }
    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]  
    public async Task<IActionResult> Create(Kurs model)
    {
        _context.Kurslar.Add(model);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var kurs = await _context.Kurslar.FindAsync(id);
        
        if (kurs == null)
        {
            return NotFound();
        }

        return View(kurs);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]  //güvenlik önlemi
    public async Task<IActionResult> Edit(int id, Kurs model)
    {
        if (id != model.KursId)
            return NotFound();
        
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(model);
                await _context.SaveChangesAsync(); //güncelleme yapar
            }
            catch (DbUpdateException)
            {
                if (!_context.Ogrenciler.Any(o => o.KursId == model.KursId)) //herhangi bir kayıt yok mu
                    return NotFound();
                else
                    throw; //kaldığın yerden devam et
            }
            return RedirectToAction("Index");
        }
        return View(model);
    }
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();
        var kurs = await _context.Kurslar.FindAsync(id);

        if (kurs == null)
            return NotFound();
        
        return View(kurs);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromForm] int id)
    {
        var kurs = await _context.Kurslar.FindAsync(id);
        if (kurs == null)
            return NotFound();
        
        _context.Kurslar.Remove(kurs);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index"); //yönlendir
    }
    
}