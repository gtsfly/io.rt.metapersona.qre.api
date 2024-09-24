using System.ComponentModel.DataAnnotations;

namespace experience_survey_backend.Dtos
{
    public class UserRatingDto
    {
        [Key]
        public int UserId { get; set; }
        public int HotelId { get; set; }
        public int ExperienceId { get; set; }
        public int Rating { get; set; }
    }
}
