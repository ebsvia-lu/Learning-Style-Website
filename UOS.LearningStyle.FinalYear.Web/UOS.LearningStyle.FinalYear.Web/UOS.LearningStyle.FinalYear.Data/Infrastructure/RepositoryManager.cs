using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UOS.LearningStyle.FinalYear.Data.Infrastructure
{
    public abstract class RepositoryManager<T> where T : class
    {
        private DataContext dataContext;
        private readonly IDbSet<T> dbset;

        protected RepositoryManager(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            dbset = DataContext.Set<T>();
        }

        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        protected DataContext DataContext
        {
            get { return dataContext ?? (dataContext = DatabaseFactory.Get()); }
        }
        public virtual void Add(T entity)
        {
            dbset.Add(entity);
        }
        public virtual void Update(T entity)
        {
            dbset.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where, string[] includeProperties)
        {
            IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbset.Remove(obj);
        }

        public virtual T GetById(int id, string userid, string[] includeProperties)
        {
            IQueryable<T> query = dbset;
            if (includeProperties != null && includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    if (!string.IsNullOrEmpty(includeProperty))
                    {
                        query = query.Include(includeProperty.Trim());
                    }
                }
            }

            return dbset.Find(id, userid);
        }

        public virtual T GetById(long id, string[] includeProperties)
        {
            IQueryable<T> query = dbset;
            if (includeProperties != null && includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    if (!string.IsNullOrEmpty(includeProperty))
                    {
                        query = query.Include(includeProperty.Trim());
                    }
                }
            }

            return dbset.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbset.ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where, string[] includeProperties)
        {
            IQueryable<T> query = dbset;

            if (includeProperties != null && includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    if (!string.IsNullOrEmpty(includeProperty))
                    {
                        query = query.Include(includeProperty.Trim());
                    }
                }
            }

            return query.Where(where).ToList();
        }

        public T Get(Expression<Func<T, bool>> where, string[] includeProperties)
        {
            IQueryable<T> query = dbset;
            if (includeProperties != null && includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    if (!string.IsNullOrEmpty(includeProperty))
                    {
                        query = query.Include(includeProperty.Trim());
                    }
                }
            }

            return query.Where(where).FirstOrDefault<T>();
        }

        public void Commit()
        {
            DataContext.Commit();
        }
    }
}
