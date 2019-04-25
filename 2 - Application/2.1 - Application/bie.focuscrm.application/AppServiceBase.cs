

using System;

using System.Collections.Generic;
using bie.focuscrm.application.Interfaces;
using bie.focuscrm.domain.Interfaces.Service;

namespace bie.focuscrm.application
{

    public class AppServiceBase<TEntity> : IDisposable, IAppServiceBase<TEntity> where TEntity : class
    {
        private readonly IServiceBase<TEntity> _serviceBase;

        public AppServiceBase(IServiceBase<TEntity> serviceBase)
        {
            _serviceBase = serviceBase;
        }

        public void Add(TEntity obj)
        {
            _serviceBase.Add(obj);
        }

        public TEntity GetById(int id)
        {
            return _serviceBase.GetById(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _serviceBase.GetAll();
        }

        public void Update(TEntity obj)
        {
            _serviceBase.Update(obj);
        }

        public void Remove(TEntity obj)
        {
            _serviceBase.Remove(obj);
        }

        public void Dispose()
        {
            _serviceBase.Dispose();
        }

        public IEnumerable<TEntity> GetTakeSkip(int take, int skip)
        {
            return _serviceBase.GetTakeSkip(take, skip);
        }

        public IEnumerable<TEntity> GetSkipTake(int skip, int take)
        {
            return _serviceBase.GetSkipTake(skip, take);

        }

        public int Count()
        {
            return _serviceBase.Count();
        }

        public long LongCount()
        {
            return _serviceBase.LongCount();
        }

        public IEnumerable<TEntity> DoSearch(string strSearch)
        {
            return _serviceBase.DoSearch(strSearch);
        }

   
    }
}
