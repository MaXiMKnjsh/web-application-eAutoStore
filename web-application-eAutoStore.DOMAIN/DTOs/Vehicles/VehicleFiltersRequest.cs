using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_application_eAutoStore.Enumerations;

namespace web_application_eAutoStore.DOMAIN.DTOs.Vehicles
{
    public class VehicleFiltersRequest
    {
		public int Portion { get; set; } = 1;
		public string? Brand { get; set; }
		public int? Mileage {  get; set; }
		public string? Model { get; set; }
		public CarType? Type { get; set; }
		public CarQuality? Quality { get; set; }
		public CarTransmission? Transmission { get; set; }
		
		public int? PriceFrom { get; set; }
		public int? PriceTo { get; set; }
		
		public int? YearFrom { get; set; }
		public int? YearTo {  get; set; }
		
		public float? EngineCapacityFrom { get; set; }
		public float? EngineCapacityTo { get; set; }

		public int? EnginePowerFrom { get; set; }
		public int? EnginePowerTo { get; set; }
	}
}
