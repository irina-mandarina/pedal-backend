namespace Pedal.Entities
{
    public class Preference
    {
        public int MinMileage {  get; set; }
        public int MaxMileage { get; set; }
        public int Radius { get; set; }
        public required string Brand { get; set; }
        public required string Model { get; set; }
    }
}
