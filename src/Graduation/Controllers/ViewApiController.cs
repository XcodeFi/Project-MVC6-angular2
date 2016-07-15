using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Graduation.Infrastructure.Repositories.Abstract;
using Graduation.Entities;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Graduation.Controllers
{
    [Authorize(Policy = "Manager")]
    [Route("api/[controller]")]
    public class ViewApiController : Controller
    {
        private readonly IViewRepository _viewRepo;
        private readonly ILoggingRepository _loggRepo;
         
        public ViewApiController (
            IViewRepository viewRepo,
            ILoggingRepository loggRepo
            )
        {
            _viewRepo = viewRepo;
            _loggRepo = loggRepo;
        }

        // GET: api/values
        [HttpGet]
        public  IActionResult Get()
        {
            View _view = _viewRepo.FindBy(c => c.Key == 1).FirstOrDefault();
            
            return new OkObjectResult(_view);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
