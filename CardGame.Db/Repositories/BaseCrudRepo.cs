using System;
using System.Collections.Generic;
using System.Linq;
using CardGame.Common.Classes.Db;
using CardGame.Db.Contexts;
using CardGame.Db.UnitOfWork;

namespace CardGame.Db.Repositories
{
    public class BaseCrudRepo<TEntity> where TEntity : EntityTrackable
    {
        protected readonly MainDbContext Context;

        public BaseCrudRepo(CommonUnitOfWork<MainDbContext> uow)
        {
            this.Context = uow.Context;
        }

        public TEntity Add(TEntity entity)
        {
            entity.Created = DateTime.Now;
            return this.Context.Set<TEntity>().Add(entity).Entity;
        }

        public void Remove(Guid entityId)
        {
            var entity = this.GetOne(entityId);
            if (entity != null)
            {
                this.Context.Set<TEntity>().Remove(entity);
            }

            throw new ArgumentException($"Unable to delete {typeof(TEntity).Name} by id {entityId}. Entity does not exist.");
        }

        public TEntity GetOne(Guid entityId)
        {
            return this.Context.Set<TEntity>().FirstOrDefault(x => x.EntityId == entityId);
        }

        public List<TEntity> GetAll()
        {
            return this.Context.Set<TEntity>().ToList();
        }

        public void RemoveRange(List<Guid> entityIds)
        {
            var entities = this.GetRangeByIds(entityIds);
            if (entities.Any())
            {
                this.Context.Set<TEntity>().RemoveRange(entities);
            }
        }

        public void AddRange(List<TEntity> entities)
        {
            entities.ForEach(x =>
            {
                x.Created = DateTime.Now;
            });

            this.Context.Set<TEntity>().AddRange(entities);
        }

        public List<TEntity> GetRangeByIds(List<Guid> ids)
        {
            return this.Context.Set<TEntity>().Where(x => ids.Contains(x.EntityId)).ToList();
        }
    }
}
