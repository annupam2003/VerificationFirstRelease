using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verification.EntityModel;
using Verification.DomainModel;

namespace Verification.Mapper
{
    public class MState : IEntityMapper<State, StateModel>
    {
        public List<StateZone> stateZones { get; set; }

        public StateModel mapFromEntity(State state)
        {
            return new StateModel
            {
                Id = state.Id,
                IsActive = state.IsActive ?? true,
                DateIs = state.DateIs,
                Name = state.Name,
                StateZone = state.StateZone.Name
            };
        }

        public bool mapFromEntity(State state, StateModel stateModel)
        {
            stateModel.Id = state.Id;
            stateModel.IsActive = state.IsActive ?? true;
            stateModel.DateIs = state.DateIs;
            stateModel.Name = state.Name;
            stateModel.StateZone = state.StateZone.Name;
            return true;
        }

        public State mapToEntity(StateModel stateModel)
        {

            var stateZone = stateZones.Where(x => x.Name.ToUpper().Trim().Equals(stateModel.StateZone.ToUpper().Trim())).Select(x => x).FirstOrDefault();
            if (stateZone != null)
            {
                return new State
                {
                    Id = stateModel.Id,
                    IsActive = stateModel.IsActive,
                    DateIs = stateModel.DateIs,
                    Name = stateModel.Name,
                    StateZone = stateZone,
                    StateZoneId = stateZone.Id
                };
            }
            return null;
        }

        public bool mapToEntity(StateModel stateModel, State state)
        {
            var stateZone = stateZones.Where(x => x.Name.ToUpper().Trim().Equals(stateModel.StateZone.ToUpper().Trim())).Select(x => x).FirstOrDefault();
            if (stateZone != null)
            {
                state.Id = stateModel.Id;
                state.IsActive = stateModel.IsActive;
                state.DateIs = stateModel.DateIs;
                state.Name = stateModel.Name;
                state.StateZone = stateZone;
                state.StateZoneId = stateZone.Id;
                return true;
            }
            return false;

        }
    }
}
