using Kitias.Persistence.Contexts;
using Kitias.Repository.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kitias.Repository.Implementations.Base
{
	/// <summary>
	/// Instance of base repository
	/// </summary>
	/// <typeparam name="T">Type of entity</typeparam>
	public class Repository<T> : IRepository<T>
		where T : class
	{
		private readonly DataContext _dbContext;

		/// <summary>
		/// Configure all necessary services
		/// </summary>
		/// <param name="dbContext">Db context to work of some dbs</param>
		public Repository(DataContext dbContext) => _dbContext = dbContext;

		public virtual T Create(T entity) => _dbContext.Set<T>().Add(entity).Entity;

		public virtual T Update(T entity)
		{
			var result = _dbContext.Set<T>().Attach(entity).Entity;
			_dbContext.Entry(entity).State = EntityState.Modified;

			return result;
		}

		public virtual void Delete(T entity)
		{
			var dbSet = _dbContext.Set<T>();

			if (_dbContext.Entry(entity).State == EntityState.Detached)
				dbSet.Attach(entity);
			dbSet.Remove(entity);
		}

		public IQueryable<T> FindBy(Expression<Func<T, bool>> expression) => _dbContext.Set<T>().Where(expression).AsNoTracking();

		public IQueryable<T> GetAllWithInclude(params Expression<Func<T, object>>[] includeProperties)
		{
			var query = _dbContext.Set<T>().AsQueryable();
			var includedQuery = includeProperties.Aggregate(
				query,
				(current, includeProperty) => current.Include(includeProperty)
			);

			return includedQuery.AsNoTracking();
		}

		public IQueryable<T> GetAll() => _dbContext.Set<T>().AsNoTracking();

		public Task<bool> AnyAsync(Expression<Func<T, bool>> expression) => _dbContext.Set<T>().AnyAsync(expression);
	}
}
