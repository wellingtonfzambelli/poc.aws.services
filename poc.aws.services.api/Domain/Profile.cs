namespace poc.aws.services.api.Domain;

public sealed class Profile
{
    public Profile(string name, string email, string photoId)
    {
        Name = name;
        Email = email;
        PhotoId = photoId;
        IsActive = false;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public bool IsActive { get; private set; }
    public string PhotoId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
}