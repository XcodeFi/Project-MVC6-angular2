using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Graduation.Controllers
{
    public class CardController : Controller
    {
        // GET: /<controller>/
        public IActionResult CardItem()
        {
            return PartialView();
        }
    }
}
