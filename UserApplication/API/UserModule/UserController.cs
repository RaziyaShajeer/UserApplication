using Domain.Models;
using Domain.Services.UserModule.Interface;
using Microsoft.AspNetCore.Mvc;
using UserApplication.API.UserModule.DTO;

namespace UserApplication.API.UserModule
{
    public class userController : Controller
    {
        private readonly IUserService _userinterface;
		private readonly IWebHostEnvironment _environment;
		IHttpContextAccessor _httpContextAccessor;
        public userController(IUserService userinterface, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment)
        {
            _userinterface = userinterface;
            _httpContextAccessor = httpContextAccessor;
			_environment = environment;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task< IActionResult> ViewAllUsers()
        {
            
            
                try
                {
                if (_httpContextAccessor.HttpContext.Session.GetString("UserId") == null)
                {
                    return RedirectToAction("Login", "Authentication");

                }
                var usersList = await _userinterface.GetUsersAsync();

                    
                    if (usersList == null || !usersList.Any())
                    {
                       
                        ViewBag.Message = "No users found.";
                        return View(new List<Users>()); 
                    }

                    return View(usersList);
                }
                catch (Exception ex)
                {
                  
                    ViewBag.ErrorMessage = "An error occurred while retrieving users. Please try again later.";
                    return View(new List<Users>());
                }
         }


		[HttpGet]
		public async Task<IActionResult> Delete(Guid id)
        {
			try
			{
				var status = await _userinterface.DeleteUserAsync(id);

				if (status)
				{
					TempData["Message"] = "Deleted successfully";
				}
				else
				{
					TempData["Error"] = "User not found";
				}

				return RedirectToAction("ViewAllUsers");
			}
			catch (Exception ex)
			{
				TempData["Error"] = "Error while deleting user: " + ex.Message;
				return RedirectToAction("ViewAllUsers");
			}
		}
		[HttpGet]
		public async Task<IActionResult> EditUser(Guid id)
		{
			try
			{
				var user = await _userinterface.GetUserByIdAsync(id);

				if (user == null)
				{
					TempData["Error"] = "User not found.";
					return RedirectToAction("ViewAllUsers");
				}

				
				return View(user);

			}
			catch (Exception ex)
			{
				
				TempData["Error"] = "An error occurred while trying to fetch the user: " + ex.Message;
				return RedirectToAction("ViewAllUsers");
			}
		}
		[HttpPost]
		public async Task<IActionResult> EditUser(Users user,IFormFile ProfileImage)
		{
			try
			{

				string fileName = "";

				if (ProfileImage != null && ProfileImage.Length > 0)
				{
					var uploadFolder = Path.Combine(_environment.WebRootPath, "images");

					if (!Directory.Exists(uploadFolder))
					{
						Directory.CreateDirectory(uploadFolder);
					}

					fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfileImage.FileName);

					var filePath = Path.Combine(uploadFolder, fileName);

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await ProfileImage.CopyToAsync(stream);
					}
				}
				user.ProfileImage = fileName;
				var updatedUser = await _userinterface.UpdateUserAsync(user);
				if (updatedUser != null)
				{
					return RedirectToAction("ViewAllUsers");
				}
				else
				{
					TempData["Error"] = "User not found.";
					return RedirectToAction("ViewAllUsers");
				}
			}
			catch (Exception ex)
			{
				TempData["Error"] = "Error while updating user: " + ex.Message;
				return RedirectToAction("ViewAllUsers");
			}
		}

	}
}
