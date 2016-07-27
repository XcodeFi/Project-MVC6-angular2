using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Graduation.Models;
using Graduation.Services;
using Microsoft.Extensions.Logging;
using Graduation.Infrastructure.Repositories.Abstract;
using Graduation.Entities;
using Microsoft.AspNetCore.Authorization;
using Graduation.Models.AccountViewModels;
using Graduation.Infrastructure;
using Graduation.Infrastructure.Core;
using Graduation.Models.ManageViewModels;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Graduation.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class AccountApiController : BaseController
    {
        #region Declare variable
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly ILoggingRepository _loggingRepository;
        private readonly GraduationDbContext _context;
        #endregion
        #region Constructor
        public AccountApiController(
                    UserManager<ApplicationUser> userManager,
                    SignInManager<ApplicationUser> signInManager,
                    IEmailSender emailSender,
                    ISmsSender smsSender,
                    ILoggerFactory loggerFactory,
                    ILoggingRepository _errorRepository,
                    GraduationDbContext context,
                    ILoggingRepository logging
                    ):base(logging)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _loggingRepository = _errorRepository;
            _context = context;
        }
        #endregion
        /// <summary>
        /// Get current user informattion
        /// </summary>
        /// <returns></returns>
        // GET: api/values
        [HttpGet("CurrentUserInfo", Name = "Info")]
        public async Task<IActionResult> GetCurrentUser()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            return new ObjectResult(user);
        }


        [Authorize(Policy = "Manager")]
        [HttpGet("GetAllUsers/{page:int=0}/{pageSize:int=5}")]
        //[AllowAnonymous]
        public IActionResult GetAllUsers(int? page=0, int? pageSize = 5)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            PaginationSet<ApplicationUser> pagedSet = new PaginationSet<ApplicationUser>();

            IEnumerable<ApplicationUser> users = _context.Users
                   .Skip(currentPage * currentPageSize)
                   .Take(currentPageSize).
                   ToList();

            int _totalUser = _context.Users.ToList().Count;

            pagedSet = new PaginationSet<ApplicationUser>()
            {
                Page = currentPage,
                TotalCount = _totalUser,
                TotalPages = (int)Math.Ceiling((decimal)_totalUser / currentPageSize),
                Items = users
            };

            return new ObjectResult(pagedSet);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        // POST api/values
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    //login success
                    _logger.LogInformation(1, "User logged in.");
                    //return new JsonResult(1);
                    return CreatedAtAction("Info", model);
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning(2, "User account locked out.");
                    return new JsonResult(2);
                }
                else
                {
                    //wrong user name or password
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return new JsonResult(3);
                }
            }
            // If we got this far, something failed, redisplay form
            return BadRequest(ModelState);
        }
        //logout
        [HttpPost("Logoff")]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            //return RedirectToAction(nameof(HomeController.Index), "Home");

            return new OkObjectResult(1);//logout success
        }

        /// <summary>
        /// kiem tra co dang nhap he thong khong
        /// </summary>
        /// <returns></returns>
        [HttpPost("IsSignIn")]
        [AllowAnonymous]
        public IActionResult IsSignIn()
        {
            bool result = _signInManager.IsSignedIn(User);
            _logger.LogInformation(4, "User logged out.");
            //return RedirectToAction(nameof(HomeController.Index), "Home");

            return new OkObjectResult(result);//logout success
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GenericResult _result = null;
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation(3, "User changed their password successfully.");
                    _result = new GenericResult()
                    {
                        Message= "User changed their password successfully.",
                        Succeeded=true
                    };

                    return new OkObjectResult(_result);
                }
                else
                {
                    _result = new GenericResult()
                    {
                        Message = "Old password not trully!",
                        Succeeded = false
                    };
                    return new OkObjectResult(_result);

                }
                //AddErrors(result);
            }
            _result = new GenericResult()
            {
                Message = "Something wrong!",
                Succeeded = false
            };
            return new OkObjectResult(_result);
        }

        /// <summary>
        /// update last logout
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>

        [HttpGet("UpdateLogoff")]
        public async Task<IActionResult> UpdateLogoff()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            user.LockoutEnd = DateTimeOffset.Now;
            await _userManager.UpdateAsync(user);
            return new OkObjectResult(user);
        }
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {

            object _result;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                    // Send an email with this link
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                    //    $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation(3, "User created a new account with password.");
                    //return RedirectToLocal(returnUrl);
                    return new OkObjectResult(model);
                }
                AddErrors(result);
                _result = result;
            }
            else
            {
                return BadRequest(ModelState);
            }
            // If we got this far, something failed, redisplay form
            return new JsonResult(_result);
        }
        #region Help
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
        #endregion
    }
}

