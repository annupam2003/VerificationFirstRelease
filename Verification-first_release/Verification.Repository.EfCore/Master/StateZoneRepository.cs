using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Verification.Data.EfCore;
using Verification.Data.EfCore.BaseEfCore;
using Verification.Data.EfCore.Master;
using Verification.EntityModel;
using Verification.Mapper;
using Verification.DomainModel;
using static Verification.Tracker.GlobleEnums;
using static Verification.Tracker.Track;

namespace Verification.Repository.EfCore.Master
{
    public class StateZoneRepository : IStateZoneRepository
    {
        private readonly StateZoneEfCore stateZone;
        Stopwatch stopwatch;
        public StateZoneRepository(VerificationDbContext verification, IEntityMapper<StateZone, StateZoneModel> mapper)
        {
            stateZone = new StateZoneEfCore(verification, mapper);
            stopwatch = new Stopwatch();
        }

        public async Task<Tuple<ResponseStatus, dynamic>> Create(StateZoneModel model)
        {
            try
            {
                stopwatch.Start();

                var IsExists = await stateZone.Count(x => x.Name.Trim().ToUpper().Equals(model.Name.Trim().ToUpper()));
                if (IsExists == 0)
                {
                    var result = await stateZone.InsertSingle(model);
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
                GetInstance().Error("Verification.Repository.EfCore|StateZoneRepository|Create ", ex.Message);
                return new Tuple<ResponseStatus, dynamic>(ResponseStatus.Error, ex.Message);
            }
            finally
            {
                GetInstance().Trace(stopwatch, "Verification.Repository.EfCore|StateZoneRepository|Create ");
            }
        }

        public async Task<Tuple<ResponseStatus, dynamic>> Update(int Id, StateZoneModel model)
        {
            try
            {
                stopwatch.Start();
                var IsExists = await stateZone.Count(x => x.Name.Trim().ToUpper().Equals(model.Name.Trim().ToUpper()));
                if (IsExists == 0)
                {

                    var result = await stateZone.EditSingle(Id, model);
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
                GetInstance().Error("Verification.Repository.EfCore|StateZoneRepository|Update ", ex.Message);
                return new Tuple<ResponseStatus, dynamic>(ResponseStatus.Error, ex.Message);
            }
            finally
            {
                GetInstance().Trace(stopwatch, "Verification.Repository.EfCore|StateZoneRepository|Update ");
            }
        }

        public async Task<Tuple<ResponseStatus, dynamic>> Delete(int Id)
        {
            try
            {
                stopwatch.Start();
                var result = await stateZone.RemoveSingle(Id);
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
                GetInstance().Error("Verification.Repository.EfCore|StateZoneRepository|Delete ", ex.Message);
                return new Tuple<ResponseStatus, dynamic>(ResponseStatus.Error, ex.Message);
            }
            finally
            {
                GetInstance().Trace(stopwatch, "Verification.Repository.EfCore|StateZoneRepository|Delete ");
            }

        }

        public async Task<int> Count(bool? IsActive)
        {
            try
            {
                stopwatch.Start();
                return await stateZone.Count(IsActive == null ? null : x => x.IsActive == IsActive);
            }
            catch (Exception ex)
            {
                GetInstance().Error("Verification.Repository.EfCore|StateZoneRepository|Count ", ex.Message);
                return (int)ResponseStatus.Error;
            }
            finally
            {
                GetInstance().Trace(stopwatch, "Verification.Repository.EfCore|StateZoneRepository|Count ");
            }

        }

        public async Task<Tuple<ResponseStatus, StateZoneModel>> RecordById(int Id)
        {
            try
            {
                stopwatch.Start();
                var result = await stateZone.Filter(x => x.Id == Id);
                if (result.Item1)
                    return new Tuple<ResponseStatus, StateZoneModel>(ResponseStatus.Success, ((List<StateZoneModel>)result.Item2).FirstOrDefault());
                return new Tuple<ResponseStatus, StateZoneModel>(ResponseStatus.Fail, null);
            }
            catch (Exception ex)
            {
                GetInstance().Error("Verification.Repository.EfCore|StateZoneRepository|RecordById ", ex.Message);
                return new Tuple<ResponseStatus, StateZoneModel>(ResponseStatus.Error, null);
            }
            finally
            {
                GetInstance().Trace(stopwatch, "Verification.Repository.EfCore|StateZoneRepository|RecordById ");
            }
        }

        public async Task<Tuple<ResponseStatus, IEnumerable<StateZoneModel>>> Records(bool? IsActive)
        {
            try
            {
                stopwatch.Start();
                var result = await stateZone.Filter(IsActive == null ? null : x => x.IsActive == IsActive);
                if (result.Item1)
                {
                    return new Tuple<ResponseStatus, IEnumerable<StateZoneModel>>(ResponseStatus.Success, (List<StateZoneModel>)result.Item2);
                }
                else
                {
                    return new Tuple<ResponseStatus, IEnumerable<StateZoneModel>>(ResponseStatus.Fail, null);
                }

            }
            catch (Exception ex)
            {
                GetInstance().Error("Verification.Repository.EfCore|StateZoneRepository|Records ", ex.Message);
                return new Tuple<ResponseStatus, IEnumerable<StateZoneModel>>(ResponseStatus.Error, null);
            }
            finally
            {
                GetInstance().Trace(stopwatch, "Verification.Repository.EfCore|StateZoneRepository|Records ");
            }
        }

        public async Task<Tuple<ResponseStatus, IEnumerable<StateZoneModel>>> RecordsByPagingAll(int take, int skip)
        {
            try
            {
                stopwatch.Start();
                var result = await stateZone.Filter(null, take, skip);
                if (result.Item1)
                {
                    return new Tuple<ResponseStatus, IEnumerable<StateZoneModel>>(ResponseStatus.Success, (List<StateZoneModel>)result.Item2);
                }
                else
                {
                    return new Tuple<ResponseStatus, IEnumerable<StateZoneModel>>(ResponseStatus.Fail, null);
                }
            }
            catch (Exception ex)
            {
                GetInstance().Error("Verification.Repository.EfCore|StateZoneRepository|RecordsByPagingAll ", ex.Message);
                return new Tuple<ResponseStatus, IEnumerable<StateZoneModel>>(ResponseStatus.Error, null);
            }
            finally
            {
                GetInstance().Debug(stopwatch, "Verification.Repository.EfCore|StateZoneRepository|RecordsByPagingAll ");
            }
        }

        public async Task<Tuple<ResponseStatus, IEnumerable<StateZoneModel>>> RecordsByPagingFilter(bool IsActive, int take, int skip)
        {
            try
            {
                stopwatch.Start();
                var result = await stateZone.Filter(x => x.IsActive == IsActive, take, skip);
                if (result.Item1)
                {
                    return new Tuple<ResponseStatus, IEnumerable<StateZoneModel>>(ResponseStatus.Success, (List<StateZoneModel>)result.Item2);
                }
                else
                {
                    return new Tuple<ResponseStatus, IEnumerable<StateZoneModel>>(ResponseStatus.Fail, null);
                }
            }
            catch (Exception ex)
            {
                GetInstance().Error("Verification.Repository.EfCore|StateZoneRepository|RecordsByPagingFilter ", ex.Message);
                return new Tuple<ResponseStatus, IEnumerable<StateZoneModel>>(ResponseStatus.Error, null);
            }
            finally
            {
                GetInstance().Trace(stopwatch, "Verification.Repository.EfCore|StateZoneRepository|RecordsByPagingFilter ");
            }
        }

       
    }
}