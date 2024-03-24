namespace web_application_eAutoStore.Models
{
    public class Dialog
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        // navigation fields
        public ICollection<Message> Messages { get; set; }
    }
}
