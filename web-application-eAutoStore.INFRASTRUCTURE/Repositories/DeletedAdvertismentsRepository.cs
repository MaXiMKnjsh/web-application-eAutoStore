using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_application_eAutoStore.APPLICATION.Interfaces.Repositories;
using web_application_eAutoStore.Data;
using web_application_eAutoStore.Enumerations;
using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.INFRASTRUCTURE.Repositories
{
    public class DeletedAdvertismentsRepository : IDeletedAdvertismentsRepository
    {
        private readonly DataContext _dataContext;
        public DeletedAdvertismentsRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<bool> SaveAsync()
        {
            int savedCount = await _dataContext.SaveChangesAsync();
            return savedCount > 0 ? true : false;
        }

        public async Task<bool> AddReasonOfRemovingAsync(DeletedAdvertisement deletedAdvertisement)
        {
            await _dataContext.DeletedAdvertisements.AddAsync(deletedAdvertisement);
            return await SaveAsync();
        }
    }
}
