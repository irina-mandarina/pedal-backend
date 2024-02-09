﻿using Pedal.Entities;
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
            if (SwipeExists(swiperId, swipedId))
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
            return swipeRepository.CreateSwipeAsync(new Swipe()
            {
                SwipedId = swipedId,
                SwiperId = swiperId,
                //SwipeDirection = swipeDirection,
                SwipeTime = DateTime.Now,
            }).Result;
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
            var updatedSwipe = swipeRepository.GetBySwiperAndSwipedAsync(swiperId, swipedId).Result;
            if (updatedSwipe == null)
            {
                return await AddSwipeAsync(swiperId, swipedId, swipeDirection);
            }
            updatedSwipe.SwipeTime = DateTime.Now;
            return swipeRepository.UpdateSwipeAsync(updatedSwipe).Result;
        }

        public void DeleteSwipe(string swipeId)
        {
            if (SwipeWithIdExists(swipeId))
            {
                throw new InvalidOperationException($"Swipe with id: {swipeId} does not exist.");
            }
            _ = swipeRepository.RemoveAsync(swipeId);
        }

        private bool SwipeWithIdExists(string swipeId)
        { 
            return swipeRepository.GetAsync(swipeId).Result != null;
        }

        private bool SwipeExists(string swiperId, string swipedId)
        {
            return swipeRepository.GetBySwiperAndSwipedAsync(swiperId, swipedId).Result != null;
        }
    }
}
