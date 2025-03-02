using Core.Entities.Persisted;

namespace Core.Ports.Driving
{
    public interface IGroceryListService : IGenericEntityService<GroceryList>
    {
        public List<GroceryListItem> GetItems(int listId);
        public void SwitchCompleted(int listId, bool completed);
        public void SwitchPin(int listId, bool pinned);
    }
}
