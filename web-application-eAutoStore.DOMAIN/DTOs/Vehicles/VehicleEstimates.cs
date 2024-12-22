using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_application_eAutoStore.Enumerations;

namespace web_application_eAutoStore.DOMAIN.DTOs.Vehicles
{
	public class VehicleEstimates
	{
		public int AveragePrice { get; set; }
		public int MaximumPrice { get; set; }
		public int MinimumPrice { get; set; }
	}
}
