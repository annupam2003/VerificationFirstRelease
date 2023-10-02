using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Verification.Data.EfCore.BaseEfCore;
using Verification.EntityModel;
using Verification.Mapper;
using Verification.DomainModel;
using static Verification.Tracker.Track;

namespace Verification.Data.EfCore.Master
{
    public class AreaEfCore : CurdOperation<Area, AreaModel>
    {
        #region "Fields"
        private readonly IEntityMapper<Area, AreaModel> mapper;
        private readonly VerificationDbContext dbContext;
        private readonly Stopwatch stopwatch;
        private readonly List<AreaZone> areaZone;
        #endregion

        public AreaEfCore(VerificationDbContext verification, IEntityMapper<Area, AreaModel> mapper) : base(verification, mapper)
        {
            this.dbContext = verification;
            this.mapper = mapper;
            stopwatch = new Stopwatch();
            areaZone = verification.AreaZone.ToList();   //relation ship entity
        }

        #region "Create"
        //Create New Record
        public override async Task<Tuple<bool, dynamic>> InsertSingle(AreaModel model)
        {
            try
            {
                stopwatch.Start();
                ((MArea)mapper).areaZones = areaZone;
                Area entity = mapper.mapToEntity(model);
                if (entity != null)
                {
                    await dbContext.Area.AddAsync(entity);
                    var result = await dbContext.SaveChangesAsync();
                    return new Tuple<bool, dynamic>(true, entity);
                }
                else
                {
                    return new Tuple<bool, dynamic>(false, "Please Area Zone Name");
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
        public virtual async Task<Tuple<bool, dynamic>> EditSingle(int Id, AreaModel model)
        {
            try
            {
                stopwatch.Start();

                var area = await dbContext.Area.FindAsync(Id);
                if (area != null)
                {
                    ((MArea)mapper).areaZones = areaZone;
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
