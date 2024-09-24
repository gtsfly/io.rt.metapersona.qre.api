using experience_survey_backend.Data;
using experience_survey_backend.Models;
using Microsoft.AspNetCore.Mvc;
using experience_survey_backend.Dtos;
using Microsoft.EntityFrameworkCore;

namespace experience_survey_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRatingController : ControllerBase
    {
        private readonly SurveyContext _context;

        public UserRatingController(SurveyContext context)
        {
            _context = context;
        }

        [HttpPost("rate")]
        public async Task<IActionResult> RateHotel([FromBody] UserRatingDto ratingDto)
        {
            // Check if a rating already exists for the same user, hotel, and experience
            var existingRating = await _context.UserRatings
                .FirstOrDefaultAsync(r => r.UserId == ratingDto.UserId
                                          && r.HotelId == ratingDto.HotelId
                                          && r.ExperienceId == ratingDto.ExperienceId);

            if (existingRating != null)
            {
                // Update the existing rating
                existingRating.Rating = ratingDto.Rating;
                _context.UserRatings.Update(existingRating);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Rating updated successfully" });
            }
            else
            {
                // If no existing rating, create a new one
                var newRating = new UserRating
                {
                    UserId = ratingDto.UserId,
                    HotelId = ratingDto.HotelId,
                    ExperienceId = ratingDto.ExperienceId,
                    Rating = ratingDto.Rating
                };

                _context.UserRatings.Add(newRating);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Rating saved successfully" });
            }
        }
        [HttpGet("ratings")]
        public async Task<IActionResult> GetUserRatings([FromQuery] int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("Kullanıcı ID'si gereklidir.");
            }

            var userRatings = await _context.UserRatings
                .Where(r => r.UserId == userId)
                .Select(r => new UserRatingDto
                {
                    UserId = r.UserId,
                    HotelId = r.HotelId,
                    ExperienceId = r.ExperienceId,
                    Rating = r.Rating
                })
                .ToListAsync();

            if (userRatings == null || !userRatings.Any())
            {
                return NotFound("Bu kullanıcı için oy bulunamadı.");
            }

            return Ok(userRatings);
        }
    }

}
