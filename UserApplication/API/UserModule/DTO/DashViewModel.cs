using Domain.Models;

namespace UserApplication.API.UserModule.DTO
{
    public class DashViewModel
    {
        public int TotalUsers {  get; set; }    
        public List<Users> LastFiveUsers { get; set; }
    }
}
