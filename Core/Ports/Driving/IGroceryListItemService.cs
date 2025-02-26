using Core.Entities.Persisted;

namespace Core.Ports.Driving
{
    public interface IGroceryListItemService : IGenericEntityService<GroceryListItem>
    {
        public void SwitchCompleted(int listId, bool completed);
    }
}
