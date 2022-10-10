using System.Collections.Generic;
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
        private readonly IMongoCollection<EnhancedText> enhancedTextsCollection;

        public EnhancedTextRepository(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            enhancedTextsCollection =
                mongoDatabase.GetCollection<EnhancedText>(databaseSettings.Value.EnhancedTextsCollection);
        }

        public async Task<List<EnhancedText>> GetAllAsync() =>
            await enhancedTextsCollection.Find(_ => true).ToListAsync();

        public async Task<EnhancedText> GetFirstAsync(string id) =>
            await enhancedTextsCollection.Find(text => text.Id == id).Limit(1).SingleOrDefaultAsync();

        public async Task<EnhancedText> AddAsync(EnhancedText entity)
        {
            await enhancedTextsCollection.InsertOneAsync(entity);
            return entity;
        }

        public async Task RemoveOne(EnhancedText entity) =>
            await enhancedTextsCollection.DeleteOneAsync(x => x.Id == entity.Id);


        public async Task<EnhancedText> UpdateOne(string id, EnhancedText entity)
        {
            await enhancedTextsCollection.ReplaceOneAsync(text => text.Id == id, entity);
            return await GetFirstAsync(id);
        }
    }
}