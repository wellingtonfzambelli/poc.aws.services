using poc.aws.services.api.Domain;

namespace poc.aws.services.api.Repository;

public interface IProfileRepository
{
    Task<UserProfile?> GetByIdAsync(Guid id);
    Task<int> AddAsync(UserProfile profile);
}