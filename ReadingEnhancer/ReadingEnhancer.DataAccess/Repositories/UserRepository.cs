using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ReadingEnhancer.DataAccess.Configurations;
using ReadingEnhancer.Domain.Entities;
using ReadingEnhancer.Domain.Repositories;

namespace ReadingEnhancer.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _userCollection;


    public UserRepository(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _userCollection = mongoDatabase.GetCollection<User>(databaseSettings.Value.UsersCollection);
    }

    public async Task<User> GetFirstAsync(string id) =>
        await _userCollection.Find(user => user.Id == id).Limit(1).SingleOrDefaultAsync();

    public async Task<List<User>> GetAllAsync()
        => await _userCollection.Find(_ => true).ToListAsync();

    public async Task<User> AddAsync(User entity)
    {
        await _userCollection.InsertOneAsync(entity);
        return entity;
    }

    public async Task<User> GetFirstAsync(Expression<Func<User, bool>> predicate)
    {
        return await _userCollection.Find(predicate).Limit(1).SingleOrDefaultAsync();
    }

    public async Task RemoveOne(User entity)
        => await _userCollection.DeleteOneAsync(x => x.Id == entity.Id);

    public async Task<User> UpdateOne(string id, User entity)
    {
        await _userCollection.ReplaceOneAsync(user => user.Id == id, entity);
        return await GetFirstAsync(id);
    }
}