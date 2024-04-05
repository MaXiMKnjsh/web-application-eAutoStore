using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_application_eAutoStore.Enumerations;

namespace web_application_eAutoStore.DOMAIN.DTOs.Vehicles
{
    public class VehicleAddRequest
    {
		[Required]
		public string Brand { get; set; }
		[Required]
		public string Model { get; set; }
		[Required]
		public int Mileage { get; set; }
		[Required]
		public int Price { get; set; }
		public CarType? Type { get; set; }
		public CarQuality? Quality { get; set; }
		public CarTransmission? Transmission { get; set; }
		public int? Year { get; set; }
		public float? EngineCapacity { get; set; }
		public int? EnginePower { get; set; }
		public IFormFile? Image { get; set; }
	}
}
