namespace ResumeParser.DTOs
{
    public class CandidateProfileDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public List<string> Skills { get; set; } = [];
    }
}
