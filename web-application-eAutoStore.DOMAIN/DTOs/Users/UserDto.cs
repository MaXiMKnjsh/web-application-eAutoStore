using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_application_eAutoStore.Enumerations;

namespace web_application_eAutoStore.DOMAIN.DTOs.Users
{
    public class UserDto
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Surname { get; set; }
		public string HashedPassword { get; set; }
		public string Email { get; set; }
		public UserRole Role { get; set; }
	}
}
