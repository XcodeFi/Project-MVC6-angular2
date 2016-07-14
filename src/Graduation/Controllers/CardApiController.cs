using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Graduation.Infrastructure.Repositories.Abstract;
using Graduation.Models;
using Graduation.Entities;
using System.Linq;
using AutoMapper;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Graduation.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class CardApiController : Controller
    {
        private readonly ILoggingRepository _logRepo;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private ICardRepository _cardRepo;
        private ICateRepository _cateRepo;
        int page = 1;
        int pageSize = 10;
        public CardApiController(
            ICateRepository cateRepo,
            ICardRepository cardRepo,
            UserManager<ApplicationUser> userManager,
            ILoggingRepository logRepo,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _cardRepo = cardRepo;
            _cateRepo = cateRepo;

            _logRepo = logRepo;
        }

        // GET: api/values
        [HttpGet]
        [AllowAnonymous]
        public  IActionResult Get()
        {
            int currentPage = page;
            int currentPageSize = pageSize;
            //var totalPages = (int)Math.Ceiling((double)totalSchedules / pageSize);

            IEnumerable<Card> _cards =_cardRepo
           .FindBy(c => c.IsDeleted == false && c.IsPublished == true)
           .OrderBy(u => u.Title)
           //.Skip((currentPage - 1) * currentPageSize)
           //.Take(currentPageSize)
           .ToList();

            //IEnumerable<CardViewModel> _cardVM=Mapper.Map<IEnumerable<Card>,IEnumerable<CardViewModel>>(_cards);
            IEnumerable<CardViewModel> _cardVM = Mapper.Map<IEnumerable<Card>, IEnumerable<CardViewModel>>(_cards);
            return new OkObjectResult(_cardVM);
        }

        [HttpGet("admin")]
        [Authorize(Policy = "Manager")]
        public IActionResult GetAdmin()
        {
            int currentPage = page;
            int currentPageSize = pageSize;
            //var totalPages = (int)Math.Ceiling((double)totalSchedules / pageSize);

            IEnumerable<Card> _cards = _cardRepo
           .FindBy(c => c.IsDeleted == false)
           .OrderBy(u => u.Title)
           //.Skip((currentPage - 1) * currentPageSize)
           //.Take(currentPageSize)
           .ToList();

            //IEnumerable<CardViewModel> _cardVM=Mapper.Map<IEnumerable<Card>,IEnumerable<CardViewModel>>(_cards);
            IEnumerable<CardViewModel> _cardVM = Mapper.Map<IEnumerable<Card>, IEnumerable<CardViewModel>>(_cards);
            return new OkObjectResult(_cardVM);
        }



        // GET api/values/5
        [HttpGet("{id}", Name = "GetCard")]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            Card _card = _cardRepo
            .GetSingle(c => c.Id == id && c.IsDeleted == false);

            if (_card != null)
            {
                CardViewModel _cardVM = Mapper.Map<Card, CardViewModel>(_card);
                return new OkObjectResult(_cardVM);
            }
            else
            {
                return NotFound();
            }
        }

        //POST api/values
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CardCreateEditViewModel card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser _user = await GetCurrentUserAsync();
            //get current User
            Card _card = new Card()
            {
                Title = card.Title,
                ApplycationUserId = _user.Id,
                Content = card.Content,
                IsPublished = card.IsPublished,
                TextSearch = card.TextSearch,
                ImageUrl = card.ImageUrl,
                CateId = card.CateId
            };
            _cardRepo.Add(_card);

            try
            {
                _cardRepo.Commit();
            }
            catch (DbUpdateException ex)
            {
                if (CardExists(_card.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
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
            //Card _cardResult=_cardRepo.f
            return CreatedAtAction("GetCard", new { id = _card.Id }, _card);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute]int id, [FromBody]CardCreateEditViewModel card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Card _card = _cardRepo.GetSingle(id);
            if (_card == null)
            {
                return NotFound();
            }
            else
            {
                _card.Title = card.Title;
                _card.ImageUrl = card.ImageUrl;
                _card.IsPublished = card.IsPublished;
                _card.CateId = card.CateId;
                _card.Content = card.Content;
                _card.DateEdited = DateTime.UtcNow;

                _cardRepo.Edit(_card);


                _cardRepo.Commit();
            }

            return CreatedAtRoute("GetCard", new {id=_card.Id }, _card);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Card _card = _cardRepo.FindBy(c => c.Id == id && c.IsDeleted == false).FirstOrDefault();

            if (_card == null)
            {
                return new NotFoundResult();
            }
            else
            {
                _card.IsDeleted = true;
                _cardRepo.Edit(_card);
                _cardRepo.Commit();
               
                return Ok(_card);
            }
        }


        #region Help
        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
        private bool CardExists(int id)
        {
            return _cardRepo.GetAll().Any(e => e.Id == id);
        }
        #endregion
    }
}
