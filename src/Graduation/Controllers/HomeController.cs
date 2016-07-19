using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Graduation.Infrastructure.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Graduation.Entities;
using Microsoft.AspNetCore.Identity;
using Graduation.Models;

namespace Graduation.Controllers
{
    public class HomeController : BaseController
    {

        public UserManager<ApplicationUser> _userManager { get; }
        public SignInManager<ApplicationUser> _signInManager { get; }
        private readonly IViewRepository _viewRepo;
        private readonly ILoggingRepository _logRepo;

        public HomeController(
            IViewRepository viewRepo,
            ILoggingRepository loggRepo,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _viewRepo = viewRepo;
            _logRepo = loggRepo;
            
        }

        public IActionResult Index()
        {
            UpdateView();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
           
            return View();
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }

        public IActionResult StatusCodePage()
        {
            return View("~/Views/Shared/StatusCodePage.cshtml");
        }

        public IActionResult AccessDenied()
        {
            return View("~/Views/Shared/AccessDenied.cshtml");
        }

        public void UpdateView()
        {
            View _view = _viewRepo.FindBy(c => c.Key == 1).FirstOrDefault();
            int _totalViews = _view.TotalViews;

            _view.TotalViews = _totalViews + 1;

            _viewRepo.Edit(_view);
            try
            {
                _viewRepo.Commit();
            }
            catch (Exception ex)
            {

                _logRepo.Add(new Error
                {
                    DateCreated = DateTime.UtcNow,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                });
                _logRepo.Commit();
            }
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
