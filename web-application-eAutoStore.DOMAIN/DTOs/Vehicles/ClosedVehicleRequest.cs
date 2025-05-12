using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_application_eAutoStore.DOMAIN.Enumerations;
using web_application_eAutoStore.Enumerations;

namespace web_application_eAutoStore.DOMAIN.DTOs.Vehicles
{
    public class ClosedVehicleRequest
    {
        [Required]
        public string WayOfAttraction { get; set; }
        [Required]
        public string WayDescription { get; set; }
        [Required]
        public string WayOfSelling { get; set; }
        [Required]
        public int Quality { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int Type { get; set; }
    }
}
