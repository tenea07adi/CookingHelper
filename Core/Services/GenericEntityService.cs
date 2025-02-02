using Core.Entities.Abstract;
using Core.Entities.Transfer;
using Core.Ports.Driving;
using System.ComponentModel;

namespace Core.Services
{
    public class GenericEntityService<T> : IGenericEntityService<T> where T : BaseEntity
    {
        protected readonly IGenericRepo<T> _genericRepo;
        protected string? _defaultOrderField;

        protected Action<T> onAddAction = (t) => { };
        protected Action<T> onUpdateAction = (t) => { };
        protected Action<T> onDeleteAction = (t) => { };

        protected Action<T> afterAddAction = (t) => { };
        protected Action<T> afterUpdateAction = (t) => { };
        protected Action<T> afterDeleteAction = (t) => { };

        public GenericEntityService(IGenericRepo<T> genericRepo)
        {
            _genericRepo = genericRepo;
        }

        public T? Get(int id)
        {
            return _genericRepo.Get(id);
        }

        public List<T> Get(PaginationParameters paginationParameters, SearchParameters searchParameters)
        {
            if (paginationParameters.Offset < 0)
            {
                throw new ArgumentException(Constants.Exceptions.InvalidEntityFilter);
            }

            if (paginationParameters.Maxsize == 0)
            {
                paginationParameters.Maxsize = 20;
            }

            if (string.IsNullOrEmpty(paginationParameters.OrderBy) && !string.IsNullOrEmpty(_defaultOrderField))
            {
                paginationParameters.OrderBy = _defaultOrderField;
            }

            var entities = _genericRepo.Get(
                paginationParameters.Offset, 
                paginationParameters.Maxsize, 
                GetOrderByExpresion<T>(paginationParameters.OrderBy!), 
                GetFilterExpresion<T>(searchParameters));

            return entities;
        }

        public List<T> GetAll()
        {
            var entities = _genericRepo.Get();

            return entities;
        }

        public T Add(T entity)
        {
            onAddAction(entity);

            _genericRepo.Add(entity);

            afterAddAction(entity);

            return entity;
        }

        public T Update(int id, T entity)
        {
            entity.Id = id;

            onUpdateAction(entity);

            _genericRepo.Update(entity);

            afterUpdateAction(entity);

            return entity;
        }

        public bool Delete(int id)
        {
            var entity = _genericRepo.Get(id);

            if (entity == null)
            {
                return false;
            }

            onDeleteAction(entity);

            _genericRepo.Delete(id);

            afterDeleteAction(entity);

            return true;
        }

        public int Count()
        {
            var count = _genericRepo.Count();

            return count;
        }

        public int Count(SearchParameters searchParameters)
        {
            var filter = GetFilterExpresion<T>(searchParameters);

            var count = 0;

            if (filter == null)
            {
                count = Count();
            }

            count = _genericRepo.Get().Where(filter!).Count();

            return count;
        }

        protected Func<TOrderObj, object>? GetOrderByExpresion<TOrderObj>(string? fieldName)
        {
            Func<TOrderObj, object>? returnExp = null;

            if (string.IsNullOrEmpty(fieldName))
            {
                return returnExp;
            }
            
            var propertyInfo = typeof(TOrderObj).GetProperty(fieldName);

            if (propertyInfo != null)
            {
                returnExp = (x => propertyInfo.GetValue(x, null)!);
            }

            return returnExp;
        }

        protected Func<TFilterObj, bool>? GetFilterExpresion<TFilterObj>(SearchParameters filter)
        {
            Func<TFilterObj, bool>? returnExp = null;

            if (string.IsNullOrEmpty(filter.Field) || string.IsNullOrEmpty(filter.Value) || filter.EvaluationType == null)
            {
                return returnExp;
            }

            var propertyInfo = typeof(TFilterObj).GetProperty(filter.Field);

            if (propertyInfo != null)
            {
                returnExp = (x => {
                    try
                    {
                        var converter = TypeDescriptor.GetConverter(propertyInfo.PropertyType);
                        var convertedValue = converter.ConvertFromString(filter.Value);

                        switch (filter.EvaluationType)
                        {
                            case (SearchEvaluationTypes.Equals):
                                return propertyInfo.GetValue(x, null).Equals(convertedValue);
                            case (SearchEvaluationTypes.NotEquals):
                                return !propertyInfo.GetValue(x, null).Equals(convertedValue);
                            case (SearchEvaluationTypes.Contains):
                                if (propertyInfo.PropertyType == typeof(string))
                                {
                                    return propertyInfo.GetValue(x, null).ToString().ToLower().Contains(convertedValue.ToString().ToLower());
                                }
                                return false;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                    return false;
                });
            }

            return returnExp;
        }
    }
}
