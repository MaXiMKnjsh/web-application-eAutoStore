using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.Interfaces.Services
{
    public interface IImageService
    {
		Task<string?> SaveImageAsync(IFormFile image);
		Task<string> GetImagePathAsync(int id);
	}
}
