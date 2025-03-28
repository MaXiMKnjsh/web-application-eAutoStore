using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_application_eAutoStore.Models;
namespace web_application_eAutoStore.DOMAIN.Models
{
    public class StatOfNewAds
    {
        public int Id { get; set; }

        public DateTime DateOf { get; set; }

        public int UserId { get; set; }
        //
        public User User { get; set; }
    }
}
