using Core.Entities.Persisted;
using Core.Ports.Driving;

namespace Core.Services
{
    public class GroceryListItemService : GenericEntityService<GroceryListItem>, IGroceryListItemService
    {
        public GroceryListItemService(
            IGenericRepo<GroceryListItem> genericRepo) : base(genericRepo)
        {
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
