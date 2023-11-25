using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FootballApi.Core.Services
{
    public interface IBaseService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
       Task<T> GetById(int id);
        void Update(T entity);
        T Delete(int id);
        Task<T> Add(T entity);
        Task<T> FindAsync(Expression<Func<T, bool>> criteria);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        void DeleteRange(IEnumerable<T> entities);
    }
}
