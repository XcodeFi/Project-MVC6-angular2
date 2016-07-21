using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Graduation.Infrastructure.Repositories.Abstract;
using Graduation.Entities;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Graduation.Controllers
{
    [Route("api/[controller]")]
    public class SlideapiController : BaseController
    {
        private readonly ISliderRepository _slideRepo;

        public SlideapiController(ISliderRepository slideRepo,ILoggingRepository logg):base(logg)
        {
            _slideRepo = slideRepo;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
           return new OkObjectResult(_slideRepo.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return new OkObjectResult(_slideRepo.GetSingle(id));
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
