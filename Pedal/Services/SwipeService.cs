using Pedal.Repositories;

namespace Pedal.Services
{
    public class SwipeService
    {
        public SwipeRepository swipeRepository;

        public SwipeService(SwipeRepository swipeRepository)
        {
            this.swipeRepository = swipeRepository;
        }
    }
}
