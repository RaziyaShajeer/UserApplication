using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class Users
	{
		[Key]
		public Guid UserId { get; set; }
		[Required]
		public string UserName { get; set; }
		[EmailAddress]
		public string EmailId { get; set; }
		[Required]
		public string Password { get; set; }
		[Phone]
		public string PhoneNumber { get;set; }
		[Required]
        public string ProfileImage { get; set; }
        [Required]
        public string Country{ get; set; }
        public DateTime CreatedAt { get; set; }=DateTime.Now;

	}
}
