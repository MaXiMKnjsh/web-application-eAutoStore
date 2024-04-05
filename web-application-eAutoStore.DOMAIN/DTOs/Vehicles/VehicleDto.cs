using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_application_eAutoStore.Enumerations;

namespace web_application_eAutoStore.DOMAIN.DTOs.Vehicles
{
    public class VehicleDto
    {
		public int Id { get; set; }
		public string Brand { get; set; }
		public string Model { get; set; }
		public int Mileage { get; set; }
		public int Price { get; set; }
		public CarType? Type { get; set; }
		public int OwnerId { get; set; }
		public CarQuality? Quality { get; set; }
		public CarTransmission? Transmission { get; set; }
		public int? Year { get; set; }
		public float? EngineCapacity { get; set; }
		public int? EnginePower { get; set; }
		public string? ImagePath { get; set; }
	}
}
