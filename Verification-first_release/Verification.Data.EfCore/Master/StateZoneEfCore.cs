using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verification.EntityModel;
using Verification.DomainModel;
using Verification.Data.EfCore.BaseEfCore;
using System.Linq.Expressions;
using Verification.Mapper;

namespace Verification.Data.EfCore.Master
{
    public class StateZoneEfCore : CurdOperation<StateZone, StateZoneModel>
    {
        public StateZoneEfCore(VerificationDbContext verification, IEntityMapper<StateZone, StateZoneModel> mapper):base(verification, mapper)
        {

        }
    }
}
