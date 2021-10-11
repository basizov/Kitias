using System;
using System.Linq;
using System.Linq.Expressions;

namespace Kitias.Repository.Interfaces.Base
{
	public interface IRepository<T>
	{
		void Create(T entity);
		void Update(T entity);
		void Delete(T entity);
		IQueryable<T> GetAll();
		IQueryable<T> GetAllWithInclude(params Expression<Func<T, object>>[] includeProperties);
		IQueryable<T> FindBy(Expression<Func<T, bool>> expression);
	}
}
