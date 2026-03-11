using Domain.Models;
using Domain.Services.UserModule;
using Domain.Services.UserModule.Interface;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace UserApplication.Extensions
{
	public static class ApplicationServiceExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
		{
			services.AddDbContext<UserContext>(options =>
			   options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUserService, UserService>();
			services.AddScoped<IUserRepository, UserRepository>();	
            return services;	
		}
	}
}
