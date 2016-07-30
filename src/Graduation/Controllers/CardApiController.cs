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
using Graduation.Infrastructure.Core;
using System.Linq.Expressions;
using Graduation.Infrastructure;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Graduation.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class CardApiController : BaseController
    {
        #region Declare repo and varible
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly GraduationDbContext _context;
        private Expression<Func<Category, object>>[] includeProperties;
        private ICardRepository _cardRepo;
        private ICateRepository _cateRepo;

        #endregion
        public CardApiController(
            ICateRepository cateRepo,
            ICardRepository cardRepo,
            UserManager<ApplicationUser> userManager,
            ILoggingRepository logRepo,
            GraduationDbContext context,
            SignInManager<ApplicationUser> signInManager) : base(logRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            includeProperties = Expressions.LoadCardNavigations();
            _cardRepo = cardRepo;
            _cateRepo = cateRepo;
        }

        // GET: api/values
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            IEnumerable<Card> _cards = _cardRepo
           .FindBy(c => c.IsDeleted == false && c.IsPublished == true)
           .OrderByDescending(u => u.DateCreated)
           //.Skip((currentPage - 1) * currentPageSize)
           //.Take(currentPageSize)
           .ToList();
            IEnumerable<CardViewModel> _cardVM = Mapper.Map<IEnumerable<Card>, IEnumerable<CardViewModel>>(_cards);
            return new OkObjectResult(_cardVM);
        }

        // GET: api/cardapi/getnative
        [HttpGet("getNative")]
        [AllowAnonymous]
        public IActionResult GetNative()
        {
            IEnumerable<Card> _cards = _cardRepo
           .FindBy(c => c.IsDeleted == false && c.IsPublished == true)
           .OrderByDescending(u => u.DateCreated)
           //.Skip((currentPage - 1) * currentPageSize)
           //.Take(currentPageSize)
           .ToList();

            foreach (var item in _cards)
            {
                item.Category = _cateRepo.GetSingle(c => c.Id == item.CateId);
            }

            IEnumerable<CardViewModel> _cardVM = Mapper.Map<IEnumerable<Card>, IEnumerable<CardViewModel>>(_cards);
            return new OkObjectResult(_cardVM);
        }


        [HttpGet("getMycard/{id}")]
        public IActionResult GetMyCard(string id)
        {
            IEnumerable<Card> _cards = _cardRepo
           .FindBy(c => c.IsDeleted == false&&id==c.ApplycationUserId)
           .OrderByDescending(u => u.DateCreated)
           //.Skip((currentPage - 1) * currentPageSize)
           //.Take(currentPageSize)
           .ToList();

            foreach (var item in _cards)
            {
                item.Category = _cateRepo.GetSingle(c => c.Id == item.CateId);
            }

            IEnumerable<CardViewModel> _cardVM = Mapper.Map<IEnumerable<Card>, IEnumerable<CardViewModel>>(_cards);
            return new OkObjectResult(_cardVM);
        }


        [AllowAnonymous]
        [HttpGet("search/{key}")]
        public IActionResult Search(string key = "")
        {
            IEnumerable<Card> _cards = _cardRepo
           .FindBy(c => c.IsDeleted == false)
           .OrderBy(u => u.Title)
           .ToList();

            IEnumerable<Card> cardTmp = (from i in _cards
                                         where i.Content.Contains(key) || i.Title.Contains(key) || i.TextSearch.Contains(key)
                                         select i
                       ).ToList();

            //IEnumerable<CardViewModel> _cardVM=Mapper.Map<IEnumerable<Card>,IEnumerable<CardViewModel>>(_cards);
            IEnumerable<CardViewModel> _cardVM = Mapper.Map<IEnumerable<Card>, IEnumerable<CardViewModel>>(cardTmp);
            return new OkObjectResult(_cardVM);
        }

        // GET api/cardapi/4
        [HttpGet("{id}", Name = "GetCard")]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            Card _card = _cardRepo
            .GetSingle(c => c.Id == id && c.IsDeleted == false);

            if (_card != null)
            {
                CardViewModel _cardVM = Mapper.Map<Card, CardViewModel>(_card);
                UpdateView(id);
                return new OkObjectResult(_cardVM);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpGet("geturl/{url}", Name = "GetCardUrl")]
        [AllowAnonymous]
        public IActionResult GetUrl(string url)
        {
            Card _card = _cardRepo
            .GetSingle(c => c.UrlSlug == url && c.IsDeleted == false);

            if (_card != null)
            {
                CardViewModel _cardVM = Mapper.Map<Card, CardViewModel>(_card);
                UpdateView(_card.Id);
                return new OkObjectResult(_cardVM);
            }
            else
            {
                return NotFound();
            }
        }



        //POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CardCreateEditViewModel card)
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
                CateId = card.CateId,
                UrlSlug = Common.ConvertToUrlString(card.Title)
            };
            _cardRepo.Add(_card);

            try
            {
                if (IsCardNameExists(_card.UrlSlug,_card.Id))
                {
                    return new NotFoundObjectResult("This name has exists!");
                }
                _cardRepo.Commit();
            }
            catch (DbUpdateException ex)
            {
               
                    _logRepo.Add(new Error
                    {
                        DateCreated = DateTime.UtcNow,
                        Message = ex.Message,
                        StackTrace = ex.StackTrace
                    });
                    _logRepo.Commit();
                
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
                _card.UrlSlug = Common.ConvertToUrlString(card.Title);

                _cardRepo.Edit(_card);

                try
                {
                    if (IsCardNameExists(_card.UrlSlug,_card.Id))
                    {
                        return new NotFoundObjectResult("Name of card was exists!");
                    }
                    _cardRepo.Commit();
                }
                catch (DbUpdateException ex)
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

            return CreatedAtRoute("GetCard", new { id = _card.Id }, _card);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [Authorize(Policy ="Manager")]
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
        /// <summary>
        /// Delete method for all meber
        /// allow member can be deleet theme card
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpDelete("user/{id}/{userId}")]
        public IActionResult DeleteMember([FromRoute] int id,string userId)
        {
            GenericResult result = null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Card _card = _cardRepo.FindBy(c => c.Id == id && c.IsDeleted == false&&c.ApplycationUserId==userId).FirstOrDefault();
            if (_card == null)
            {
                return new NotFoundResult();
            }
            else
            {
                _card.IsDeleted = true;
                _cardRepo.Edit(_card);
                _cardRepo.Commit();
                result = new GenericResult()
                {
                    Message = "Thiệp đã xóa thành công",
                    Succeeded = true
                };
                return Ok(result);
            }
        }


        #region Admin
        [Authorize(Policy = "Manager")]
        [HttpGet("admin/{page:int=0}/{pageSize}")]
        public IActionResult GetAdmin(int? page, int? pageSize = 12)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            PaginationSet<CardViewModel> pagedSet = new PaginationSet<CardViewModel>();

            //var totalPages = (int)Math.Ceiling((double)totalSchedules / pageSize);

            IEnumerable<Card> _cards = _cardRepo
           .FindBy(c => c.IsDeleted == false)
           .OrderByDescending(u => u.DateCreated)
           .Skip(currentPage * currentPageSize)
           .Take(currentPageSize)
           .ToList();

            int _totalCard = _cardRepo.FindBy(c => c.IsDeleted == false).Count();

            //IEnumerable<CardViewModel> _cardVM=Mapper.Map<IEnumerable<Card>,IEnumerable<CardViewModel>>(_cards);
            IEnumerable<CardViewModel> _cardVM = Mapper.Map<IEnumerable<Card>, IEnumerable<CardViewModel>>(_cards);

            pagedSet = new PaginationSet<CardViewModel>()
            {
                Page = currentPage,
                TotalCount = _totalCard,
                TotalPages = (int)Math.Ceiling((decimal)_totalCard / currentPageSize),
                Items = _cardVM
            };
            return new OkObjectResult(pagedSet);
        }

        [Authorize(Policy = "Manager")]
        [HttpGet("chart")]
        public IActionResult CardChart()
        {
            IEnumerable<Card> _cards = _cardRepo
           .FindBy(c => c.IsDeleted == false)
           .OrderByDescending(u => u.ViewNo)
           .Take(10)
           .ToList();

            int _totalCard = _cardRepo.FindBy(c => c.IsDeleted == false).Count();

            //IEnumerable<CardViewModel> _cardVM=Mapper.Map<IEnumerable<Card>,IEnumerable<CardViewModel>>(_cards);
            IEnumerable<CardChartVM> _cardVM = Mapper.Map<IEnumerable<Card>, IEnumerable<CardChartVM>>(_cards);

            return new OkObjectResult(_cardVM);
        }



        #endregion

        #region Component
        /// <summary>
        /// Auto update view when user or anyone get card from page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult UpdateView([FromRoute]int id)
        {
            Card _card = _cardRepo.GetSingle(id);
            if (_card == null)
            {
                return NotFound();
            }
            else
            {
                int _currentViewNo = _card.ViewNo;
                _card.ViewNo = _currentViewNo + 1;
                _cardRepo.Edit(_card);
                try
                {
                    _cardRepo.Commit();
                }
                catch (DbUpdateException ex)
                {
                    if (!IsCardExist(_card.Id))
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
            }
            return CreatedAtRoute("GetCard", new { id = _card.Id }, _card);
        }
        #endregion
        #region Help
        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }


        /// <summary>
        /// phuong thuc ktra su ton tai cua ten
        /// su dung cho phuong thuc them, sua moi mot anh
        /// </summary>
        /// <param name="urlSlug"></param>
        /// <returns>true if exist</returns>
        private bool IsCardNameExists(string urlSlug, int id)
        {
            IEnumerable<Card> listTmp=  _cardRepo.FindBy(e => e.UrlSlug.Equals(urlSlug) && e.Id!=id&&e.IsDeleted==false);
            return listTmp.Count() == 1 ? true : false;
        }
        /// <summary>
        /// tra ve gia tri true neu id ton tai
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool IsCardExist(int id)
        {
            return _cardRepo.GetAll().Any(e =>e.Id == id);
        }

        #endregion
    }
}
