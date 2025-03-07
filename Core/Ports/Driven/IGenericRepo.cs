﻿using Core.Entities.Abstract;

namespace Core.Ports.Driving
{
    public interface IGenericRepo<T> where T : BaseEntity
    {
        public T? Get(int id);

        public T? Get(int id, Func<T, bool>? conditions = null);

        public List<T> Get(Func<T, bool>? filter = null, Func<T, object>? orderByExpresion = null);

        public List<T> Get(int offset, int maxsize);

        public List<T> Get(int offset, int maxsize, Func<T, object>? orderByExpresion = null, Func<T, bool>? filter = null);

        public void Add(T entity);

        public void Update(T entity);

        public void Delete(int id);

        public void Delete(Func<T, bool> filter);

        public bool Exists(int id);

        public bool Exists(int id, Func<T, bool> filter);

        public int Count();

        public int Count(Func<T, bool>? filter);

        public bool IsEntityCreatedByCurrentUser(int entityId);

        public bool IsEntityCreatedBy(int entityId, int userId);
    }
}
