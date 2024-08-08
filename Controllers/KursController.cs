using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using efcoreApp.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace efcoreApp.Controllers;

[Route("api/[controller]")] //
[ApiController]    //
public class KursController : Controller
{
    private readonly DataContext _context;

    public KursController(DataContext context)
    {
        _context = context;
    }

    public IActionResult Create()
    {
        return View();
    }
    
    [HttpGet] ///
    public async Task<ActionResult<IEnumerable<KursKayit>>> GetKursKayitlar()
    {
        return await _context.KursKayitlar.ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Kurs model)
    {
        _context.Kurslar.Add(model);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpDelete("{ogrenciId}/{kursId}")]
    public async Task<IActionResult> DeleteKursKayit(int ogrenciId, int kursId)
    {
        var kursKayit = await _context.KursKayitlar.FindAsync(ogrenciId, kursId);
        if (kursKayit == null)
        {
            return NotFound();
        }

        _context.KursKayitlar.Remove(kursKayit);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}