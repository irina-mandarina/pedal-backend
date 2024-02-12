using Pedal.Entities;
using Pedal.Entities.Enums;
using Pedal.Models;
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

        public async Task<Swipe> AddSwipeAsync(SwipeRequest swipeRequest)
        {
            if (await SwipeExists(swipeRequest.SwiperId, swipeRequest.SwipedId))
            {
                var existingSwipe = await swipeRepository.GetBySwiperAndSwipedAsync(swipeRequest.SwiperId, swipeRequest.SwipedId);
                existingSwipe!.SwipeDirection = swipeRequest.SwipeDirection;
                return await UpdateSwipeAsync(existingSwipe);
            }
            if (! await carService.CarWithIdExistsAsync(swipeRequest.SwiperId))
            {
                throw new InvalidDataException($"Car (which is swiping) with id: {swipeRequest.SwiperId} does not exist.");
            }
            if (! await carService.CarWithIdExistsAsync(swipeRequest.SwipedId))
            {
                throw new InvalidDataException($"Car (which is getting swiped) with id: {swipeRequest.SwipedId} does not exist.");
            }
            return await swipeRepository.CreateSwipeAsync(new Swipe()
            {
                Id = "",
                SwipedId = swipeRequest.SwipedId,
                SwiperId = swipeRequest.SwiperId,
                SwipeDirection = swipeRequest.SwipeDirection,
                SwipeTime = DateTime.Now,
            });
        }

        public async Task<Swipe[]> GetSwipesForIdAsync(string swipedId, SwipeDirection? swipeDirection)
        {
            return (await swipeRepository.GetSwipesBySwipedIdAsync(swipedId, swipeDirection)).ToArray();
        }

        public async Task<Swipe[]> GetSwipesBySwiperIdAsync(string swiperId, SwipeDirection? swipeDirection)
        {
            return (await swipeRepository.GetSwipesBySwiperIdAsync(swiperId, swipeDirection)).ToArray();
        }

        public async Task<Car[]?> GetCarsSwipedOnByAsync(string swiperId, SwipeDirection? swipeDirection)
        {
            if (!await carService.CarWithIdExistsAsync(swiperId))
            {
                throw new InvalidDataException($"Car (which is swiping) with id: {swiperId} does not exist.");
            }
            var swipes =  await GetSwipesBySwiperIdAsync(swiperId, swipeDirection);
            return await Task.WhenAll(swipes.Select(async x => await carService.GetCarByIdAsync(x.SwiperId)));
        }

        public async Task<Car[]?> GetCarsSwipedOnAsync(string swipedId, SwipeDirection? swipeDirection)
        {
            var swipes = await GetSwipesForIdAsync(swipedId, swipeDirection);
            var tasks = swipes.Select(x => carService.GetCarByIdAsync(x.SwipedId));
            return await Task.WhenAll(tasks);
        }


        public async Task<Swipe> UpdateSwipeAsync(Swipe updatedSwipe)
        {
            if (!await carService.CarWithIdExistsAsync(updatedSwipe.SwiperId))
            {
                throw new InvalidDataException($"Car (which is swiping) with id: {updatedSwipe.SwiperId} does not exist.");
            }
            if (!await carService.CarWithIdExistsAsync(updatedSwipe.SwipedId))
            {
                throw new InvalidDataException($"Car (which is getting swiped) with id: {updatedSwipe.SwipedId} does not exist.");
            }
            if (!await SwipeExists(updatedSwipe.Id))
            {
                return await AddSwipeAsync(new SwipeRequest() 
                { 
                    SwiperId = updatedSwipe.SwiperId, 
                    SwipedId = updatedSwipe.SwipedId, 
                    SwipeDirection = updatedSwipe.SwipeDirection 
                });
            }
            updatedSwipe.SwipeTime = DateTime.Now;
            return await swipeRepository.UpdateSwipeAsync(updatedSwipe);
        }

        public async Task DeleteSwipeAsync(string swipeId)
        {
            if (!(await SwipeWithIdExistsAsync(swipeId)))
            {
                throw new InvalidOperationException($"Swipe with id: {swipeId} does not exist.");
            }
            swipeRepository.RemoveAsync(swipeId).Wait();
        }

        private async Task<bool> SwipeWithIdExistsAsync(string swipeId)
        {
            var swipe = await swipeRepository.GetAsync(swipeId);
            return swipe != null;
        }

        private async Task<bool> SwipeExists(string swiperId, string swipedId)
        {
            return (await swipeRepository.GetBySwiperAndSwipedAsync(swiperId, swipedId)) != null;
        }

        private async Task<bool> SwipeExists(string id)
        {
            return (await swipeRepository.GetAsync(id)) != null;
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
