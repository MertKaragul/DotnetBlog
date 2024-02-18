using Core.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.GenericRepository {
	public class GenericRepository<T> : IGenericRepository<T> where T : class {

		private readonly AppDbContext _appDbContext;
		private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
			_dbSet = _appDbContext.Set<T>();
        }


        public async Task AddAsync(T t)
		{
			await _dbSet.AddAsync(t);
		}

		public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> values)
		{
			await _dbSet.AddRangeAsync(values);
			return values;
		}

		public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
		{
			return await _dbSet.AnyAsync(expression);
		}

		public void Delete(T t)
		{
			_dbSet.Remove(t);
		}

		public async Task<T> FindById(int id)
		{
			return await _dbSet.FindAsync(id);
		}

		public IQueryable<T> GetAll()
		{
			return _dbSet.AsQueryable();
		}

		public void RemoveRange(IEnumerable<T> values)
		{
			_dbSet.RemoveRange(values);
		}

		public void Update(T t)
		{
			_dbSet.Update(t);
		}

		public IQueryable<T> Where(Expression<Func<T, bool>> expression)
		{
			return _dbSet.Where(expression);
		}
	}
}
