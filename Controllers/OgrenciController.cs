using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace efcoreApp.Controllers;

public class OgrenciController : Controller
{
    private readonly DataContext _context;

    public OgrenciController(DataContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Ogrenciler.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Ogrenci model) //blok yapmasını engellemek için async
    {
        _context.Ogrenciler.Add(model);
        _context.SaveChangesAsync();
        return RedirectToAction("Index");
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ogr = await _context.Ogrenciler.FindAsync(id);
        
        if (ogr == null)
        {
            return NotFound();
        }

        return View(ogr);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]  //güvenlik önlemi
    public async Task<IActionResult> Edit(int id, Ogrenci model)
    {
        if (id != model.OgrenciId)
            return NotFound();
        
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(model);
                await _context.SaveChangesAsync(); //güncelleme yapar
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Ogrenciler.Any(o => o.OgrenciId == model.OgrenciId)) //herhangi bir kayıt yok mu
                    return NotFound();
                else
                    throw; //kaldığın yerden devam et
            }
            return RedirectToAction("Index");
        }
        return View(model);
    }

}