using Core.Entities.Persisted;
using Core.Ports.Driving;

namespace Core.Services
{
    public class GroceryListItemService : GenericEntityService<GroceryListItem>, IGroceryListItemService
    {
        private readonly IGenericRepo<GroceryList> _groceryListRepo;
        private readonly ISessionInfoService _sessionInfoService;

        public GroceryListItemService(
            IGenericRepo<GroceryListItem> groceryListItemRepo,
            IGenericRepo<GroceryList> groceryListRepo,
            ISessionInfoService sessionInfoService) : base(groceryListItemRepo)
        {
            _groceryListRepo = groceryListRepo;
            _sessionInfoService = sessionInfoService;

            additionalGetCondition = PrivateListFilter;

            _defaultOrderField = "CreatedAt";
        }

        public void SwitchCompleted(int listId, bool completed)
        {
            var record = _genericRepo.Get(listId);

            if (record == null)
            {
                throw new Exception(Constants.Exceptions.EntityNotFound);
            }

            record.IsCompleted = completed;

            _genericRepo.Update(record);
        }

        private bool PrivateListFilter(GroceryListItem listItem)
        {
            var currentUser = _sessionInfoService.GetCurrentUserInfo();
            var list = _groceryListRepo.Get(listItem.Id);

            if (listItem == null || list == null || currentUser == null)
            {
                return false;
            }

            if (!list.IsPrivate)
            {
                return true;
            }

            if (currentUser.Id == list.CreatedBy)
            {
                return true;
            }

            return false;
        }
    }
}
