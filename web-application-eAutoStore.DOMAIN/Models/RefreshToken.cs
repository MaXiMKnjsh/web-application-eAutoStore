using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.DOMAIN.Models
{
    public class RefreshToken
    {
        public Guid Guid { get; set; }
        public int UserId { get; set; }
        public string AssociatedDeviceName { get; set; }
        public DateTime GeneratedAt { get; set; }
        public DateTime ExpiringAt {  get; set; }
        // navigation fields
        public User User { get; set; }
    }
}
