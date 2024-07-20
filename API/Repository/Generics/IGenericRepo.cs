using API.Models.BaseModels;

namespace API.Repository.Generics
{
    public interface IGenericRepo<T> where T : BaseDBM
    {
        public T Get(int id);

        public List<T> Get();

        public List<T> Get(Func<T, bool> filter);

        public List<T> Get(int offset, int maxsize);

        public void Add(T entity);

        public void Update(T entity);

        public void Delete(int id);

        public void Delete(Func<T, bool> filter);

        public bool Exists(int Id);

        public int Count();
    }
}
