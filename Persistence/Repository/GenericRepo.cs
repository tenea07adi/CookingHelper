using Core.Entities.Abstract;
using Core.Ports.Driving;
using Microsoft.EntityFrameworkCore;
using Persistence.DataBase;

namespace Persistence.Repository
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity
    {
        protected readonly DataBaseContext _dbContext;
        protected readonly ISessionInfoService _sessionInfoService;

        public GenericRepo(DataBaseContext dbContext, ISessionInfoService sessionInfoService)
        {
            _dbContext = dbContext;
            _sessionInfoService = sessionInfoService;
        }

        public T? Get(int id)
        {
            return Get(id, null);
        }

        public T? Get(int id, Func<T, bool>? filter)
        {
            if(filter == null)
            {
                filter = (c => true);
            }

            return _dbContext.Set<T>().AsEnumerable().FirstOrDefault(x => x.Id == id && filter(x));
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
            var currentInfo = _sessionInfoService.GetCurrentUserInfo();

            if (currentInfo != null)
            {
                entity.CreatedBy = currentInfo.Id;
                entity.UpdatedBy = currentInfo.Id;
            }
            else
            {
                entity.CreatedBy = 0;
                entity.UpdatedBy = 0;
            }

            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;

            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            var currentInfo = _sessionInfoService.GetCurrentUserInfo();

            if (currentInfo != null)
            {
                entity.UpdatedBy = currentInfo.Id;
            }
            else
            {
                entity.UpdatedBy = 0;
            }

            entity.UpdatedAt = DateTime.Now;

            _dbContext.Set<T>().Update(entity);

            _dbContext.Entry(entity).Property(c => c.CreatedAt).IsModified = false;
            _dbContext.Entry(entity).Property(c => c.CreatedBy).IsModified = false;

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

        public bool Exists(int id)
        {
            return Exists(id, null);
        }

        public bool Exists(int id, Func<T, bool>? filter)
        {
            if (filter == null)
            {
                filter = c => true;
            }

            if (_dbContext.Set<T>().AsNoTracking().AsEnumerable().Where(c => c.Id == id && filter(c)).Any())
            {
                return true;
            }

            return false;
        }

        public int Count()
        {
            return Count(null);
        }

        public int Count(Func<T, bool>? filter)
        {
            if(filter == null)
            {
                filter = c => true;
            }

            return _dbContext.Set<T>().AsNoTracking().AsEnumerable().Where(filter).Count();
        }

        public bool IsEntityCreatedByCurrentUser(int entityId)
        {
            var currentUser = _sessionInfoService.GetCurrentUserInfo();

            if (currentUser == null)
            {
                return false;
            }

            return IsEntityCreatedBy(entityId, currentUser.Id);
        }

        public bool IsEntityCreatedBy(int entityId, int userId)
        {
            return _dbContext.Set<T>().AsNoTracking().AsEnumerable().Where(c => c.Id == entityId && c.CreatedBy == userId).Any();
        }
    }
}
