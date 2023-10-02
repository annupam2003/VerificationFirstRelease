using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Verification.Mapper;
using static Verification.Tracker.Track;
using System.Reflection;

namespace Verification.Data.EfCore.BaseEfCore
{
    public abstract class CurdOperation<E, M> : ICurdOperation<E, M> where E : class where M : class
    {
        #region "Fields"
        private readonly Stopwatch stopwatch;
        private readonly VerificationDbContext dbContext;
        private readonly IEntityMapper<E, M> mapper;
        private DbSet<E> objEntity;
        #endregion

        #region "Construtor"
        public CurdOperation(VerificationDbContext verification, IEntityMapper<E, M> mapper)
        {
            this.dbContext = verification;
            objEntity = verification.Set<E>();

            this.mapper = mapper;
            stopwatch = new Stopwatch();
        }
        #endregion

        #region "Create"
        //Create New Record
        public virtual async Task<Tuple<bool, dynamic>> InsertSingle(M model)
        {
            try
            {
                stopwatch.Start();
                E entity = mapper.mapToEntity(model);
                await objEntity.AddAsync(entity);
                var result = await dbContext.SaveChangesAsync();
                return new Tuple<bool, dynamic>(true, entity);
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
        //Create New Record
        public virtual async Task<Tuple<bool, dynamic>> InsertBulk(List<M> Models)
        {
            List<E> entitys = new List<E>();
            try
            {
                stopwatch.Start();
                Models.ForEach(async delegate (M model)
                {
                    entitys.Add(mapper.mapToEntity(model));
                });
                await objEntity.AddRangeAsync(entitys);
                var result = await dbContext.SaveChangesAsync();
                return new Tuple<bool, dynamic>(true, entitys);
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
        public virtual async Task<Tuple<bool, dynamic>> EditSingle(int Id, M model)
        {
            try
            {
                stopwatch.Start();

                var state = await objEntity.FindAsync(Id);
                if (state != null)
                {
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
        //Update Existing Record
        public virtual async Task<Tuple<bool, dynamic>> EditBulk(IDictionary<int, M> models)
        {
            List<int> Id = new List<int>();
            try
            {
                stopwatch.Start();
                foreach (KeyValuePair<int, M> m in models)
                {
                    var entity = await objEntity.FindAsync(m.Key);
                    if (entity != null)
                    {
                        mapper.mapToEntity(m.Value, entity);
                        await dbContext.SaveChangesAsync();
                        Id.Add(m.Key);
                    }
                }
                return new Tuple<bool, dynamic>(true, Id);
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

        #region "Delete"
        //Delete Existing Record
        public virtual async Task<Tuple<bool, dynamic>> RemoveSingle(int Id)
        {
            try
            {
                stopwatch.Start();
                E mEntity = await objEntity.FindAsync(Id);
                if (mEntity != null)
                {
                    PropertyInfo propInfo = mEntity.GetType().GetProperty("IsActive");
                    propInfo.SetValue(mEntity, false, null);

                    var result = await dbContext.SaveChangesAsync();
                    return new Tuple<bool, dynamic>(true, result);
                }
                return new Tuple<bool, dynamic>(false, "Record Not Found");
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
        //Delete Existing Record
        public virtual async Task<Tuple<bool, dynamic>> RemoveBulk(List<int> Id)
        {
            List<int> Idx = new List<int>();
            try
            {
                stopwatch.Start();
                foreach (int i in Id)
                {
                    E mEntity = await objEntity.FindAsync(Id);
                    if (mEntity != null)
                    {
                        PropertyInfo propInfo = mEntity.GetType().GetProperty("IsActive");
                        propInfo.SetValue(mEntity, false, null);

                        await dbContext.SaveChangesAsync();
                        Idx.Add(i);

                    }
                }
                return new Tuple<bool, dynamic>(true, Idx);
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

        #region "Read"
        public virtual async Task<Tuple<bool, IEnumerable<M>>> Filter(Expression<Func<E, bool>>? predicate, int Tk = 0, int Sk = 0)
        {
            try
            {
                stopwatch.Start();
                if (Tk >= Sk)
                {
                    if (predicate == null)
                    {
                        if (Tk == 0)
                        {
                            return new Tuple<bool, IEnumerable<M>>(true, await objEntity.Select(x => mapper.mapFromEntity(x)).ToListAsync());
                        }
                        return new Tuple<bool, IEnumerable<M>>(true, await objEntity.Select(x => mapper.mapFromEntity(x)).Take(Tk).Skip(Sk).ToListAsync());
                    }
                    else
                    {
                        if (Tk == 0)
                        {
                            return new Tuple<bool, IEnumerable<M>>(true, await objEntity.Where(predicate).Select(x => mapper.mapFromEntity(x)).ToListAsync());
                        }
                        return new Tuple<bool, IEnumerable<M>>(true, await objEntity.Where(predicate).Select(x => mapper.mapFromEntity(x)).Take(Tk).Skip(Sk).ToListAsync());
                    }
                }
                return new Tuple<bool, IEnumerable<M>>(false, null);
            }
            catch (Exception ex)
            {
                GetInstance().Error(ex.Message);
                return new Tuple<bool, IEnumerable<M>>(false, null);
            }

            finally
            {
                GetInstance().Debug(stopwatch, "debug");
            }
        }
        #endregion

        #region "Count"
        public virtual async Task<int> Count(Expression<Func<E, bool>>? predicate)
        {
            try
            {
                stopwatch.Start();
                return predicate == null ? await objEntity.CountAsync() : await objEntity.CountAsync(predicate);
            }
            catch (Exception ex)
            {
                GetInstance().Error(ex.Message);
                return -1;
            }
            finally
            {
                GetInstance().Debug(stopwatch, "debug");
            }
        }
        #endregion

    }
}
