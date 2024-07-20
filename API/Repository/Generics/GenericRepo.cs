using API.DataBase;
using API.Models.BaseModels;

namespace API.Repository.Generics
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseDBM
    {
        protected readonly DataBaseContext _dbContext;

        public GenericRepo(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T Get(int id)
        {
            return _dbContext.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public List<T> Get()
        {
            return _dbContext.Set<T>().ToList();
        }

        public List<T> Get(Func<T,bool> filter)
        {
            return _dbContext.Set<T>().AsEnumerable().Where(c => filter.Invoke(c)).ToList();
        }

        public List<T> Get(int offset, int maxsize)
        {
            return _dbContext.Set<T>()
                .Skip(offset)
                .Take(maxsize)
                .ToList();
        }

        public void Add(T entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;

            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            entity.UpdatedAt = DateTime.Now;

            _dbContext.Set<T>().Update(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            _dbContext.Set<T>().Remove(Get(id));
            _dbContext.SaveChanges();
        }

        public void Delete(Func<T, bool> filter)
        {
            var entities = _dbContext.Set<T>().AsEnumerable().Where(c => filter.Invoke(c));

            foreach (var entity in entities)
            {
                _dbContext.Set<T>().Remove(entity);
            }

            _dbContext.SaveChanges();
        }

        public bool Exists(int Id)
        {
            if (Get(Id) == null)
            {
                return false;
            }

            return true;
        }

        public int Count()
        {
            return _dbContext.Set<T>().Count();
        }
    }
}
