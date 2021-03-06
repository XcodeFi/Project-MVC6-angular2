﻿using System.Collections.Generic;
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
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Graduation.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class CateApiController : BaseController
    {
        private ICardRepository _cardRepo;
        private ICateRepository _cateRepo;
        private Expression<Func<Category, object>>[] includeProperties;
        private int page = 1;
        private int pageSize = 10;
        public CateApiController(ICardRepository cardRepo, ICateRepository cateRepo, ILoggingRepository log) : base(log)
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
            IEnumerable<Category> cateParent = _cateRepo
           .FindBy(c => c.IsDeleted == false && c.IsPublished == true && c.IsMainMenu == true && c.Level == 0)
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
                    if (item.Id == _itemC.ParentId)
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
        [HttpGet("getChild")]
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
        [HttpGet("{id}", Name = "GetCate")]
        public IActionResult Get(int id)
        {
            Category _cate = _cateRepo
            .GetSingle(c => c.Id == id && c.IsDeleted == false && c.IsPublished == true && c.IsMainMenu == true);

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
        [HttpGet("geturl/{url}")]
        public IActionResult GetUrl(string url)
        {
            Category _cate = _cateRepo
            .GetSingle(c => c.UrlSlug == url && c.IsDeleted == false && c.IsPublished == true && c.IsMainMenu == true, includeProperties);

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
        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]CateCreateViewModel cateVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Category _cate = _cateRepo.GetSingle(id);

            if (cateVM.Level == 0)
            {
                cateVM.ParentId = null;
            }

            if (_cate == null)
            {
                return NotFound();
            }
            else
            {
                _cate.Name = cateVM.Name;
                _cate.Icon = cateVM.Icon;
                _cate.Description = cateVM.Description;
                _cate.IsPublished = cateVM.IsPublished;
                _cate.ImageUrl = cateVM.ImageUrl;
                _cate.Level = cateVM.Level;
                _cate.IsMainMenu = cateVM.IsMainMenu;
                _cate.UrlSlug = Common.ConvertToUrlString(cateVM.Name);
                _cateRepo.Edit(_cate);

                if (IsCateExists(_cate.UrlSlug, _cate.Id))
                {
                    return new NotFoundObjectResult("Can not change this name like that./n Because the name was exists!");
                }
                try
                {
                    _cateRepo.Commit();
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
            return CreatedAtAction("GetCate", new { id = _cate.Id }, _cate);
        }

        #region Admin
        [Authorize(Policy = "Manager")]
        // GET: api/values
        [HttpGet("cateChart")]
        public IActionResult CateChart()
        {
            //lay cate con
            IEnumerable<Category> cateChild = _cateRepo
           .FindBy(c => c.IsDeleted == false && c.IsPublished == true && c.IsMainMenu == true && c.Level == 1)
           .OrderBy(u => u.DateCreated)
           .Take(10)
           .ToList();

            IEnumerable<CateViewModel> _cateC = Mapper.Map<IEnumerable<Category>, IEnumerable<CateViewModel>>(cateChild);

            return new OkObjectResult(_cateC);
        }

        [Authorize(Policy = "Manager")]
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
                if (!IsHasCard(_cate.Id))
                {
                    return new NotFoundObjectResult("Can not delete this cate, because it has card!");
                }
                _cate.IsDeleted = true;
                _cateRepo.Edit(_cate);

                try
                {
                    _cateRepo.Commit();
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
                return new NoContentResult();
            }
        }
        [Authorize(Policy = "Manager")]
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]CateCreateViewModel cateVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Category _newCate = new Category();

            if (cateVM.Level == 0)
            {
                cateVM.ParentId = null;
            }
            //Category _newCate = Mapper.Map<CardViewModel, Card>(cardVm);

            if (cateVM.Icon.Equals(""))
            {
                _newCate.Name = cateVM.Name;
                _newCate.Level = cateVM.Level;
                _newCate.IsPublished = cateVM.IsPublished;
                _newCate.IsMainMenu = cateVM.IsMainMenu;
                _newCate.Description = cateVM.Description;
                _newCate.ParentId = cateVM.ParentId;
                _newCate.ImageUrl = cateVM.ImageUrl;
                _newCate.UrlSlug = Common.ConvertToUrlString(cateVM.Name);
            }
            else
            {
                _newCate.Icon = cateVM.Icon;
            }

            _cateRepo.Add(_newCate);
            try
            {
                if (IsCateExists(_newCate.UrlSlug, _newCate.Id))
                {
                    return new NotFoundObjectResult("Can not insert this cate, because the name has been exists!");
                }
                _cateRepo.Commit();
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
            //CreatedAtRouteResult result = CreatedAtRoute("GetSchedule", new { controller = "", id = cardVm.Id }, cardVm);
            return CreatedAtAction("GetCate", new { id = _newCate.Id }, _newCate);
        }

        [Authorize(Policy = "Manager")]
        // GET api/values/5
        [HttpGet("adminget/{id}", Name = "GetCateAdmin")]
        public IActionResult GetAmin(int id)
        {
            Category _cate = _cateRepo
            .GetSingle(c => c.Id == id, includeProperties);

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

        [Authorize(Policy = "Manager")]
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            //lay cate cha
            IEnumerable<Category> cateParent = _cateRepo
           .FindBy(c => c.IsDeleted == false)
           .OrderBy(u => u.Level)
           .ToList();

            return new OkObjectResult(cateParent);
        }
        #endregion

        #region Help
        private bool IsCateExists(string urlSlug, int id)
        {
            try
            {
                return _cateRepo.GetAll().Any(e => e.UrlSlug.Equals(urlSlug) && e.Id != id && e.IsDeleted == false);
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
            return true;
            
        }
        private bool IsHasCard(int id)
        {
            return _cardRepo.FindBy(c => c.CateId == id).Any();
        }
        #endregion
    }
}
