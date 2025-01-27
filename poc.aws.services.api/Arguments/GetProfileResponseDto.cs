namespace poc.aws.services.api.Arguments;

public sealed record GetProfileResponseDto
(
    string name,
    string email,
    string imageURL
);