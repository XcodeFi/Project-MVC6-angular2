using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Graduation.Infrastructure.Repositories.Abstract;
using Graduation.Infrastructure;
using Graduation.Entities;
using System.Linq.Expressions;
using System;
using Graduation.Infrastructure.Core;
using Graduation.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Graduation.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class CateApiController : Controller
    {
        private ICardRepository _cardRepo;
        private ICateRepository _cateRepo;
        private Expression<Func<Category, object>>[] includeProperties;
        private int page = 1;
        private int pageSize = 10;
        public CateApiController(ICardRepository cardRepo,ICateRepository cateRepo)
        {
            _cateRepo = cateRepo;
            _cardRepo = cardRepo;
            includeProperties = Expressions.LoadCateNavigations();
        }

        [AllowAnonymous]
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            //lay cate cha
            IEnumerable<Category> cateParent=_cateRepo
           .FindBy(c => c.IsDeleted == false && c.IsPublished == true&&c.IsMainMenu==true&&c.Level==0)
           .OrderBy(u => u.DateCreated)
           .ToList();

            IEnumerable<CateViewModel> _cateP = Mapper.Map<IEnumerable<Category>, IEnumerable<CateViewModel>>(cateParent);

            //lay cate con
            IEnumerable<Category> cateChild = _cateRepo
           .FindBy(c => c.IsDeleted == false && c.IsPublished == true && c.IsMainMenu == true && c.Level == 1)
           .OrderBy(u => u.DateCreated)
           .ToList();

            IEnumerable<CateViewModel> _cateC = Mapper.Map<IEnumerable<Category>, IEnumerable<CateViewModel>>(cateChild);
            //lay cate cha
            List<CateViewModel> CateView = new List<CateViewModel>();

            //them con vao cate cha
            foreach (var item in _cateP)
            {
                foreach (var _itemC in _cateC)
                {
                    if (item.Id==_itemC.ParentId)
                    {
                        item.CateChilds.Add(_itemC);
                    }
                }
            }
            //loc ra nhung cate co con
            List<CateViewModel> _cateRe = (from i in _cateP
                                                 where i.CateChilds.Count() > 0
                                                 select i
                                                 ).ToList();

            return new OkObjectResult(_cateRe);
        }

        /// <summary>
        /// Tra ve loai con
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        // GET: api/values
        [HttpGet("/api/cateapi/getChild")]
        public IActionResult GetChild()
        {
            //lay cate con
            IEnumerable<Category> cateChild = _cateRepo
           .FindBy(c => c.IsDeleted == false && c.IsPublished == true && c.IsMainMenu == true && c.Level == 1)
           .OrderBy(u => u.DateCreated)
           .ToList();

            IEnumerable<CateViewModel> _cateC = Mapper.Map<IEnumerable<Category>, IEnumerable<CateViewModel>>(cateChild);

            return new OkObjectResult(_cateC);
        }

        [AllowAnonymous]
        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Category _cate = _cateRepo
            .GetSingle(c => c.Id == id && c.IsDeleted == false && c.IsPublished == true&&c.IsMainMenu==true,includeProperties);

            if (_cate != null)
            {
                //CardViewModel _cardVM = Mapper.Map<Card, CardViewModel>(_card);
                return new OkObjectResult(_cate);
            }
            else
            {
                return NotFound();
            }
        }
        [AllowAnonymous]
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Category value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            //Category _newCate = Mapper.Map<CardViewModel, Card>(cardVm);

            Category _newCate = new Category {
                Description = value.Description,
                ImageUrl = value.ImageUrl,
                UrlSlug=value.UrlSlug,
                Level=value.Level
            };
            _cateRepo.Add(_newCate);
            _cateRepo.Commit();

            //CreatedAtRouteResult result = CreatedAtRoute("GetSchedule", new { controller = "", id = cardVm.Id }, cardVm);
            return new NoContentResult();
        }
        [AllowAnonymous]
        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Category value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Category _cate = _cateRepo.GetSingle(id);

            if (_cate == null)
            {
                return NotFound();
            }
            else
            {
                _cate.Name = value.Name;
                _cate.Status = value.Status;
                _cate.Description = value.Description;

                _cate.ImageUrl = value.ImageUrl;
                _cate.Status = value.Status;
                _cate.IsMainMenu = value.IsMainMenu;
                _cate.UrlSlug = value.UrlSlug;

                _cateRepo.Edit(_cate);
                _cateRepo.Commit();
            }

            return new NoContentResult();
        }
        [AllowAnonymous]
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Category _cate = _cateRepo.FindBy(c => c.Id == id && c.IsDeleted == false && c.IsPublished == true).FirstOrDefault();
        
            if (_cate == null)
            {
                return new NotFoundResult();
            }
            else
            {
                _cate.IsDeleted = false;
                _cateRepo.Edit(_cate);
                _cateRepo.Commit();

                return new NoContentResult();
            }
        }
    }
}
