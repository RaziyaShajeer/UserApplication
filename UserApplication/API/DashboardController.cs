using Domain.Models;
using Domain.Services.UserModule.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UserApplication.API.UserModule.DTO;

namespace UserApplication.API
{
    public class DashboardController : Controller
    {
        IUserService _userService;
        IHttpContextAccessor _httpContextAccessor;
        public DashboardController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {

            _userService = userService;    
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ViewDashboard()
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("UserId") == null)
            {
                return RedirectToAction("Login", "Authentication");
                    
            }
            var usercount = await _userService.CountUsersGetUsersCountAsync();
            var lastFiveUsers = await _userService.lastFiveUsersAsync();
            var model = new DashViewModel
            {
                TotalUsers = usercount,
                LastFiveUsers = lastFiveUsers
            };

            return View(model);


        }
    }

}
