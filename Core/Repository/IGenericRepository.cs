using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository {
	public interface IGenericRepository<T> where T : class {
		Task AddAsync(T t);
		void Delete(T t);
		void Update(T t);
		Task<T> FindById(int id);
		IQueryable<T> Where(Expression<Func<T,bool>> expression);
		Task<bool> AnyAsync(Expression<Func<T,bool>> expression);
		IQueryable<T> GetAll();
		void RemoveRange(IEnumerable<T> values); 
		Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> values); 
	}
}
