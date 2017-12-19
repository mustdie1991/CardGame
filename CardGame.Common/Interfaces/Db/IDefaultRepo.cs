using System;
using System.Collections.Generic;

namespace CardGame.Common.Interfaces.Db
{
    public interface IDefaultRepo<TEntity>
    {
        TEntity Add(TEntity entity);

        void Remove(Guid entityId);

        void RemoveRange(List<Guid> entityIds);

        void AddRange(List<TEntity> entities);

        TEntity GetOne(Guid entityId);

        List<TEntity> GetAll();

        List<TEntity> GetRangeByIds(List<Guid> ids);
    }
}