using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Graduation.Infrastructure.Repositories.Abstract;
using Graduation.Entities;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Graduation.Controllers
{
    public class BaseController : Controller
    {
        protected ILoggingRepository _logRepo;

        public BaseController(ILoggingRepository logRepo)
        {
            _logRepo = logRepo;
        }
    }
}
