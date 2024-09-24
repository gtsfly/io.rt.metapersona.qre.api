namespace experience_survey_backend.Models
{
    public class Experience
    {
        public int ExperienceId { get; set; }
        public string ExperienceName { get; set; }
        public int ExperienceOrder { get; set; }

        public ICollection<Hotel> Hotels { get; set; }
    }

}
