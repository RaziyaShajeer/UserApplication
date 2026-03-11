using Domain.Models;
using Domain.Services.UserModule.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.UserModule.Interface
{
	public interface IUserService
	{
		Task<bool> UserRegistration(UserDTO userDTO);
		Task<Users> UserLoginAsync(string Email, string Password);
		Task<List<Users>> GetUsersAsync();
		Task<int> CountUsersGetUsersCountAsync();
		Task<List<Users>> lastFiveUsersAsync();
		Task<Users> GetUserByIdAsync(Guid UserId);
		Task<bool> DeleteUserAsync(Guid UserId);
		Task<Users> UpdateUserAsync(Users user);	


	}
}
