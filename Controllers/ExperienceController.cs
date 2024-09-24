using experience_survey_backend.Data;
using experience_survey_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace experience_survey_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienceController : ControllerBase
    {
        private readonly SurveyContext _context;
         
        public ExperienceController(SurveyContext context)
        {
            _context = context;
        }

        // GET: api/experience
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Experience>>> GetExperiences()
        {
            var experiences = await _context.Experiences
                .OrderBy(e => e.ExperienceOrder) 
                .ToListAsync();
            return experiences;
        }

        // GET: api/experience/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Experience>> GetExperience(int id)
        {
            var experience = await _context.Experiences.FindAsync(id);

            if (experience == null)
            {
                return NotFound();
            }

            return experience;
        }

    }
}