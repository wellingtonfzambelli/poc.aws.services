﻿using Dapper;
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

    public async Task<UserProfile?> GetByIdAsync(Guid id) =>
      (await _connection.QueryAsync<UserProfile>(
          "SELECT * FROM profile WHERE id = @id LIMIT 1",
          new { id },
          transaction: _transaction
      )).FirstOrDefault();

    public async Task<int> AddAsync(UserProfile profile)
    {
        var sql = @"INSERT INTO profile
                    (
                        name, 
                        email,                         
                        isactive,
                        photoid
                    ) 
                    VALUES 
                    (
                        @Name,
                        @Email,
                        @IsActive,
                        @PhotoId
                    )";

        return await _connection.ExecuteAsync(
            sql,
            profile,
            transaction: _transaction
        );
    }
}