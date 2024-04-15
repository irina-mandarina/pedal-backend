using Pedal.Entities.Enums;
using Pedal.Entities;

namespace Pedal.Models
{
    public class CarLoginRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
