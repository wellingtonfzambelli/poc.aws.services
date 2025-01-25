using Microsoft.AspNetCore.Mvc;
using poc.aws.services.api.Arguments;
using poc.aws.services.api.Domain;
using poc.aws.services.api.Repository.UnitOfWork;

namespace poc.aws.services.api.Controllers;

[ApiController]
[Route("profile")]
public sealed class ProfileController : ControllerBase
{
    private readonly ILogger<ProfileController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ProfileController(IUnitOfWork unitOfWork, ILogger<ProfileController> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    [HttpGet("{profileId}")]
    public async Task<IActionResult> GetProfileByIdAsync(Guid profileId, CancellationToken ct)
    {
        if (await _unitOfWork.Profiles.GetByIdAsync(profileId)
            is var profile && profile is null)
            return NotFound();

        return Ok(profile);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProfileAsync
    (
        [FromBody] CreateProffileRequestDto request,
        //[FromForm] IFormFile file, 
        CancellationToken ct
    )
    {
        var profile = new Profile
        {
            Id = Guid.NewGuid(),
            Email = request.email,
            CreatedAt = DateTime.Now,
            IsActive = true,
            Name = request.name,
            PhotoId = "8383"
        };

        if (await _unitOfWork.Profiles.AddAsync(profile)
            is var created && created > 0)
            return BadRequest();

        return Created();
    }
}