using Domain.Services.UserModule.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
namespace Domain.Services.UserModule.Interface
{
	public interface IUserRepository
	{
		Task<bool> UserRegistration(Users user);
		Task<Users> UserLoginAsync(string Email,string Password);
		Task<List<Users>> GetUsersAsync();
		Task<int> GetUserCountAsync();
		Task<List<Users>> lastFiveUsersAsync();
		Task<bool> DeleteUserAsync(Guid UserId);
		Task<Users> GetUserByIdasync(Guid UserId);
		Task<Users> UpdateUserAsync(Users user);


	}
}
