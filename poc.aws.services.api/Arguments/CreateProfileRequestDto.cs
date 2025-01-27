namespace poc.aws.services.api.Arguments;

public sealed record CreateProfileRequestDto
(
    string name,
    string email,
    IFormFile? ProfileImage
);