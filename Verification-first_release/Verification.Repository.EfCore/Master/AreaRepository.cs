using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
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
    public class AreaRepository : IAreaRepository
    {
        private readonly AreaEfCore area;
        Stopwatch stopwatch;
        public AreaRepository(VerificationDbContext verification, IEntityMapper<Area, AreaModel> mapper)
        {
            area = new AreaEfCore(verification, mapper);
            stopwatch = new Stopwatch();
        }


        public async Task<Tuple<GlobleEnums.ResponseStatus, dynamic>> Create(AreaModel model)
        {
            try
            {
                stopwatch.Start();

                var IsExists = await area.Count(x => x.Name.Trim().ToUpper().Equals(model.Name.Trim().ToUpper()) && x.Pincode.Trim().Equals(model.Pincode.Trim()));
                if (IsExists == 0)
                {
                    var result = await area.InsertSingle(model);
                    if (result.Item1)
                    {
                        return new Tuple<ResponseStatus, dynamic>(ResponseStatus.Success, ((Area)result.Item2).Id);
                    }
                    else
                    {
                        return new Tuple<ResponseStatus, dynamic>(ResponseStatus.Fail, result.Item2);
                    }
                }
                else
                {
                    return new Tuple<ResponseStatus, dynamic>(ResponseStatus.Exists, "Name Already Exists given Pincode");
                }
            }
            catch (Exception ex)
            {
                GetInstance().Error("Verification.Repository.EfCore|areaRepository|Create ", ex.Message);
                return new Tuple<ResponseStatus, dynamic>(ResponseStatus.Error, ex.Message);
            }
            finally
            {
                GetInstance().Debug(stopwatch, "Verification.Repository.EfCore|AreaRepository|Create ");
            }
        }

        public async Task<Tuple<GlobleEnums.ResponseStatus, dynamic>> Update(int Id, AreaModel model)
        {
            try
            {
                stopwatch.Start();
                var IsExists = await area.Count(x => x.Name.Trim().ToUpper().Equals(model.Name.Trim().ToUpper()));
                if (IsExists == 0)
                {

                    var result = await area.EditSingle(Id, model);
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
                GetInstance().Error("Verification.Repository.EfCore|AreaRepository|Update ", ex.Message);
                return new Tuple<ResponseStatus, dynamic>(ResponseStatus.Error, ex.Message);
            }
            finally
            {
                GetInstance().Debug(stopwatch, "Verification.Repository.EfCore|AreaRepository|Update ");
            }
        }

        public async Task<Tuple<GlobleEnums.ResponseStatus, dynamic>> Delete(int Id)
        {
            try
            {
                stopwatch.Start();
                var result = await area.RemoveSingle(Id);
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
                GetInstance().Error("Verification.Repository.EfCore|AreaRepository|Delete ", ex.Message);
                return new Tuple<ResponseStatus, dynamic>(ResponseStatus.Error, ex.Message);
            }
            finally
            {
                GetInstance().Debug(stopwatch, "Verification.Repository.EfCore|AreaRepository|Delete ");
            }
        }

        public async Task<int> Count(bool? IsActive)
        {
            try
            {
                stopwatch.Start();
                return await area.Count(IsActive == null ? null : x => x.IsActive == IsActive);
            }
            catch (Exception ex)
            {
                GetInstance().Error("Verification.Repository.EfCore|AreaRepository|Count ", ex.Message);
                return -1;
            }
            finally
            {
                GetInstance().Debug(stopwatch, "Verification.Repository.EfCore|AreaRepository|Count ");
            }

        }
        public async Task<Tuple<GlobleEnums.ResponseStatus, IEnumerable<AreaModel>>> Records(bool? IsActive)
        {
            try
            {
                stopwatch.Start();
                var result = await area.Filter(IsActive == null ? null : x => x.IsActive == IsActive);
                if (result.Item1)
                {
                    return new Tuple<ResponseStatus, IEnumerable<AreaModel>>(ResponseStatus.Success, (List<AreaModel>)result.Item2);
                }
                else
                {
                    return new Tuple<ResponseStatus, IEnumerable<AreaModel>>(ResponseStatus.Fail, null);
                }

            }
            catch (Exception ex)
            {
                GetInstance().Error("Verification.Repository.EfCore|AreaRepository|Records ", ex.Message);
                return null;
            }
            finally
            {
                GetInstance().Debug(stopwatch, "Verification.Repository.EfCore|AreaRepository|Records ");
            }
        }

        public async Task<Tuple<GlobleEnums.ResponseStatus, AreaModel>> RecordById(int Id)
        {
            try
            {
                stopwatch.Start();
                var result = await area.Filter(x => x.Id == Id);
                if (result.Item1)
                    return new Tuple<ResponseStatus, AreaModel>(ResponseStatus.Success, ((List<AreaModel>)result.Item2).FirstOrDefault());
                return new Tuple<ResponseStatus, AreaModel>(ResponseStatus.Fail, null);
            }
            catch (Exception ex)
            {
                GetInstance().Error("Verification.Repository.EfCore|AreaRepository|RecordById ", ex.Message);
                return null;
            }
            finally
            {
                GetInstance().Debug(stopwatch, "Verification.Repository.EfCore|AreaRepository|RecordById ");
            }
        }

        public async Task<Tuple<GlobleEnums.ResponseStatus, IEnumerable<AreaModel>>> RecordsByPagingAll(int take, int skip)
        {
            try
            {
                stopwatch.Start();
                var result = await area.Filter(null, take, skip);
                if (result.Item1)
                {
                    return new Tuple<ResponseStatus, IEnumerable<AreaModel>>(ResponseStatus.Success, (List<AreaModel>)result.Item2);
                }
                else
                {
                    return new Tuple<ResponseStatus, IEnumerable<AreaModel>>(ResponseStatus.Fail, null);
                }
            }
            catch (Exception ex)
            {
                GetInstance().Error("Verification.Repository.EfCore|AreaRepository|RecordsByPagingAll ", ex.Message);
                return null;
            }
            finally
            {
                GetInstance().Debug(stopwatch, "Verification.Repository.EfCore|AreaRepository|RecordsByPagingAll ");
            }
        }

        public async Task<Tuple<GlobleEnums.ResponseStatus, IEnumerable<AreaModel>>> RecordsByPagingFilter(bool IsActive, int take, int skip)
        {
            try
            {
                stopwatch.Start();
                var result = await area.Filter(x => x.IsActive == IsActive, take, skip);
                if (result.Item1)
                {
                    return new Tuple<ResponseStatus, IEnumerable<AreaModel>>(ResponseStatus.Success, (List<AreaModel>)result.Item2);
                }
                else
                {
                    return new Tuple<ResponseStatus, IEnumerable<AreaModel>>(ResponseStatus.Fail, null);
                }
            }
            catch (Exception ex)
            {
                GetInstance().Error("Verification.Repository.EfCore|AreaRepository|RecordsByPagingFilter ", ex.Message);
                return null;
            }
            finally
            {
                GetInstance().Debug(stopwatch, "Verification.Repository.EfCore|AreaRepository|RecordsByPagingFilter ");
            }
        }
    }
}
