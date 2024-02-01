using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Pedal.Entities;
using Pedal.Models;

namespace Pedal.Repositories
{
    public class CarRepository
    {
        private readonly IMongoCollection<Car> _carsCollection;

        public CarRepository(
            IOptions<CarStoreDatabaseSettings> carStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                carStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                carStoreDatabaseSettings.Value.DatabaseName);

            _carsCollection = mongoDatabase.GetCollection<Car>(
                carStoreDatabaseSettings.Value.CarsCollectionName);
        }

        public async Task<List<Car>> GetAsync() =>
            await _carsCollection.Find(_ => true).ToListAsync();

        public async Task<Car?> GetAsync(string id) =>
            await _carsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Car newCar) =>
            await _carsCollection.InsertOneAsync(newCar);

        public async Task UpdateAsync(string id, Car updatedCar) =>
            await _carsCollection.ReplaceOneAsync(x => x.Id == id, updatedCar);

        public async Task RemoveAsync(string id) =>
            await _carsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
