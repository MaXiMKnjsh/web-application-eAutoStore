namespace web_application_eAutoStore.Models
{
    public class Dialog
    {
        public int Id { get; set; }
        public int User1Id { get; set; }
        public int User2Id { get; set; }
        // navigation fields
        public ICollection<Message> Messages { get; set; }
    }
}
