using Microsoft.AspNetCore.Mvc;
using poc.aws.services.api.Arguments;
using poc.aws.services.api.Services.Interfaces;

namespace poc.aws.services.api.Controllers;

[ApiController]
[Route("profile")]
public sealed class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;
    private readonly ILogger<ProfileController> _logger;

    public ProfileController(IProfileService profileService, ILogger<ProfileController> logger)
    {
        _profileService = profileService;
        _logger = logger;
    }

    [HttpGet("{profileId}")]
    public async Task<IActionResult> GetProfileByIdAsync(Guid profileId, CancellationToken ct)
    {
        if (await _profileService.GetProfileByIdAsync(profileId)
            is var profile && profile is null)
            return NotFound();

        return Ok(profile);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> CreateProfileAsync
    (
        [FromForm] CreateProfileRequestDto request,
        CancellationToken ct
    )
    {
        if (request.ProfileImage is null)
            return BadRequest("image is required");

        if (ValidateImageFile(request.ProfileImage) is var formatValid && formatValid is not null)
            return BadRequest(formatValid.ToString());

        await _profileService.CreateProfileAsync(request, ct);

        return Created();
    }

    private string? ValidateImageFile(IFormFile file)
    {
        // Check the MIME type of the file
        var validImageTypes = new[] { "image/jpeg", "image/png", "image/gif" };
        if (!validImageTypes.Contains(file.ContentType))
            return "Only image files (JPEG, PNG, GIF) are allowed.";

        var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        var fileExtension = Path.GetExtension(file.FileName);
        if (!validExtensions.Contains(fileExtension.ToLower()))
        {
            return "Invalid file extension. Only JPG, JPEG, PNG, and GIF are allowed.";
        }

        return null; // Valid file
    }
}