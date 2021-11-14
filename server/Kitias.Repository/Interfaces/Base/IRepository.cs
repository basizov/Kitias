using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kitias.Repository.Interfaces.Base
{
	/// <summary>
	/// Base repository to work woth db
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IRepository<T>
	{
		/// <summary>
		/// Create new db instance
		/// </summary>
		/// <param name="entity">Db entity</param>
		/// <returns>New db entity</returns>
		T Create(T entity);
		/// <summary>
		/// Update db instance
		/// </summary>
		/// <param name="entity">Db entity</param>
		/// <returns>New db entity</returns>
		T Update(T entity);
		/// <summary>
		/// Delete entity drom db
		/// </summary>
		/// <param name="entity">Db entity</param>
		void Delete(T entity);
		/// <summary>
		/// Take all entities from db
		/// </summary>
		/// <returns>Entities</returns>
		IQueryable<T> GetAll();
		/// <summary>
		/// Take entityes with include params
		/// </summary>
		/// <param name="includeProperties">Include params</param>
		/// <returns>Entities</returns>
		IQueryable<T> GetAllWithInclude(params Expression<Func<T, object>>[] includeProperties);
		/// <summary>
		/// Find by condition
		/// </summary>
		/// <param name="expression">Expression function</param>
		/// <returns>Entities</returns>
		IQueryable<T> FindBy(Expression<Func<T, bool>> expression);
		/// <summary>
		/// Find by condition with include
		/// </summary>
		/// <param name="expression">Expression function</param>
		/// <param name="includeProperties">Include params</param>
		/// <returns>Entities</returns>
		IQueryable<T> FindByAndInclude(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties);
		/// <summary>
		/// Check entities by condition for existing
		/// </summary>
		/// <param name="expression">Expression function</param>
		/// <returns>Is success</returns>
		Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
	}
}
