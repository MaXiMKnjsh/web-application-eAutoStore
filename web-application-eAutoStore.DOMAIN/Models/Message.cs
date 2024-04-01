namespace web_application_eAutoStore.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public DateTime MessageTime { get; set; }
        public int DialogId { get; set; }
        // navigation fields
        public Dialog Dialog { get; set; }
        public User Receiver { get; set; }
        public User Sender { get; set; }
    }
}
