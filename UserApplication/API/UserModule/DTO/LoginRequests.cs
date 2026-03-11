using System.ComponentModel.DataAnnotations;

namespace UserApplication.API.UserModule.DTO
{
    public class LoginRequests
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
