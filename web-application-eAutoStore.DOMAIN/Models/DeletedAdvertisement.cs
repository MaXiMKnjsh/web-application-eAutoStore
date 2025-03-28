using web_application_eAutoStore.Enumerations;

namespace web_application_eAutoStore.Models
{
    public class DeletedAdvertisement
    {
        public int Id { get; set; }
        public DateTime DateOf { get; set; }
        public ReasonOfRemoving ReasonOfRemoving { get;set;}
        public string? RemovingDescrtiption { get; set; }
    }
}
