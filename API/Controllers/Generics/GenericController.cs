﻿using API.Controllers.ActionFilters;
using API.Models.BaseModels;
using API.Models.DTOs;
using API.Repository.Generics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers.Generics
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthActionFilterAttribute]
    public abstract class GenericController<T> : ControllerBase
        where T : BaseDBM
    {
        private readonly IGenericRepo<T> _genericRepo;
        private string? _defaultOrderField;

        protected Action<T> onAddAction = (t) => { };
        protected Action<T> onUpdateAction = (t) => { };
        protected Action<int> onDeleteAction = (t) => { };

        public GenericController(IGenericRepo<T> repo, string? defaultOrderField = null)
        {
            this._genericRepo = repo;
            this._defaultOrderField = defaultOrderField;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var entity = _genericRepo.Get(id);

            return Ok(entity);
        }

        [HttpGet]
        public IActionResult Get([FromQuery(Name = "offset")] int offset, [FromQuery(Name = "maxsize")] int maxsize, [FromQuery(Name = "orderBy")] string? orderBy)
        {
            if(offset < 0)
            {
                return BadRequest();
            }

            if (maxsize == 0)
            {
                maxsize = 20;
            }

            if(orderBy == null)
            {
                orderBy = _defaultOrderField;
            }

            var entities = _genericRepo.Get(offset, maxsize, GetOrderByExpresion<T>(orderBy), null);

            var result = new ListContainerDTO<T>()
            {
                NextOffset = entities.Count() < maxsize ? -1 : offset + maxsize,
                Count = entities.Count(),
                Records = entities
            };

            return Ok(result);
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var entities = _genericRepo.Get();

            return Ok(entities);
        }

        [HttpPost]
        [AuthActionFilterAttribute(Models.DBModels.Roles.Admin)]
        public IActionResult Add(T entity)
        {
            onAddAction(entity);

            _genericRepo.Add(entity);

            return Get(entity.Id);
        }

        [HttpPut("{id}")]
        [AuthActionFilterAttribute(Models.DBModels.Roles.Admin)]
        public IActionResult Update(int id, T entity)
        {
            entity.Id = id;

            onUpdateAction(entity);

            _genericRepo.Update(entity);

            return Get(entity.Id);
        }

        [HttpDelete("{id}")]
        [AuthActionFilterAttribute(Models.DBModels.Roles.Admin)]
        public IActionResult Delete(int id)
        {
            onDeleteAction(id);

            _genericRepo.Delete(id);

            return Ok();
        }

        [HttpDelete("count")]
        public IActionResult Count(int id)
        {
            return Ok(_genericRepo.Count());
        }

        protected Func<TOrderObj, object> GetOrderByExpresion<TOrderObj>(string fieldName)
        {
            Func<TOrderObj, object> returnExp = null;

            if (fieldName.IsNullOrEmpty())
            {
                return returnExp;
            }

            var propertyInfo = typeof(TOrderObj).GetProperty(fieldName);

            if(propertyInfo != null)
            {
                returnExp = (x => propertyInfo.GetValue(x, null));
            }

            return returnExp;
        }
    }
}
