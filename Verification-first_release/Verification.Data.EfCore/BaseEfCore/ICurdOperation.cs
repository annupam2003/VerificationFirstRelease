using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Verification.Data.EfCore.BaseEfCore
{
    public interface ICurdOperation<E, M>
    {
        Task<Tuple<bool, dynamic>> InsertSingle(M model);
        Task<Tuple<bool, dynamic>> InsertBulk(List<M> Models);
        Task<Tuple<bool, dynamic>> EditSingle(int Id, M model);
        Task<Tuple<bool, dynamic>> EditBulk(IDictionary<int, M> models);
        Task<Tuple<bool, dynamic>> RemoveSingle(int Id);
        Task<Tuple<bool, dynamic>> RemoveBulk(List<int> Id);
        Task<Tuple<bool, IEnumerable<M>>> Filter(Expression<Func<E, bool>> ? predicate, int Tk = 0, int Sk = 0);
        Task<int> Count(Expression<Func<E, bool>> ? predicate);
    }
}
