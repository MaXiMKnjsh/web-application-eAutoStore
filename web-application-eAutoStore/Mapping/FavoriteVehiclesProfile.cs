﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_application_eAutoStore.DOMAIN.DTOs.FavoriteVehicles;
using web_application_eAutoStore.DOMAIN.DTOs.Users;
using web_application_eAutoStore.DOMAIN.DTOs.Vehicles;
using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.APPLICATION.Mapping
{
    public class FavoriteVehiclesProfile : Profile
    {
        public FavoriteVehiclesProfile()
        {
            CreateMap<FavoriteVehicle,FavVehicleDto>();
        }
    }
}
