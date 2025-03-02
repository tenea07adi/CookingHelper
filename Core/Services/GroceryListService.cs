using Core.Entities.Persisted;
using Core.Ports.Driving;

namespace Core.Services
{
    public class GroceryListService : GenericEntityService<GroceryList>, IGroceryListService
    {
        private readonly IGenericRepo<GroceryListItem> _groceryListItemRepo;
        private readonly ISessionInfoService _sessionInfoService;

        public GroceryListService(
            IGenericRepo<GroceryList> genericRepo, 
            IGenericRepo<GroceryListItem> groceryListItemRepo,
            ISessionInfoService sessionInfoService) : base(genericRepo)
        {
            _groceryListItemRepo = groceryListItemRepo;
            _sessionInfoService = sessionInfoService;

            onDeleteAction = DeleteLinkedItems;

            additionalGetCondition = PrivateListFilter;
            additionalUpdateCondition = IsEntityOwner;
            additionalDeleteCondition = IsEntityOwner;

            _defaultOrderField = "CreatedAt";
        }

        public List<GroceryListItem> GetItems(int listId)
        {
            var list = _genericRepo.Get(listId, additionalGetCondition);

            if (list == null) 
            {
                throw new KeyNotFoundException(Constants.Exceptions.EntityNotFound);
            }

            return _groceryListItemRepo.Get(c => c.GroceryListId == listId, c => c.IsCompleted);
        }

        public void SwitchCompleted(int listId, bool completed)
        {
            var record = _genericRepo.Get(listId, additionalGetCondition);

            if (record == null) 
            {
                throw new KeyNotFoundException(Constants.Exceptions.EntityNotFound);
            }

            record.IsCompleted = completed;

            _genericRepo.Update(record);
        }

        public void SwitchPin(int listId, bool pinned)
        {
            var record = _genericRepo.Get(listId, additionalGetCondition);

            if (record == null)
            {
                throw new KeyNotFoundException(Constants.Exceptions.EntityNotFound);
            }

            record.IsPinned = pinned;

            _genericRepo.Update(record);
        }

        private bool PrivateListFilter(GroceryList list)
        {
            var currentUser = _sessionInfoService.GetCurrentUserInfo();

            if (list == null || currentUser == null)
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

        private void DeleteLinkedItems(GroceryList list)
        {
            _groceryListItemRepo.Delete(c => c.GroceryListId == list.Id);
        }

        protected bool IsEntityOwner(GroceryList entity)
        {
            var user = _sessionInfoService.GetCurrentUserInfo();

            if (user == null)
            {
                return false;
            }

            if(user.Id != entity.CreatedBy) 
            { 
                return false; 
            }

            return true;
        }
    }
}
