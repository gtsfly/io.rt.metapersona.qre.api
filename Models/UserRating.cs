using System.ComponentModel.DataAnnotations;

namespace experience_survey_backend.Models
{
    public class UserRating
    {
        [Key]
        public int RatingId { get; set; }
        public int UserId { get; set; }
        public int HotelId { get; set; }
        public int ExperienceId { get; set; }
        public int Rating { get; set; }
        public Hotel Hotel { get; set; }
        public Experience Experience { get; set; }
    }

}
