using Core.Entities.Persisted;
using Core.Ports.Driving;

namespace Core.Services
{
    public class GroceryListService : GenericEntityService<GroceryList>, IGroceryListService
    {
        private readonly IGenericRepo<GroceryListItem> _groceryListItemRepo;
        public GroceryListService(
            IGenericRepo<GroceryList> genericRepo, 
            IGenericRepo<GroceryListItem> groceryListItemRepo) : base(genericRepo)
        {
            _groceryListItemRepo = groceryListItemRepo;

            _defaultOrderField = "CreatedAt";
        }

        public List<GroceryListItem> GetItems(int listId)
        {
            return _groceryListItemRepo.Get(c => c.GroceryListId == listId, c => c.IsCompleted);
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
    }
}
