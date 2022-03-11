
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace OnlineBookStore.Common.Application;

public interface IBaseRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Insert Data type of TEntity in data base
    /// </summary>
    /// <param name="entity">datat will save on the database</param>
    ResultDto<TEntity> Create(TEntity entity);

    /// <summary>
    /// update Data type of TEntity in data base
    /// </summary>
    /// <param name="entity">datat will save on the database</param>
    ResultDto<TEntity> Update(TEntity entity);

    /// <summary>
    /// find the record by id and return that.
    /// </summary>
    /// <param name="Id">uniq identitfier of data.</param>

    ResultDto<TEntity> FindById(object Id);

    /// <summary>
    /// get record by expression and return that record.
    /// </summary>
    /// <param name="expression">the logic for filtering the data will return.</param>
    /// <returns></returns>
    ResultDto<TEntity> FindRecordByCondition(Expression<Func<bool, TEntity>> expression);

    /// <summary>
    /// return all data with out filtering.
    /// </summary>
    /// <param name="count">how many data you need to return</param>
    /// <param name="skip">how many data you need to skip</param>
    /// <returns></returns>
    ListResultDto<TEntity> GetAll(int count, int skip,int page);


    /// <summary>
    /// return list of data type of <typeparamref name="TEntity"/> with condition and pagetion.
    /// </summary>
    /// <param name="count">how many data you need to return</param>
    /// <param name="skip">how many data you need to skip</param>
    /// <param name="expression">condition will filter the returned data.</param>
    /// <returns></returns>
    ListResultDto<TEntity> GetByCondition(int count, int skip,int page, Expression<Func<bool, TEntity>> expression);
}

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{

    private ILogger _logger;
    private DbSet<TEntity> _entity;
    private DbContext _context;
    public BaseRepository(ILogger logger, DbContext dbContext)
    {
        _logger = logger;
        _context = dbContext;
        _entity = dbContext.Set<TEntity>();
    }


    public ResultDto<TEntity> Create(TEntity entity)
    {
        ResultDto<TEntity> result = new();

        try
        {
            _entity.Add(entity);

            var saveresult = _context.SaveChanges();

            if (saveresult > 0)
                return result.Onsuccss(ApplicationMessages.DefaultSucess, entity);
            else
                return result.Onsuccss(ApplicationMessages.NotSavedError, entity);
        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(Create), nameof(TEntity)) + "\n Error Messge : " + ex.Message);
            return result.OnFailer(ApplicationMessages.DefaultFailer, null);
        }
    }

    public ResultDto<TEntity> FindById(object Id)
    {
        var result = new ResultDto<TEntity>();

        try
        {
            return result.Onsuccss(ApplicationMessages.DefaultSucess, _entity.Find(Id));
        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(FindById), nameof(TEntity)) + $"Error Message : {ex.Message}");
            return result.OnFailer(ApplicationMessages.DefaultFailer, null);
        }
    }

    public ListResultDto<TEntity> GetAll(int count, int skip, int page)
    {
        var result = new ListResultDto<TEntity>();

        try
        {
            return result.Onsuccss(ApplicationMessages.DefaultSucess, _entity.AsQueryable().Take(count).Skip(page - 1 * count).ToList());
        }
        catch (Exception ex)
        {
            return result.OnFailer(ApplicationMessages.ExceptionMessage(nameof(GetAll),nameof(TEntity))+$"Error message :{ex.Message}",null);
        }
    }

    public ResultDto<TEntity> FindRecordByCondition(Expression<Func<TEntity, bool>> condition)
    {
        var result = new ResultDto<TEntity>();

        try
        {
            var data = _context.Set<TEntity>().FirstOrDefault(condition);
            return result.Onsuccss(ApplicationMessages.DefaultSucess, data);
        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(FindById), nameof(TEntity)) + $"Error Message : {ex.Message}");
            return result.OnFailer(ApplicationMessages.DefaultFailer, null);
        }
    }

    public ListResultDto<TEntity> GetByCondition(int count, int skip, int page, Expression<Func<TEntity, bool>> expression)
    {
        var result = new ListResultDto<TEntity>();

        try
        {
            var data = _entity.AsQueryable().Where(expression).Take(count).Skip(page - 1 * count).ToList();

            return result.Onsuccss(ApplicationMessages.DefaultSucess, data);
        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(FindById), nameof(TEntity)) + $"Error Message : {ex.Message}");
            return result.OnFailer(ApplicationMessages.DefaultFailer, null);
        }

    }

    public ResultDto<TEntity> Update(TEntity entity)
    {
        ResultDto<TEntity> result = new();

        try
        {
            _entity.Update(entity);

            var saveresult = _context.SaveChanges();

            return result.Onsuccss(ApplicationMessages.DefaultSucess, entity);
        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(Create), nameof(TEntity)) + "\n Error Messge : " + ex.Message);
            return result.OnFailer(ApplicationMessages.DefaultFailer, null);
        }
    }
}
