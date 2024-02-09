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
            IOptions<StoreDatabaseSettings> swipeStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                swipeStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                swipeStoreDatabaseSettings.Value.DatabaseName);

            _swipesCollection = mongoDatabase.GetCollection<Swipe>(
                swipeStoreDatabaseSettings.Value.SwipesCollectionName);
        }

        public async Task<List<Swipe>> GetAsync() =>
            await _swipesCollection.Find(_ => true).ToListAsync();

        public async Task<Swipe?> GetAsync(string id) =>
            await _swipesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        private async Task CreateAsync(Swipe newSwipe) =>
            await _swipesCollection.InsertOneAsync(newSwipe);

        private async Task UpdateAsync(string id, Swipe updatedSwipe) =>
            await _swipesCollection.ReplaceOneAsync(x => x.Id == id, updatedSwipe);

        public async Task RemoveAsync(string id) =>
            await _swipesCollection.DeleteOneAsync(x => x.Id == id);


        public async Task<Swipe> CreateSwipeAsync(Swipe swipe)
        {
            await this.CreateAsync(swipe);

            return swipe;
        }

        public async Task<Swipe> UpdateSwipeAsync(Swipe updatedSwipe)
        {
            await _swipesCollection.ReplaceOneAsync(x => x.Id == updatedSwipe.Id, updatedSwipe);
            return updatedSwipe;
        }

        public async Task<Swipe?> GetBySwiperAndSwipedAsync(string swiperId, string swipedId)
        {
            return await _swipesCollection.Find(x => x.SwiperId == swiperId && x.SwipedId == swipedId).FirstOrDefaultAsync();
        }

        public async Task<List<Swipe>> GetSwipesBySwiperIdAsync(string swiperId, SwipeDirection? swipeDirection = null)
        {
            return await _swipesCollection.Find(x => x.SwiperId.Equals(swiperId) && (swipeDirection == null || (swipeDirection != null && x.SwipeDirection == swipeDirection))).ToListAsync();
        }

        public async Task<List<Swipe>> GetSwipesBySwipedIdAsync(string swipedId, SwipeDirection? swipeDirection = null)
        {
            return await _swipesCollection.Find(x => x.SwipedId.Equals(swipedId) && (swipeDirection == null || (swipeDirection != null && x.SwipeDirection == swipeDirection))).ToListAsync();
        }

        public async Task<List<Swipe>?> GetMatchesAsync(string carId)
        {
            var carsTheySwipedRightOn = (await GetSwipesBySwiperIdAsync(carId)).Where(x => x.SwipeDirection == true).Select(x => x.SwipedId).ToList();
            return _swipesCollection.Find(x => x.SwipeDirection == true && carsTheySwipedRightOn.Contains(x.SwiperId)).ToList();
        }
    }
}
