﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.APPLICATION.Interfaces.Auth
{
    public interface ITokensService
    {
        Task<Guid> GenerateRefreshTokenAsync(User user, string ipAddres);
        Task<string> GenerateJWTokenAsync(User user);
    }
    
}