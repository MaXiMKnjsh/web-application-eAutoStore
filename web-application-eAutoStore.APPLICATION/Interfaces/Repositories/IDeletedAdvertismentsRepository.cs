using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_application_eAutoStore.Enumerations;
using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.APPLICATION.Interfaces.Repositories
{
    public interface IDeletedAdvertismentsRepository
    {
        Task<bool> AddReasonOfRemovingAsync(DeletedAdvertisement deletedAdvertisement);
    }
}
