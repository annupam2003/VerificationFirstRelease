using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Verification.EntityModel;
using Verification.DomainModel;
using Verification.Mapper;
using static Verification.Tracker.Track;
using System.Diagnostics;
using System.Linq.Expressions;
using Verification.Data.EfCore.BaseEfCore;

namespace Verification.Data.EfCore.Master
{
    public class AreaZoneEfCore : CurdOperation<AreaZone, AreaZoneModel>
    {
        #region "Fields"
        private readonly IEntityMapper<AreaZone, AreaZoneModel> mapper;
        private readonly VerificationDbContext dbContext;
        private readonly Stopwatch stopwatch;
        private readonly List<State> state;
        #endregion

        public AreaZoneEfCore(VerificationDbContext verification, IEntityMapper<AreaZone, AreaZoneModel> mapper) : base(verification, mapper)
        {
            this.dbContext = verification;
            this.mapper = mapper;
            stopwatch = new Stopwatch();
            state = verification.State.ToList();     //relation ship entity
        }
        #region "Create"
        //Create New Record
        public override async Task<Tuple<bool, dynamic>> InsertSingle(AreaZoneModel model)
        {
            try
            {
                stopwatch.Start();
                ((MAreaZone)mapper).States = state;
                AreaZone entity = mapper.mapToEntity(model);
                if (entity != null)
                {
                    await dbContext.AreaZone.AddAsync(entity);
                    var result = await dbContext.SaveChangesAsync();
                    return new Tuple<bool, dynamic>(true, entity);
                }
                else
                {
                    return new Tuple<bool, dynamic>(false, "Please check State & Area Zone Name");
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
        public virtual async Task<Tuple<bool, dynamic>> EditSingle(int Id, AreaZoneModel model)
        {
            try
            {
                stopwatch.Start();

                var area = await dbContext.AreaZone.FindAsync(Id);
                if (area != null)
                {
                    ((MAreaZone)mapper).States = state;
                    mapper.mapToEntity(model, area);
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
