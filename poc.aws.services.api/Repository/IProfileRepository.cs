using poc.aws.services.api.Domain;

namespace poc.aws.services.api.Repository;

public interface IProfileRepository
{
    Task<Profile?> GetByIdAsync(Guid id);
    Task<int> AddAsync(Profile profile);
}