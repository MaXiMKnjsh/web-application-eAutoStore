using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_application_eAutoStore.Enumerations;

namespace web_application_eAutoStore.DOMAIN.DTOs.Vehicles
{
    public class DeleteVehicleRequest
    {
        [Required]
        public int VehicleId { get; set; }        // ID транспортного средства
        public string Reason { get; set; }       // Причина удаления (строка)
        [Required]
        public string ReasonEnum { get; set; } // Причина удаления (перечисление)
        /*[Required]
        public int VehicleId { get; set; }        // ID транспортного средства
        public string? Reason { get; set; }       // Причина удаления (строка)
        [Required]
        public ReasonOfRemoving ReasonEnum { get; set; } // Причина удаления (перечисление)*/
    }
}
