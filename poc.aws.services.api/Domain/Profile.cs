namespace poc.aws.services.api.Domain;

public sealed class Profile
{
    //public Profile(string name, string email, string photoId)
    //{
    //    Name = name;
    //    Email = email;
    //    PhotoId = photoId;
    //    IsActive = false;
    //    CreatedAt = DateTime.UtcNow;
    //}

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public string PhotoId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}