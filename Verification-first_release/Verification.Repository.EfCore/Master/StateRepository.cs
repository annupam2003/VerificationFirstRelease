using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verification.Data.EfCore;
using Verification.Data.EfCore.BaseEfCore;
using Verification.Data.EfCore.Master;
using Verification.EntityModel;
using Verification.Mapper;
using Verification.DomainModel;
using Verification.Tracker;
using static Verification.Tracker.GlobleEnums;
using static Verification.Tracker.Track;

namespace Verification.Repository.EfCore.Master
{
    public class StateRepository : IStateRepository
    {
        private readonly StateEfCore state;
        Stopwatch stopwatch;

        public StateRepository(VerificationDbContext verification, IEntityMapper<State, StateModel> mapper)
        {
            state = new StateEfCore(verification, mapper);
            stopwatch = new Stopwatch();
        }

        public async Task<Tuple<GlobleEnums.ResponseStatus, dynamic>> Create(StateModel model)
        {
            try
            {
                stopwatch.Start();

                var IsExists = await state.Count(x => x.Name.Trim().ToUpper().Equals(model.Name.Trim().ToUpper()));
                if (IsExists == 0)
                {
                    var result = await state.InsertSingle(model);
                    if (result.Item1)
                    {
                        return new Tuple<ResponseStatus, dynamic>(ResponseStatus.Success, ((State)result.Item2).Id);
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
                GetInstance().Error("Verification.Repository.EfCore|StateRepository|Create ", ex.Message);
                return new Tuple<ResponseStatus, dynamic>(ResponseStatus.Error, ex.Message);
            }
            finally
            {
                GetInstance().Debug(stopwatch, "Verification.Repository.EfCore|StateRepository|Create ");
            }
        }

        public async Task<Tuple<GlobleEnums.ResponseStatus, dynamic>> Update(int Id, StateModel model)
        {
            try
            {
                stopwatch.Start();
                var IsExists = await state.Count(x => x.Name.Trim().ToUpper().Equals(model.Name.Trim().ToUpper()));
                if (IsExists == 0)
                {

                    var result = await state.EditSingle(Id, model);
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
                GetInstance().Error("Verification.Repository.EfCore|StateRepository|Update ", ex.Message);
                return new Tuple<ResponseStatus, dynamic>(ResponseStatus.Error, ex.Message);
            }
            finally
            {
                GetInstance().Debug(stopwatch, "Verification.Repository.EfCore|StateRepository|Update ");
            }
        }

        public async Task<Tuple<GlobleEnums.ResponseStatus, dynamic>> Delete(int Id)
        {
            try
            {
                stopwatch.Start();
                var result = await state.RemoveSingle(Id);
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
                GetInstance().Error("Verification.Repository.EfCore|StateRepository|Delete ", ex.Message);
                return new Tuple<ResponseStatus, dynamic>(ResponseStatus.Error, ex.Message);
            }
            finally
            {
                GetInstance().Debug(stopwatch, "Verification.Repository.EfCore|StateRepository|Delete ");
            }
        }

        public async Task<int> Count(bool? IsActive)
        {
            try
            {
                stopwatch.Start();
                return await state.Count(IsActive == null ? null : x => x.IsActive == IsActive);
            }
            catch (Exception ex)
            {
                GetInstance().Error("Verification.Repository.EfCore|StateRepository|Count ", ex.Message);
                return -1;
            }
            finally
            {
                GetInstance().Debug(stopwatch, "Verification.Repository.EfCore|StateRepository|Count ");
            }
        }

        public async Task<Tuple<GlobleEnums.ResponseStatus, IEnumerable<StateModel>>> Records(bool? IsActive)
        {
            try
            {
                stopwatch.Start();
                var result = await state.Filter(IsActive == null ? null : x => x.IsActive == IsActive);
                if (result.Item1)
                {
                    return new Tuple<ResponseStatus, IEnumerable<StateModel>>(ResponseStatus.Success, (List<StateModel>)result.Item2);
                }
                else
                {
                    return new Tuple<ResponseStatus, IEnumerable<StateModel>>(ResponseStatus.Fail, null);
                }

            }
            catch (Exception ex)
            {
                GetInstance().Error("Verification.Repository.EfCore|StateRepository|Count ", ex.Message);
                return new Tuple<ResponseStatus, IEnumerable<StateModel>>(ResponseStatus.Error, null);
            }
            finally
            {
                GetInstance().Debug(stopwatch, "Verification.Repository.EfCore|StateRepository|Count ");
            }
        }

        public async Task<Tuple<GlobleEnums.ResponseStatus, StateModel>> RecordById(int Id)
        {
            try
            {

                stopwatch.Start();
                var result = await state.Filter(x => x.Id == Id);
                if (result.Item1)
                    return new Tuple<ResponseStatus, StateModel>(ResponseStatus.Success, ((List<StateModel>)result.Item2).FirstOrDefault());
                return new Tuple<ResponseStatus, StateModel>(ResponseStatus.Fail, null);

            }
            catch (Exception ex)
            {
                GetInstance().Error("Verification.Repository.EfCore|StateRepository|Count ", ex.Message);
                return new Tuple<ResponseStatus, StateModel>(ResponseStatus.Error, null);
            }
            finally
            {
                GetInstance().Debug(stopwatch, "Verification.Repository.EfCore|StateRepository|Count ");
            }
        }

        public async Task<Tuple<GlobleEnums.ResponseStatus, IEnumerable<StateModel>>> RecordsByPagingAll(int take, int skip)
        {
            try
            {
                stopwatch.Start();
                var result = await state.Filter(null, take, skip);
                if (result.Item1)
                {
                    return new Tuple<ResponseStatus, IEnumerable<StateModel>>(ResponseStatus.Success, (List<StateModel>)result.Item2);
                }
                else
                {
                    return new Tuple<ResponseStatus, IEnumerable<StateModel>>(ResponseStatus.Fail, null);
                }
            }
            catch (Exception ex)
            {
                GetInstance().Error("Verification.Repository.EfCore|StateRepository|RecordsByPagingAll ", ex.Message);
                return new Tuple<ResponseStatus, IEnumerable<StateModel>>(ResponseStatus.Error, null);
            }
            finally
            {
                GetInstance().Debug(stopwatch, "Verification.Repository.EfCore|StateRepository|RecordsByPagingAll ");
            }
        }

        public async Task<Tuple<GlobleEnums.ResponseStatus, IEnumerable<StateModel>>> RecordsByPagingFilter(bool IsActive, int take, int skip)
        {
            try
            {
                stopwatch.Start();
                var result = await state.Filter(x => x.IsActive == IsActive, take, skip);
                if (result.Item1)
                {
                    return new Tuple<ResponseStatus, IEnumerable<StateModel>>(ResponseStatus.Success, (List<StateModel>)result.Item2);
                }
                else
                {
                    return new Tuple<ResponseStatus, IEnumerable<StateModel>>(ResponseStatus.Fail, null);
                }
            }
            catch (Exception ex)
            {
                GetInstance().Error("Verification.Repository.EfCore|StateRepository|RecordsByPagingFilter ", ex.Message);
                return new Tuple<ResponseStatus, IEnumerable<StateModel>>(ResponseStatus.Error, null);
            }
            finally
            {
                GetInstance().Debug(stopwatch, "Verification.Repository.EfCore|StateRepository|RecordsByPagingFilter ");
            }
        }
    }
}
