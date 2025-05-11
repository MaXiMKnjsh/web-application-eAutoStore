using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_application_eAutoStore.APPLICATION.Interfaces.Repositories;
using web_application_eAutoStore.APPLICATION.Interfaces.Services;
using web_application_eAutoStore.Enumerations;
using web_application_eAutoStore.Interfaces.Repositories;
using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.APPLICATION.Services
{
    public class DeletedAdvertisementsService : IDeletedAdvertisementsService
    {
        private readonly IDeletedAdvertismentsRepository _deletedAdvertismentsRepository;
        public DeletedAdvertisementsService(IDeletedAdvertismentsRepository deletedAdvertismentsRepository)
        {
            _deletedAdvertismentsRepository = deletedAdvertismentsRepository;
        }
        public async Task<bool> AddReasonOfRemovingAsync(ReasonOfRemoving reasonOfRemoving, string reason)
        {
            var deletedAdvertisement = new DeletedAdvertisement();
            deletedAdvertisement.ReasonOfRemoving = reasonOfRemoving;
            deletedAdvertisement.RemovingDescrtiption = reason;
            deletedAdvertisement.DateOf = DateTime.Now;

            var result= await _deletedAdvertismentsRepository.AddReasonOfRemovingAsync(deletedAdvertisement);
            return result;
        }
    }
}
