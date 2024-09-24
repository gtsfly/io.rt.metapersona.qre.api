namespace experience_survey_backend.Models
{
    public class Hotel
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string Location { get; set; }

        public ICollection<Experience> Experiences { get; set; }
    }

}
