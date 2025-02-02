using API.Controllers.ActionFilters;
using API.DTOs;
using Core.Entities.Abstract;
using Core.Entities.Persisted;
using Core.Entities.Transfer;
using Core.Ports.Driving;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Generics
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthActionFilterAttribute]
    public abstract class GenericController<T> : ControllerBase
        where T : BaseEntity
    {
        private readonly IGenericEntityService<T> _genericEntityService;

        public GenericController(IGenericEntityService<T> genericEntityService)
        {
            _genericEntityService = genericEntityService;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var entity = _genericEntityService.Get(id);

            return Ok(entity);
        }

        [HttpGet]
        public IActionResult Get(
            [FromQuery(Name = "offset")] int offset, [FromQuery(Name = "maxsize")] int maxsize,
            [FromQuery(Name = "orderBy")] string? orderBy,
            [FromQuery(Name = "filterField")] string? filterField, [FromQuery(Name = "filterValue")] string? filterValue, 
            [FromQuery(Name = "filterType")] Core.Entities.Transfer.SearchEvaluationTypes? filterType
            )
        {
            var filter = new SearchParameters()
            {
                Field = filterField,
                Value = filterValue,
                EvaluationType = filterType
            };

            var paginationParameters = new PaginationParameters()
            {
                Offset = offset,
                Maxsize = maxsize,
                OrderBy = orderBy
            };

            var entities = _genericEntityService.Get(paginationParameters, filter);

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
            var entities = _genericEntityService.GetAll();

            return Ok(entities);
        }

        [HttpPost]
        [AuthActionFilterAttribute(Roles.Admin)]
        public IActionResult Add(T entity)
        {
            return Ok(_genericEntityService.Add(entity));
        }

        [HttpPut("{id}")]
        [AuthActionFilterAttribute(Roles.Admin)]
        public IActionResult Update(int id, T entity)
        {
            return Ok(_genericEntityService.Update(id, entity));
        }

        [HttpDelete("{id}")]
        [AuthActionFilterAttribute(Roles.Admin)]
        public IActionResult Delete(int id)
        {
            _genericEntityService.Delete(id);

            return Ok();
        }

        [HttpDelete("count")]
        public IActionResult Count()
        {
            var count = _genericEntityService.Count();

            return Ok(count);
        }
    }
}
