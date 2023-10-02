using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verification.Data.EfCore;
using Verification.Data.EfCore.Master;
using Verification.EntityModel;
using Verification.Mapper;
using Verification.DomainModel;
using Verification.Tracker;
using static Verification.Tracker.GlobleEnums;
using static Verification.Tracker.Track;

namespace Verification.Repository.EfCore.Master
{
    public class AreaZoneRepository : IAreaZoneRepository
    {
        private readonly AreaZoneEfCore areaZone;
        Stopwatch stopwatch;
        public AreaZoneRepository(VerificationDbContext verification, IEntityMapper<AreaZone, AreaZoneModel> mapper)
        {
            areaZone = new AreaZoneEfCore(verification, mapper);
            stopwatch = new Stopwatch();
        }


        

        

        public async Task<Tuple<GlobleEnums.ResponseStatus, dynamic>> Create(AreaZoneModel model)
        {
            try
            {
                stopwatch.Start();

                var IsExists = await areaZone.Count(x => x.Name.Trim().ToUpper().Equals(model.Name.Trim().ToUpper()));
                if (IsExists == 0)
                {
                    var result = await areaZone.InsertSingle(model);
                    if (result.Item1)
                    {
                        return new Tuple<ResponseStatus, dynamic>(ResponseStatus.Success, ((StateZone)result.Item2).Id);
                    }
                    else
                    {
                        return new Tuple<ResponseStatus, dynamic>(ResponseStatus.Fail, result.Item2);
                    }
                }
                else
                {
                    return new Tuple<ResponseStatus, dynamic>(ResponseStatus.Exists, "Name Already Exists");
                }
            }
            catch (Exception ex)
            {
                GetInstance().Error("Verification.Repository.EfCore|AreaZoneRepository|Create ", ex.Message);
                return new Tuple<ResponseStatus, dynamic>(ResponseStatus.Error, ex.Message);
            }
            finally
            {
                GetInstance().Debug(stopwatch, "Verification.Repository.EfCore|AreaZoneRepository|Create ");
            }
        }

        public async Task<Tuple<GlobleEnums.ResponseStatus, dynamic>> Update(int Id, AreaZoneModel model)
        {
            try
            {
                stopwatch.Start();
                var IsExists = await areaZone.Count(x => x.Name.Trim().ToUpper().Equals(model.Name.Trim().ToUpper()));
                if (IsExists == 0)
                {

                    var result = await areaZone.EditSingle(Id, model);
                    if (result.Item1)
                    {
                        return new Tuple<ResponseStatus, dynamic>(ResponseStatus.Success, result.Item2);
                    }
                    else
                    {
                        return new Tuple<ResponseStatus, dynamic>(ResponseStatus.Fail, result.Item2);
                    }
                }
                else
                {
                    return new Tuple<ResponseStatus, dynamic>(ResponseStatus.Exists, "Name Already Exists ");
                }
            }
            catch (Exception ex)
            {
                GetInstance().Error("Verification.Repository.EfCore|AreaZoneRepository|Update ", ex.Message);
                return new Tuple<ResponseStatus, dynamic>(ResponseStatus.Error, ex.Message);
            }
            finally
            {
                GetInstance().Debug(stopwatch, "Verification.Repository.EfCore|AreaZoneRepository|Update ");
            }
        }

        public async Task<Tuple<GlobleEnums.ResponseStatus, dynamic>> Delete(int Id)
        {
            try
            {
                stopwatch.Start();
                var result = await areaZone.RemoveSingle(Id);
                if (result.Item1)
                {
                    return new Tuple<ResponseStatus, dynamic>(ResponseStatus.Success, true);
                }
                else
                {
                    return new Tuple<ResponseStatus, dynamic>(ResponseStatus.Fail, false);
                }
            }
            catch (Exception ex)
            {
                GetInstance().Error("Verification.Repository.EfCore|AreaZoneRepository|Delete ", ex.Message);
                return new Tuple<ResponseStatus, dynamic>(ResponseStatus.Error, ex.Message);
            }
            finally
            {
                GetInstance().Debug(stopwatch, "Verification.Repository.EfCore|AreaZoneRepository|Delete ");
            }
        }

        public async Task<int> Count(bool? IsActive)
        {
            try
            {
                stopwatch.Start();
                return await areaZone.Count(IsActive == null ? null : x => x.IsActive == IsActive);
            }
            catch (Exception ex)
            {
                GetInstance().Error("Verification.Repository.EfCore|AreaZoneRepository|Count ", ex.Message);
                return -1;
            }
            finally
            {
                GetInstance().Debug(stopwatch, "Verification.Repository.EfCore|AreaZoneRepository|Count ");
            }

        }
        public async Task<Tuple<GlobleEnums.ResponseStatus, IEnumerable<AreaZoneModel>>> Records(bool? IsActive)
        {
            try
            {
                stopwatch.Start();
                var result = await areaZone.Filter(IsActive == null ? null : x => x.IsActive == IsActive);
                if (result.Item1)
                {
                    return new Tuple<ResponseStatus, IEnumerable<AreaZoneModel>>(ResponseStatus.Success, (List<AreaZoneModel>)result.Item2);
                }
                else
                {
                    return new Tuple<ResponseStatus, IEnumerable<AreaZoneModel>>(ResponseStatus.Fail, null);
                }

            }
            catch (Exception ex)
            {
                GetInstance().Error("Verification.Repository.EfCore|AreaZoneRepository|Records ", ex.Message);
                return null;
            }
            finally
            {
                GetInstance().Debug(stopwatch, "Verification.Repository.EfCore|AreaZoneRepository|Records ");
            }
        }

        public async Task<Tuple<GlobleEnums.ResponseStatus, AreaZoneModel>> RecordById(int Id)
        {
            try
            {
                stopwatch.Start();
                var result = await areaZone.Filter(x => x.Id == Id);
                if (result.Item1)
                    return new Tuple<ResponseStatus, AreaZoneModel>(ResponseStatus.Success, ((List<AreaZoneModel>)result.Item2).FirstOrDefault());
                return new Tuple<ResponseStatus, AreaZoneModel>(ResponseStatus.Fail, null);
            }
            catch (Exception ex)
            {
                GetInstance().Error("Verification.Repository.EfCore|AreaZoneRepository|RecordById ", ex.Message);
                return null;
            }
            finally
            {
                GetInstance().Debug(stopwatch, "Verification.Repository.EfCore|AreaZoneRepository|RecordById ");
            }
        }

        public async Task<Tuple<GlobleEnums.ResponseStatus, IEnumerable<AreaZoneModel>>> RecordsByPagingAll(int take, int skip)
        {
            try
            {
                stopwatch.Start();
                var result = await areaZone.Filter(null, take, skip);
                if (result.Item1)
                {
                    return new Tuple<ResponseStatus, IEnumerable<AreaZoneModel>>(ResponseStatus.Success, (List<AreaZoneModel>)result.Item2);
                }
                else
                {
                    return new Tuple<ResponseStatus, IEnumerable<AreaZoneModel>>(ResponseStatus.Fail, null);
                }
            }
            catch (Exception ex)
            {
                GetInstance().Error("Verification.Repository.EfCore|AreaZoneRepository|RecordsByPagingAll ", ex.Message);
                return null;
            }
            finally
            {
                GetInstance().Debug(stopwatch, "Verification.Repository.EfCore|AreaZoneRepository|RecordsByPagingAll ");
            }
        }

        public async Task<Tuple<GlobleEnums.ResponseStatus, IEnumerable<AreaZoneModel>>> RecordsByPagingFilter(bool IsActive, int take, int skip)
        {
            try
            {
                stopwatch.Start();
                var result = await areaZone.Filter(x => x.IsActive == IsActive, take, skip);
                if (result.Item1)
                {
                    return new Tuple<ResponseStatus, IEnumerable<AreaZoneModel>>(ResponseStatus.Success, (List<AreaZoneModel>)result.Item2);
                }
                else
                {
                    return new Tuple<ResponseStatus, IEnumerable<AreaZoneModel>>(ResponseStatus.Fail, null);
                }
            }
            catch (Exception ex)
            {
                GetInstance().Error("Verification.Repository.EfCore|AreaZoneRepository|RecordsByPagingFilter ", ex.Message);
                return null;
            }
            finally
            {
                GetInstance().Debug(stopwatch, "Verification.Repository.EfCore|AreaZoneRepository|RecordsByPagingFilter ");
            }
        }
    }
}
