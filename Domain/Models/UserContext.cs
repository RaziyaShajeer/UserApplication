using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class UserContext:DbContext
	{
		public DbSet<Users> Users { get; set; }

		public UserContext(DbContextOptions<UserContext> options)
		  : base(options)
		{
		}

	}
}
