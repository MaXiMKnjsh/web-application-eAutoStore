﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_application_eAutoStore.DOMAIN.Models;
using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.APPLICATION.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<Guid> AddAsync(RefreshToken refreshToken);
        Task<bool> SaveAsync();
        Task<RefreshToken?> GetTokenModelAsync(string refreshToken);

	}
}
