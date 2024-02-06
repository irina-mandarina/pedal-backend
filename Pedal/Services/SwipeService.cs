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

        public Swipe AddSwipe(string swiperId, string swipedId, bool swipeDirection)
        {
            if (SwipeExists(swiperId, swipedId))
            {
                return UpdateSwipe(swiperId, swipedId, swipeDirection);
            }
            if (!carService.CarWithIdExists(swiperId))
            {
                throw new InvalidDataException($"Car (which is swiping) with id: {swiperId} does not exist.");
            }
            if (!carService.CarWithIdExists(swipedId))
            {
                throw new InvalidDataException($"Car (which is getting swiped) with id: {swipedId} does not exist.");
            }
            return swipeRepository.CreateSwipeAsync(new Swipe()
            {
                SwipedId = swipedId,
                SwiperId = swiperId,
                SwipeDirection = swipeDirection,
                SwipeTime = DateTime.Now,
            }).Result;
        }

        public Swipe[] GetSwipesForId(string swipedId)
        {
            return swipeRepository.GetSwipesBySwipedIdAsync(swipedId).Result.ToArray();
        }

        public Swipe[] GetSwipesById(string swiperId)
        {
            return swipeRepository.GetSwipesBySwiperIdAsync(swiperId).Result.ToArray();
        }

        public Car[] GetCarsSwipedOnBy(string swiperId)
        {
            throw new Exception();
        }

        public Car[] GetCarsSwipedOn(string swipedId)
        {
            return GetSwipesForId(swipedId).Select(x => carService.GetCarById(x.SwipedId)).ToArray();
        }

        public Swipe UpdateSwipe(string swiperId, string swipedId, bool swipeDirection)
        {
            if (!carService.CarWithIdExists(swiperId))
            {
                throw new InvalidDataException($"Car (which is swiping) with id: {swiperId} does not exist.");
            }
            if (!carService.CarWithIdExists(swipedId))
            {
                throw new InvalidDataException($"Car (which is getting swiped) with id: {swipedId} does not exist.");
            }
            var updatedSwipe = swipeRepository.GetBySwiperAndSwipedAsync(swiperId, swipedId).Result;
            if (updatedSwipe == null)
            {
                return AddSwipe(swiperId, swipedId, swipeDirection);
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
