using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using web_application_eAutoStore.APPLICATION.Interfaces.Repositories;
using web_application_eAutoStore.Data;
using web_application_eAutoStore.DOMAIN.Models;
using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.INFRASTRUCTURE.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly DataContext _dataContext;
        public RefreshTokenRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Guid> AddAsync(RefreshToken refreshToken)
        {
            await _dataContext.AddAsync(refreshToken);
            var result = await SaveAsync();

            if (!result)
                return Guid.Empty;

            return refreshToken.Guid;
        }

        public async Task<bool> SaveAsync()
        {
            int savedCount = await _dataContext.SaveChangesAsync();
            return savedCount > 0 ? true : false;
        }
    }
}
