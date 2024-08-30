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

        public List<T> Get(Func<T, bool>? filter = null, Func<T, object>? orderByExpresion = null)
        {
            if (orderByExpresion == null)
            {
                orderByExpresion = (c => c.UpdatedAt);
            }

            if (filter == null)
            {
                filter = (c => true);
            }

            return _dbContext.Set<T>().AsEnumerable().OrderBy(orderByExpresion).Where(c => filter.Invoke(c)).ToList();
        }

        public List<T> Get(int offset, int maxsize)
        {
            return Get(offset, maxsize, null, null);
        }

        public List<T> Get(int offset, int maxsize, Func<T,object>? orderByExpresion, Func<T, bool>? filter)
        {
            if(orderByExpresion == null)
            {
                orderByExpresion = (c => c.UpdatedAt);
            }

            if(filter == null)
            {
                filter = (c => true);
            }

            return _dbContext.Set<T>()
                .AsEnumerable()
                .OrderBy(orderByExpresion)
                .Where(filter)
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
