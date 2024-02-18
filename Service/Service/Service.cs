using Core.Repository;
using Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service {
	public class Service<T> : IGenericService<T> where T : class {

		private readonly IGenericRepository<T> _service;

        public Service(IGenericRepository<T> genericRepository)
        {
            _service = genericRepository;
        }

        public async Task AddAsync(T t)
		{
			await _service.AddAsync(t);
		}

		public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> values)
		{
			await _service.AddRangeAsync(values);
			return values;
		}

		public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
		{
			return await _service.AnyAsync(expression);
		}

		public void Delete(T t)
		{
			_service.Delete(t);
		}

		public async Task<T> FindById(int id)
		{
			return await _service.FindById(id);	
		}

		public IQueryable<T> GetAll()
		{
			return _service.GetAll();
		}

		public void RemoveRange(IEnumerable<T> values)
		{
			_service.RemoveRange(values);
		}

		public void Update(T t)
		{
			_service.Update(t);
		}

		public IQueryable<T> Where(Expression<Func<T, bool>> expression)
		{
			return _service.Where(expression);
		}
	}
}
