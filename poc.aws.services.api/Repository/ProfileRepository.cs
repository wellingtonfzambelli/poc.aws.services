using Dapper;
using poc.aws.services.api.Domain;
using System.Data;

namespace poc.aws.services.api.Repository;

public sealed class ProfileRepository : IProfileRepository
{
    private readonly IDbConnection _connection;
    private readonly IDbTransaction _transaction;

    public ProfileRepository(IDbConnection connection, IDbTransaction transaction)
    {
        _connection = connection;
        _transaction = transaction;
    }

    public async Task<Profile?> GetByIdAsync(Guid id) =>
      (await _connection.QueryAsync<Profile>(
          "SELECT * FROM profile WHERE id = @id LIMIT 1",
          new { id },
          transaction: _transaction
      )).FirstOrDefault();

    public async Task<int> AddAsync(Profile profile)
    {
        var sql = @"INSERT INTO profile
                    (
                        name, 
                        email,                         
                        isActive,
                        photoId,
                        createdat
                    ) 
                    VALUES 
                    (
                        @Name, 
                        @Email, 
                        @IsActive, 
                        @PhotoId,                         
                        @CreatedAt
                    )";

        return await _connection.ExecuteAsync(
            sql,
            profile,
            transaction: _transaction
        );
    }
}