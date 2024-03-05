namespace Pedal.Models
{
    public class MessageRequest
    {
        public required string Text { get; set; }
        public DateTime Timestamp { get; set; }
        required public string SenderID { get; set; }
        required public string ReceiverID { get; set; }
    }
}
