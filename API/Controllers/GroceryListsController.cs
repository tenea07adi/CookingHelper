using API.Controllers.ActionFilters;
using API.Controllers.Generics;
using Core.Entities.Persisted;
using Core.Ports.Driving;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthActionFilterAttribute]
    public class GroceryListsController : GenericController<GroceryList>
    {
        private readonly IGroceryListService _groceryListService;
        public GroceryListsController(IGroceryListService groceryListService) : base(groceryListService)
        {
            _groceryListService = groceryListService;
        }

        [HttpGet("{listId}/Items")]
        public IActionResult GetItems(int listId)
        {
            var items = _groceryListService.GetItems(listId);

            return Ok(items);
        }

        [HttpGet("{listId}/SwitchToCompleted")]
        public IActionResult SwitchToComplete(int listId)
        {
            _groceryListService.SwitchCompleted(listId, true);

            return Ok();
        }

        [HttpGet("{listId}/SwitchToNotCompleted")]
        public IActionResult SwitchToNotComplete(int listId)
        {
            _groceryListService.SwitchCompleted(listId, false);

            return Ok();
        }
    }
}
