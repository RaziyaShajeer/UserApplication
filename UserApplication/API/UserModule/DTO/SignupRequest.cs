using System.ComponentModel.DataAnnotations;

namespace UserApplication.API.UserModule.DTO
{
	public class SignupRequest
	{
		[Required]
        public string UserName { get; set; }
		[Required]
		[EmailAddress(ErrorMessage = "Invalid email format")]
		public string EmailId { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		[Phone(ErrorMessage = "Invalid phone number format")]
		public string PhoneNumber { get; set; }
		[Required]

        public IFormFile ProfileImage { get; set; }
		[Required]
        public string Country { get; set; }
    }
}
