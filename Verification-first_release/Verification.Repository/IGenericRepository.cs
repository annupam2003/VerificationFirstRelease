using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Verification.Tracker.GlobleEnums;

namespace Verification.Repository
{
    //T is Database class
    public interface IGenericRepository<T> where T : class
    {
        Task<Tuple<ResponseStatus, dynamic>> Create(T entity);
        Task<Tuple<ResponseStatus, dynamic>> Update(int Id, T entity);
        Task<Tuple<ResponseStatus, dynamic>> Delete(int Id);
        Task<int> Count(bool? IsActive);
        Task<Tuple<ResponseStatus, IEnumerable<T>>> Records(bool? IsActive);
        Task<Tuple<ResponseStatus, T>> RecordById(int Id);
        Task<Tuple<ResponseStatus, IEnumerable<T>>> RecordsByPagingAll(int take, int skip);
        Task<Tuple<ResponseStatus, IEnumerable<T>>> RecordsByPagingFilter(bool IsActive, int take, int skip);
        
    }
}
