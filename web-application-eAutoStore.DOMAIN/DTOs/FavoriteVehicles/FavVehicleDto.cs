﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_application_eAutoStore.DOMAIN.DTOs.FavoriteVehicles
{
    public class FavVehicleDto
    {
		public int Id { get; set; }
		public int UserId { get; set; }
		public int VehicleId { get; set; }
	}
}
