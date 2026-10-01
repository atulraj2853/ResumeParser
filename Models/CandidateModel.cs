namespace ResumeParser.Models
{
    public class CandidateModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string TotalExperience { get; set; }
        public string Skills { get; set; }
        public string Education { get; set; }
        public string ResumeFileName { get; set; }
        public string GeneratedDocument { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
