namespace WpfApp.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using WpfApp.Models;


    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        internal TaxiAppDbContext Context;
        internal DbSet<TEntity> DbSet;

        public GenericRepository(TaxiAppDbContext context)
        {
            this.Context = context;
            this.DbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = this.DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetByID(object id)
        {
            return this.DbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            this.DbSet.Add(entity);
        }

        public virtual void UpdateList(List<TEntity> entity)
        {
            for (int i = 0; i < entity.Count; i++)
            {
                this.DbSet.Attach(entity[i]);
                this.Context.Entry(entity[i]).State = EntityState.Modified;

            }

            this.Context.SaveChanges();
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = this.DbSet.Find(id);
            this.Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (this.Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                this.DbSet.Attach(entityToDelete);
            }

            this.DbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            this.DbSet.Attach(entityToUpdate);
            this.Context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
