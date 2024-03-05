namespace Pedal.Models
{
    public class MessageRequest
    {
        public required string Text { get; set; }
        public DateTime Timestamp { get; set; }
        public int SenderID { get; set; }
        public int ReceiverID { get; set; }
    }
}
