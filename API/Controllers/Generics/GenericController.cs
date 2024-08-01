using API.Models.BaseModels;
using API.Models.DTOs;
using API.Repository.Generics;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Generics
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class GenericController<T> : ControllerBase
        where T : BaseDBM
    {

        private readonly IGenericRepo<T> _genericRepo;

        protected Action<T> onAddAction = (t) => { };
        protected Action<T> onUpdateAction = (t) => { };
        protected Action<int> onDeleteAction = (t) => { };

        public GenericController(IGenericRepo<T> repo)
        {
            this._genericRepo = repo;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var entity = _genericRepo.Get(id);

            return Ok(entity);
        }

        [HttpGet]
        public IActionResult Get([FromQuery(Name = "offset")] int offset, [FromQuery(Name = "maxsize")] int maxsize)
        {
            if(offset < 0)
            {
                return BadRequest();
            }

            if (maxsize == 0)
            {
                maxsize = 20;
            }

            var entities = _genericRepo.Get(offset, maxsize);

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
        public IActionResult Add(T entity)
        {
            onAddAction(entity);

            _genericRepo.Add(entity);

            return Get(entity.Id);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, T entity)
        {
            entity.Id = id;

            onUpdateAction(entity);

            _genericRepo.Update(entity);

            return Get(entity.Id);
        }

        [HttpDelete("{id}")]
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
    }
}
