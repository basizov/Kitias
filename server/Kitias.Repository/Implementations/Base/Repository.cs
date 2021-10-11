using Kitias.Persistence;
using Kitias.Repository.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Kitias.Repository.Implementations.Base
{
	public class Repository<T> : IRepository<T>
		where T : class
	{
		private readonly DataContext _dbContext;

		public Repository(DataContext dbContext) => _dbContext = dbContext;

		public virtual void Create(T entity) => throw new NotImplementedException();

		public virtual void Update(T entity) => throw new NotImplementedException();

		public virtual void Delete(T entity) => throw new NotImplementedException();

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
	}
}