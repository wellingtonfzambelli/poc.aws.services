using Microsoft.AspNetCore.Mvc;
using poc.aws.services.api.Arguments;

namespace poc.aws.services.api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class ProfileController : ControllerBase
{
    private readonly ILogger<ProfileController> _logger;

    public ProfileController(ILogger<ProfileController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProfileAsync(CreateProffileRequestDto request, CancellationToken ct)
    {

        return Created();
    }
}