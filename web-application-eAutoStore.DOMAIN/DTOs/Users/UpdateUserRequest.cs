using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_application_eAutoStore.Enumerations;

namespace web_application_eAutoStore.DOMAIN.DTOs.Users
{
    public class UpdateUserRequest
    {
		[MinLength(1)]
		[MaxLength(20)]
		public string Name { get; set; }
		[MinLength(1)]
		[MaxLength(20)]
		public string? Surname { get; set; }
		[MinLength(8)]
		[MaxLength(20)]
		public string Password { get; set; }
		[MinLength(8)]
		[MaxLength(20)]
		public string RepeatPassword { get; set; }
	}
}
