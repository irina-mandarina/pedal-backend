using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Pedal.Entities;
using Pedal.Models;

namespace Pedal.Repositories
{
    public class SwipeRepository
    {
        private readonly IMongoCollection<Swipe> _swipesCollection;

        public SwipeRepository(
            IOptions<StoreDatabaseSettings> carStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                carStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                carStoreDatabaseSettings.Value.DatabaseName);

            _swipesCollection = mongoDatabase.GetCollection<Swipe>(
                carStoreDatabaseSettings.Value.SwipesCollectionName);
        }

        public async Task<List<Swipe>> GetAsync() =>
            await _swipesCollection.Find(_ => true).ToListAsync();

        public async Task<Swipe?> GetAsync(string id) =>
            await _swipesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Swipe newSwipe) =>
            await _swipesCollection.InsertOneAsync(newSwipe);

        public async Task UpdateAsync(string id, Swipe updatedSwipe) =>
            await _swipesCollection.ReplaceOneAsync(x => x.Id == id, updatedSwipe);

        public async Task RemoveAsync(string id) =>
            await _swipesCollection.DeleteOneAsync(x => x.Id == id);
    }
}
