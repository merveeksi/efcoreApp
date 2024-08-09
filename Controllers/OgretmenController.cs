using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;

namespace efcoreApp.Controllers;

public class OgretmenController : Controller
{
    private readonly DataContext _context;

    public OgretmenController(DataContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> Index()
    {
        return View(await _context.Ogretmenler.ToListAsync());
    }
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Ogretmen model) //blok yapmasını engellemek için async
    {
        _context.Ogretmenler.Add(model);
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

        var entity = await _context
            .Ogretmenler
            .FirstOrDefaultAsync(o => o.OgretmenId == id);
        
        if (entity == null)
        {
            return NotFound();
        }

        return View(entity);
    }
    

    [HttpPost]
    [ValidateAntiForgeryToken]  //güvenlik önlemi
    public async Task<IActionResult> Edit(int id, Ogretmen model)
    {
        if (id != model.OgretmenId)
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
                if (!_context.Ogretmenler.Any(o => o.OgretmenId == model.OgretmenId)) //herhangi bir kayıt yok mu
                    return NotFound();
                else
                    throw; //kaldığın yerden devam et
            }
            return RedirectToAction("Index");
        }
        return View(model);
    }

}