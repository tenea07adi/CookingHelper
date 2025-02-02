using Core.Entities.Abstract;
using Core.Entities.Transfer;

namespace Core.Ports.Driving
{
    public interface IGenericEntityService<T> where T : BaseEntity
    {
        public T? Get(int id);
        public List<T> Get(PaginationParameters paginationParameters, SearchParameters searchParameters);
        public List<T> GetAll();
        public T Add(T entity);
        public T Update(int id, T entity);
        public bool Delete(int id);
        public int Count();
        public int Count(SearchParameters searchParameters);
    }
}
