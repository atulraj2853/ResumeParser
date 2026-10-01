using ResumeParser.DTOs;
using System.Text.RegularExpressions;

namespace ResumeParser.Services
{
    public class CandidateExtractionService
    {
        public CandidateProfileDto Extract(string text)
        {
            var profile = new CandidateProfileDto();

            profile.FullName = ExtractName(text);
            profile.Email = ExtractEmail(text);
            profile.PhoneNumber = ExtractPhone(text);
            profile.Skills = ExtractSkills(text);

            return profile;
        }
        private string ExtractName(string text)
        {
            var lines = text.Split(
            Environment.NewLine,
            StringSplitOptions.RemoveEmptyEntries);

            return lines.FirstOrDefault()?.Trim() ?? string.Empty;
        }
        private string ExtractEmail(string text)
        {
            var match = Regex.Match(
            text,
            @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}");

            return match.Success ? match.Value : string.Empty;
        }
        private string ExtractPhone(string text)
        {
            var match = Regex.Match(
            text,
            @"\+?\d[\d\s\-]{8,15}");

            return match.Success ? match.Value : string.Empty;
        }
        private List<string> ExtractSkills(string text)
        {
            var knownSkills = new List<string>
            {
                ".NET",
                "C#",
                "Angular",
                "SQL",
                "SQL Server",
                "JavaScript",
                "TypeScript",
                "Azure",
                "HTML",
                "CSS",
                "Git"
            };
            return knownSkills
                .Where(skill =>
                text.Contains(skill,
                StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

    }
}
