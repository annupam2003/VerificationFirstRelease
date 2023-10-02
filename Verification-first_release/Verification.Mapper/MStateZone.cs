using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verification.EntityModel;
using Verification.DomainModel;

namespace Verification.Mapper
{
    public class MStateZone : IEntityMapper<StateZone, StateZoneModel>
    {
        public StateZoneModel mapFromEntity(StateZone Entity)
        {
            return new StateZoneModel
            {
                Id= Entity.Id,
                IsActive = Entity.IsActive ?? true,
                DateIs = Entity.DateIs,
                Name = Entity.Name
            };
        }

        public bool mapFromEntity(StateZone Entity, StateZoneModel Model)
        {
            Model.Id = Entity.Id;
            Model.IsActive = Entity.IsActive?? true;
            Model.DateIs = Entity.DateIs;
            Model.Name = Entity.Name;
            return true;
        }

        public StateZone mapToEntity(StateZoneModel Model)
        {
            return new StateZone
            {
                Id = Model.Id,
                IsActive = Model.IsActive,
                DateIs = Model.DateIs,
                Name = Model.Name,
                States = null
            };
        }

        public bool mapToEntity(StateZoneModel Model, StateZone Entity)
        {
            Entity.Id = Model.Id;
            Entity.IsActive = Model.IsActive;
            Entity.DateIs = Model.DateIs;
            Entity.Name = Model.Name;
            Entity.States = null;
            return true;
        }
    }
}
