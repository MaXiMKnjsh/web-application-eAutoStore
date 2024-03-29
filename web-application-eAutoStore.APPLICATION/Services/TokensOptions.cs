using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_application_eAutoStore.APPLICATION.Services
{
    public class TokensOptions
    {
        public string SecretKey { get; set; }
        public int ExpiresJwtHours { get; set; }
        public int ExpiresRtHours { get; set; }
    }
}
