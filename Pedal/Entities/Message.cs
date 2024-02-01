namespace Pedal.Entities
{
    public class Message
    {
        public string Id { get; set; }
        public required string Text { get; set; }
        public DateTime Timestamp { get; set; }
        public int SenderID { get; set; }
        public int ReceiverID { get; set; }
    }
}
