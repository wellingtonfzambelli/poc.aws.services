using poc.aws.services.api.Arguments;

namespace poc.aws.services.api.Services.Interfaces;

public interface IProfileService
{
    Task<GetProfileResponseDto> GetProfileByIdAsync(Guid profileId, CancellationToken ct);
    Task CreateProfileAsync(CreateProfileRequestDto request, CancellationToken ct);
}