using Microsoft.AspNetCore.Mvc;
using ResumeParser.Services;

namespace ResumeParser.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResumeController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;
    private readonly ResumeParsingService _resumeParsingService;
    private readonly CandidateExtractionService _candidateExtractionService;

    public ResumeController(
        IWebHostEnvironment environment,
        ResumeParsingService resumeParsingService,
        CandidateExtractionService candidateExtractionService)
    {
        _environment = environment;
        _resumeParsingService = resumeParsingService;
        _candidateExtractionService = candidateExtractionService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadResume(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Please upload a valid PDF file.");
        }

        var resumesFolder = Path.Combine(
            _environment.WebRootPath,
            "resumes");

        if (!Directory.Exists(resumesFolder))
        {
            Directory.CreateDirectory(resumesFolder);
        }

        var fileName =
            $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

        var filePath = Path.Combine(
            resumesFolder,
            fileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var extractedText =
            _resumeParsingService.ExtractTextFromPdf(filePath);
        var candidate =
            _candidateExtractionService.Extract(extractedText);

        return Ok(candidate);
    }
}