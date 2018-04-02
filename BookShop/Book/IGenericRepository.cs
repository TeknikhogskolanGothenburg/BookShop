using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Data 
{
    public interface IGenericRepository<T> where T : class
    {
        //T är Movies, Actors eller liknande
        //GetAll(): motsvarar somthing.ToList()
        ICollection<T> GetAll();
        Task<ICollection<T>> GetAllAsync();
        //Skicka in en funktion som har ex Movie som argument och returnerar boolsk värde
        ICollection<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void AddAsync(T entity);
        void AddRange(ICollection<T> entities);
        void Delete(T entity);
        void DeleteRange(ICollection<T> entities);
        void Update(T entity);
        void UpdateRange(ICollection<T> entities);
        void Save();
    }
}
