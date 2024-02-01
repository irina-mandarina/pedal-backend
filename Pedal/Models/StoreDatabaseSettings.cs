namespace Pedal.Models
{
    public class StoreDatabaseSettings
    {    
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CarsCollectionName { get; set; } = null!;
        public string SwipesCollectionName { get; set; } = null!;
        public string MessagesCollectionName { get; set; } = null!;
    }
}

