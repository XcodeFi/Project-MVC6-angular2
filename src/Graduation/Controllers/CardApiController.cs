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
    public class CardApiController : Controller
    {
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
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _cardRepo = cardRepo;
            _cateRepo = cateRepo;
        }

        // GET: api/values
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            int currentPage = page;
            int currentPageSize = pageSize;
            //var totalPages = (int)Math.Ceiling((double)totalSchedules / pageSize);

            IEnumerable<Card> _cards = _cardRepo
           .FindBy(c => c.IsDeleted == false && c.IsPublished == true)
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
            .GetSingle(c => c.Id == id && c.IsDeleted == false && c.IsPublished == true);

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
        public async Task<IActionResult> Create([FromBody]CardCreateViewModel card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string id = "b449468f-94fa-4e66-98e2-42d516059d01";

            //get current User
            ApplicationUser _applicationUser = await GetCurrentUserAsync();
            Card _card = new Card
            {
                Title = card.Title,
                ApplycationUserId = id,
                Content = card.Content,
                IsPublished = card.IsPublished,
                TextSearch = card.TextSearch,
                ImageUrl = card.ImageUrl
            };
            _cardRepo.Add(_card);
            try
            {
                _cardRepo.Commit();
            }
            catch (DbUpdateException)
            {
                if (CardExists(_card.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            //Card _cardResult=_cardRepo.f
            return CreatedAtAction("GetCard", new { id = _card.Id }, _card);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Card card)
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
                _card.UrlSlug = card.UrlSlug;
                _card.IsPublished = card.IsPublished;
                _card.CateId = card.CateId;
                _card.Content = card.Content;

                _cardRepo.Edit(_card);
                _cardRepo.Commit();
            }

            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public IActionResult Delete(int id)
        {
            Card _card = _cardRepo.FindBy(c => c.Id == id && c.IsDeleted == false && c.IsPublished == true).FirstOrDefault();

            if (_card == null)
            {
                return new NotFoundResult();
            }
            else
            {
                _card.IsDeleted = true;
                _cardRepo.Edit(_card);
                _cardRepo.Commit();

                return new NoContentResult();
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
