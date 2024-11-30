using Microsoft.EntityFrameworkCore;
using Pronia.BL.Services.Abstractions;
using Pronia.DAL.Contexts;
using Pronia.DAL.Models;

namespace Pronia.MVC
{
    namespace YourProject.Services
    {
        public class SliderItemService : ISliderItemService
        {
            private readonly ProniaDBContext _context;

            public SliderItemService(ProniaDBContext context)
            {
                _context = context;
            }

            public async Task<List<SliderItem>> GetAllSliderItemsAsync()
            {
                return await _context.SliderItems.ToListAsync();  
            }
        }
    }
}
