using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ReadingEnhancer.DataAccess.Configurations;
using ReadingEnhancer.Domain.Entities;
using ReadingEnhancer.Domain.Repositories;

namespace ReadingEnhancer.DataAccess.Repositories
{
    public class EnhancedTextRepository : IEnhancedTextRepository
    {
        private readonly IMongoCollection<EnhancedText> _enhancedTextsCollection;
        private readonly Random _random;

        public EnhancedTextRepository(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _enhancedTextsCollection =
                mongoDatabase.GetCollection<EnhancedText>(databaseSettings.Value.EnhancedTextsCollection);
            _random = new Random();
        }

        public async Task<List<EnhancedText>> GetAllAsync() =>
            await _enhancedTextsCollection.Find(_ => true).ToListAsync();

        public async Task<EnhancedText> GetRandomAsync()
        {
            var allTexts = await GetAllAsync();
            var index = _random.Next(allTexts.Count);
            return allTexts[index];
        }

        public async Task<EnhancedText> GetFirstAsync(string id) =>
            await _enhancedTextsCollection.Find(text => text.Id == id).Limit(1).SingleOrDefaultAsync();

        public async Task<EnhancedText> AddAsync(EnhancedText entity)
        {
            await _enhancedTextsCollection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<EnhancedText> GetFirstAsync(Expression<Func<EnhancedText, bool>> predicate)
        {
            var enhancedText = await _enhancedTextsCollection.Find(predicate).Limit(1).SingleOrDefaultAsync();
            return enhancedText;
        }

        public async Task RemoveOne(EnhancedText entity) =>
            await _enhancedTextsCollection.DeleteOneAsync(x => x.Id == entity.Id);


        public async Task<EnhancedText> UpdateOne(string id, EnhancedText entity)
        {
            await _enhancedTextsCollection.ReplaceOneAsync(text => text.Id == id, entity);
            return await GetFirstAsync(id);
        }
    }
}