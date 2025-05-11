using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_application_eAutoStore.Enumerations;

namespace web_application_eAutoStore.APPLICATION.Interfaces.Services
{
    public interface IDeletedAdvertisementsService
    {
        Task<bool> AddReasonOfRemovingAsync(ReasonOfRemoving reasonOfRemoving, string reason);
    }
}
