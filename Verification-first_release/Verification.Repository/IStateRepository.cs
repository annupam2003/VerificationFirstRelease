using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Verification.EntityModel;
using Verification.DomainModel;

namespace Verification.Repository
{
    public interface IStateRepository : IGenericRepository<StateModel>
    {
        //Task<int> Count(bool? IsActive);
        //Task<IEnumerable<StateModel>> Records(bool? IsActive);
        //Task<StateModel> RecordById(int Id);
        //Task<IEnumerable<StateModel>> RecordsByPaging(bool? IsActive, int take, int skip);
    }
}
