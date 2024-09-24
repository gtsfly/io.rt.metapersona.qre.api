using experience_survey_backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace experience_survey_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly SurveyContext _context;

        public HotelController(SurveyContext context)
        {
            _context = context;
        }

        [HttpGet("hotels")]
        public async Task<IActionResult> GetHotels()
        {
            var hotels = await _context.Hotels
                                       .Include(h => h.Experiences)
                                       .ToListAsync();
            return Ok(hotels);
        }
    }

}
