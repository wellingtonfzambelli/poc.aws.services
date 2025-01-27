using poc.aws.services.api.Arguments;
using poc.aws.services.api.Repository.UnitOfWork;
using poc.aws.services.api.Services.Interfaces;

namespace poc.aws.services.api.Services;

public sealed class ProfileService : IProfileService
{
    private readonly IAwsS3Service _awsS3Service;
    private readonly IAwsSQSService _awsSQSService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ProfileService> _logger;

    public ProfileService
    (
        IAwsS3Service awsS3Service,
        IAwsSQSService awsSQSService,
        IUnitOfWork unitOfWork,
        ILogger<ProfileService> logger
    )
    {
        _awsS3Service = awsS3Service;
        _awsSQSService = awsSQSService;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<GetProfileResponseDto?> GetProfileByIdAsync(Guid profileId, CancellationToken ct)
    {
        if (await _unitOfWork.Profiles.GetByIdAsync(profileId)
            is var profile && profile is null)
            return null;

        var photoURL = await _awsS3Service.GetPresignedUrlAsync(profile.PhotoId);

        return new GetProfileResponseDto(profile.Name, profile.Email, photoURL);
    }

    public async Task CreateProfileAsync(CreateProfileRequestDto request, CancellationToken ct)
    {
        string newImageName = Guid.NewGuid().ToString();

        if (await _awsS3Service.UploadFileToS3Async(request.ProfileImage, newImageName, ct) == false)
            throw new Exception("Some issue happened when save the image");


    }

    public async Task SaveProfileDatabaseAsync()
    {
        //var profile = new UserProfile(request.name, request.email, newImageName);

        //if (await _unitOfWork.Profiles.AddAsync(profile)
        //    is var created && created == 0)
        //    return BadRequest("Some issue happened to salve the profile data");

        //_unitOfWork.Commit();
    }
}