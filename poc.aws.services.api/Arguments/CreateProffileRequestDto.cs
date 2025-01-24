namespace poc.aws.services.api.Arguments;

public sealed record CreateProffileRequestDto
(
    string name, 
    string surname, 
    DateTime birth, 
    byte[] profileImage
);