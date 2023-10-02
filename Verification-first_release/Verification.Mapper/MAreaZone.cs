using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verification.EntityModel;
using Verification.DomainModel;

namespace Verification.Mapper
{
    public class MAreaZone : IEntityMapper<AreaZone, AreaZoneModel>
    {
        public List<State> States { get; set; }

        public AreaZoneModel mapFromEntity(AreaZone Entity)
        {
            return new AreaZoneModel
            {
                Id = Entity.Id,
                IsActive = Entity.IsActive,
                DateIs = Entity.DateIs,
                Name = Entity.Name,
                State = Entity.State.Name
            };
        }

        public bool mapFromEntity(AreaZone Entity, AreaZoneModel Model)
        {
            Model.Id = Entity.Id;
            Model.IsActive = Entity.IsActive;
            Model.DateIs = Entity.DateIs;
            Model.Name = Entity.Name;
            Model.State = Entity.State.Name;
            return true;
        }

        public AreaZone mapToEntity(AreaZoneModel Model)
        {
            var state = States.Where(x => x.Name.ToUpper().Trim().Equals(Model.State.ToUpper().Trim())).Select(x => x).FirstOrDefault();
            if (state != null)
            {
                return new AreaZone
                {
                    Id = Model.Id,
                    IsActive = Model.IsActive,
                    DateIs = Model.DateIs,
                    Name = Model.Name,
                    State = state,
                    StateId = state.Id
                };
            }
            return null;
        }

        public bool mapToEntity(AreaZoneModel Model, AreaZone Entity)
        {
            var state = States.Where(x => x.Name.ToUpper().Trim().Equals(Model.State.ToUpper().Trim())).Select(x => x).FirstOrDefault();
            if (state != null)
            {
                Entity.Id = Model.Id;
                Entity.IsActive = Model.IsActive;
                Entity.DateIs = Model.DateIs;
                Entity.Name = Model.Name;
                Entity.State = state;
                Entity.StateId = state.Id;
                return true;
            }
            return false;
        }
    }
}
