using Pedal.Entities;
using Pedal.Repositories;

namespace Pedal.Services
{
    public class SwipeService
    {
        public SwipeRepository swipeRepository;
        public CarService carService;

        public SwipeService(SwipeRepository swipeRepository, CarService carService)
        {
            this.swipeRepository = swipeRepository;
            this.carService = carService;
        }

        public async Task<Swipe> AddSwipeAsync(string swiperId, string swipedId, bool swipeDirection)
        {
            if (await SwipeExists(swiperId, swipedId))
            {
                return await UpdateSwipeAsync(swiperId, swipedId, swipeDirection);
            }
            if (! await carService.CarWithIdExistsAsync(swiperId))
            {
                throw new InvalidDataException($"Car (which is swiping) with id: {swiperId} does not exist.");
            }
            if (! await carService.CarWithIdExistsAsync(swipedId))
            {
                throw new InvalidDataException($"Car (which is getting swiped) with id: {swipedId} does not exist.");
            }
            return await swipeRepository.CreateSwipeAsync(new Swipe()
            {
                Id = "",
                SwipedId = swipedId,
                SwiperId = swiperId,
                SwipeDirection = swipeDirection,
                SwipeTime = DateTime.Now,
            });
        }

        public async Task<Swipe[]> GetSwipesForIdAsync(string swipedId)
        {
            return (await swipeRepository.GetSwipesBySwipedIdAsync(swipedId)).ToArray();
        }

        public async Task<Swipe[]> GetSwipesByIdAsync(string swiperId)
        {
            return (await swipeRepository.GetSwipesBySwiperIdAsync(swiperId)).ToArray();
        }

        public Car[] GetCarsSwipedOnBy(string swiperId)
        {
            throw new Exception();
        }

        public async Task<Car[]> GetCarsSwipedOnAsync(string swipedId)
        {
            var swipes = await GetSwipesForIdAsync(swipedId);
            var tasks = swipes.Select(x => carService.GetCarByIdAsync(x.SwipedId));
            return await Task.WhenAll(tasks);
        }


        public async Task<Swipe> UpdateSwipeAsync(string swiperId, string swipedId, bool swipeDirection)
        {
            if (!await carService.CarWithIdExistsAsync(swiperId))
            {
                throw new InvalidDataException($"Car (which is swiping) with id: {swiperId} does not exist.");
            }
            if (!await carService.CarWithIdExistsAsync(swipedId))
            {
                throw new InvalidDataException($"Car (which is getting swiped) with id: {swipedId} does not exist.");
            }
            var updatedSwipe = await swipeRepository.GetBySwiperAndSwipedAsync(swiperId, swipedId);
            if (updatedSwipe == null)
            {
                return await AddSwipeAsync(swiperId, swipedId, swipeDirection);
            }
            updatedSwipe.SwipeTime = DateTime.Now;
            return await swipeRepository.UpdateSwipeAsync(updatedSwipe);
        }

        public async void DeleteSwipeAsync(string swipeId)
        {
            if (await SwipeWithIdExistsAsync(swipeId))
            {
                throw new InvalidOperationException($"Swipe with id: {swipeId} does not exist.");
            }
            swipeRepository.RemoveAsync(swipeId).Wait();
        }

        private async Task<bool> SwipeWithIdExistsAsync(string swipeId)
        { 
            return (await swipeRepository.GetAsync(swipeId)) != null;
        }

        private async Task<bool> SwipeExists(string swiperId, string swipedId)
        {
            return (await swipeRepository.GetBySwiperAndSwipedAsync(swiperId, swipedId)) != null;
        }

        public async Task<Car[]?> GetMatches(string carId)
        {
            var swipes = await swipeRepository.GetMatchesAsync(carId);
            if (swipes == null)
                return null;
            var tasks = swipes.Select(async s => await carService.GetCarByIdAsync(s.SwipedId));
            var cars = await Task.WhenAll(tasks);
            return cars;
        }
    }
}
