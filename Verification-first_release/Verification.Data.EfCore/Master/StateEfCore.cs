using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Verification.EntityModel;
using Verification.DomainModel;
using Verification.Mapper;
using static Verification.Tracker.Track;
using Verification.Data.EfCore.BaseEfCore;

namespace Verification.Data.EfCore.Master
{
    public class StateEfCore : CurdOperation<State, StateModel>
    {
        #region "Fields"
        private readonly IEntityMapper<State, StateModel> mapper;
        private readonly VerificationDbContext dbContext;
        private readonly Stopwatch stopwatch;
        private readonly List<StateZone> stateZones;
        #endregion

        #region "Construtor"
        public StateEfCore(VerificationDbContext verification, IEntityMapper<State, StateModel> mapper) : base(verification, mapper)
        {
            this.dbContext = verification;
            this.mapper = mapper;
            stopwatch = new Stopwatch();
            stateZones = verification.StateZone.ToList();   //relation ship entity
        }
        #endregion

        #region "Create"
        //Create New Record
        public override async Task<Tuple<bool, dynamic>> InsertSingle(StateModel model)
        {
            try
            {
                stopwatch.Start();
                ((MState)mapper).stateZones = stateZones;
                State entity = mapper.mapToEntity(model);
                if (entity != null)
                {
                    await dbContext.State.AddAsync(entity);
                    var result = await dbContext.SaveChangesAsync();
                    return new Tuple<bool, dynamic>(true, entity);
                }
                else
                {
                    return new Tuple<bool, dynamic>(false, "State Zone not exits");
                }
            }
            catch (Exception ex)
            {
                GetInstance().Error(ex.Message);
                return new Tuple<bool, dynamic>(false, ex.Message);
            }
            finally
            {
                GetInstance().Debug(stopwatch, "debug");
            }
        }
        #endregion

        #region "Update"
        //Update Existing Record
        public virtual async Task<Tuple<bool, dynamic>> EditSingle(int Id, StateModel model)
        {
            try
            {
                stopwatch.Start();

                var state = await dbContext.State.FindAsync(Id);
                if (state != null)
                {
                    ((MState)mapper).stateZones = stateZones;
                    mapper.mapToEntity(model, state);
                    var result = await dbContext.SaveChangesAsync();
                    return new Tuple<bool, dynamic>(true, result);
                }
                return new Tuple<bool, dynamic>(false, "No Record Found.");
            }
            catch (Exception ex)
            {
                return new Tuple<bool, dynamic>(false, ex.Message);
            }
            finally
            {
                GetInstance().Debug(stopwatch, "debug");
            }
        }
        #endregion
    }
}
