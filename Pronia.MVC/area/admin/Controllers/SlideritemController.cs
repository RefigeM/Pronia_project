using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.DAL.Contexts;
using Pronia.DAL.Models;

public class SliderController : Controller
{
    private readonly ProniaDBContext _context;

    public SliderController(ProniaDBContext context)
    {
        _context = context;
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SliderItem slider)
    {
        if (ModelState.IsValid)
        {
            _context.Add(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); 
        }
        return View(slider);
    }
    public async Task<IActionResult> Index()
    {
        var sliderItems = await _context.SliderItems.OrderBy(s => s.DisplayOrder).ToListAsync(); 
        return View(sliderItems);
    }
    public async Task<IActionResult> Update(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var sliderItem = await _context.SliderItems.FindAsync(id);
        if (sliderItem == null)
        {
            return NotFound();
        }

        return View(sliderItem); 
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, SliderItem sliderItem)
    {
        if (id != sliderItem.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(sliderItem);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SliderItemExists(sliderItem.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(sliderItem);
    }

    private bool SliderItemExists(int id)
    {
        return _context.SliderItems.Any(e => e.Id == id);
    }
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var sliderItem = await _context.SliderItems
            .FirstOrDefaultAsync(m => m.Id == id);
        if (sliderItem == null)
        {
            return NotFound();
        }

        return View(sliderItem); 
    }
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var sliderItem = await _context.SliderItems.FindAsync(id);
        _context.SliderItems.Remove(sliderItem);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index)); 
    }
}
