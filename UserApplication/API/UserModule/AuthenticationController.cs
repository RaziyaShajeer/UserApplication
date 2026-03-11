using AutoMapper;
using Domain.Exceptions;
using Domain.Services.UserModule.DTO;
using Domain.Services.UserModule.Interface;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using UserApplication.API.UserModule.DTO;
using static System.Net.Mime.MediaTypeNames;

namespace UserApplication.API.UserModule
{
    public class AuthenticationController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;
        private readonly IUserService _userinterface;
        IHttpContextAccessor _httpContextAccessor;
        private readonly IHubContext<DashboardHub> _hubContext;
        public AuthenticationController(IMapper mapper, IUserService userInterface, IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor, IHubContext<DashboardHub> hubContext)
        {
            _mapper = mapper;
            _userinterface = userInterface;
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;
            _hubContext = hubContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(SignupRequest signupRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string fileName = "";

                    if (signupRequest.ProfileImage != null && signupRequest.ProfileImage.Length > 0)
                    {
                        var uploadFolder = Path.Combine(_environment.WebRootPath, "images");

                        if (!Directory.Exists(uploadFolder))
                        {
                            Directory.CreateDirectory(uploadFolder);
                        }

                        fileName = Guid.NewGuid().ToString() + Path.GetExtension(signupRequest.ProfileImage.FileName);

                        var filePath = Path.Combine(uploadFolder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await signupRequest.ProfileImage.CopyToAsync(stream);
                        }
                    }

                    var userDto = _mapper.Map<UserDTO>(signupRequest);
                    userDto.ProfileImage = fileName;

                    var user = await _userinterface.UserRegistration(userDto);
                    // Notify all connected clients
                    await _hubContext.Clients.All.SendAsync("ReceiveUserUpdate");

                    if (_httpContextAccessor.HttpContext.Session.GetString("UserId") != null)
                    {
                        return RedirectToAction("ViewDashboard","Dashboard");
                    }
                    else
                    {
                        return RedirectToAction("Login");
                    }
                    
                }
                else
                {

                    return View();
                }
            }
            catch (UserAlreadyExistException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(signupRequest);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequests loginRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userinterface.UserLoginAsync(loginRequest.Email, loginRequest.Password);

                    if (user != null)
                    {
                        _httpContextAccessor.HttpContext.Session.SetString("UserName", user.UserName);
                        _httpContextAccessor.HttpContext.Session.SetString("UserId", user.UserId.ToString());
                        // Login success
                        return RedirectToAction("ViewDashboard", "Dashboard");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Email or Password");
                    }
                }

                return View(loginRequest);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while processing your request.");
                return View(loginRequest);
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();   // remove all session values
            return RedirectToAction("Login", "Authentication");
        }
    }
}
