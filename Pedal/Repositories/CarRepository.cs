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
            IOptions<StoreDatabaseSettings> carStoreDatabaseSettings)
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

        private async Task CreateAsync(Car newCar) =>
            await _carsCollection.InsertOneAsync(newCar);

        private async Task UpdateAsync(string id, Car updatedCar) =>
            await _carsCollection.ReplaceOneAsync(x => x.Id == id, updatedCar);

        public async Task RemoveAsync(string id) =>
            await _carsCollection.DeleteOneAsync(x => x.Id == id);

        public async Task<Car> CreateCarAsync(Car newCar)
        {
            await this.CreateAsync(newCar);

            return newCar;
        }

        public async Task<Car> UpdateCarAsync(Car updatedCar)
        {
            await _carsCollection.ReplaceOneAsync(x => x.Id == updatedCar.Id, updatedCar);
            return updatedCar;
        }

        public async Task<Car?> GetCarByEmailAsync(string email)
        {
            return await _carsCollection.Find(x => x.Email == email).FirstOrDefaultAsync();
        }


    }
}
