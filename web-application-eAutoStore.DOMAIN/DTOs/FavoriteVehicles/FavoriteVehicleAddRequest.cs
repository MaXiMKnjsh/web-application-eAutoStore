using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_application_eAutoStore.DOMAIN.DTOs.FavoriteVehicles
{
    public class FavoriteVehicleAddRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int VehicleId { get; set; }
    }
}
