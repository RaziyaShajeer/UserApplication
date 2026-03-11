using AutoMapper;
using Domain.Models;
using Domain.Services.UserModule.DTO;
using Domain.Services.UserModule.Interface;
using Domain.Services.UserModule.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.UserModule
{
	public class UserService:IUserService
	{
		IUserRepository _userRepository;
		IMapper _mapper;
		public UserService(IUserRepository userRepository,IMapper mapper)
		{
			_userRepository = userRepository;
			_mapper = mapper;	
		}
		public async Task<bool> UserRegistration(UserDTO userDTO)
		{
           
                var user = _mapper.Map<Users>(userDTO);
                return await _userRepository.UserRegistration(user);
            
        }
       public async  Task<Users> UserLoginAsync(string Email, string Password)
		{
			
			var result=await _userRepository.UserLoginAsync(Email, Password);	
			return result;
		}
       public async Task<List<Users>> GetUsersAsync()
		{
			var users =await _userRepository.GetUsersAsync();
			//var userDtos=_mapper.Map<List<UserDTO>>(users);
			return users;
		}
       public  Task<int> CountUsersGetUsersCountAsync()
		{
			return _userRepository.GetUserCountAsync();
		}
		public async Task<List<Users>> lastFiveUsersAsync()
		{
			return await _userRepository.lastFiveUsersAsync();
		}
		public async Task<bool> DeleteUserAsync(Guid UserId)
		{
			return await _userRepository.DeleteUserAsync(UserId);
		}
		public async Task<Users> GetUserByIdAsync(Guid UserId)
		{
			return await _userRepository.GetUserByIdasync(UserId);
		}
	public	Task<Users> UpdateUserAsync(Users user)
		{
			var updatedUser = _userRepository.UpdateUserAsync(user);
			return updatedUser;
		}

	}
}
