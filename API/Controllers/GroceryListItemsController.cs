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
    public class GroceryListItemsController : GenericController<GroceryListItem>
    {
        private readonly IGroceryListItemService _groceryListItemService;

        public GroceryListItemsController(IGroceryListItemService groceryListItemService) : base(groceryListItemService)
        {
            _groceryListItemService = groceryListItemService;
        }

        [HttpGet("{itemId}/SwitchToCompleted")]
        public IActionResult SwitchToComplete(int itemId)
        {
            _groceryListItemService.SwitchCompleted(itemId, true);

            return Ok();
        }

        [HttpGet("{itemId}/SwitchToNotCompleted")]
        public IActionResult SwitchToNotComplete(int itemId)
        {
            _groceryListItemService.SwitchCompleted(itemId, false);

            return Ok();
        }
    }
}
