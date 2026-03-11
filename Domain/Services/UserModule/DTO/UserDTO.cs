using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.UserModule.DTO
{
	public class UserDTO
	{

        public string UserName { get; set; }
       

        public string EmailId { get; set; }
	
		public string Password { get; set; }
	
		public string PhoneNumber { get; set; }
        public string ProfileImage { get; set; }
        public string Country { get; set; }


    }
}
