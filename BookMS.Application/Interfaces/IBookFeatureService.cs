using System.Linq.Expressions;

namespace BookMS.Application.Interfaces;

public interface IBookFeatureService
{
    /// <summary>
    /// Insert Data type of BookFeatures in data base
    /// </summary>
    /// <param name="entity">datat will save on the database</param>
    ResultDto<BookFeature> Create(BookFeature entity);

    /// <summary>
    /// update Data type of BookFeature in data base
    /// </summary>
    /// <param name="entity">datat will save on the database</param>
    ResultDto<BookFeature> Update(BookFeature entity);

    /// <summary>
    /// find the record by id and return that.
    /// </summary>
    /// <param name="Id">uniq identitfier of data.</param>

    ResultDto<BookFeature> FindById(object id);

    /// <summary>
    /// get record by expression and return that record.
    /// </summary>
    /// <param name="expression">the logic for filtering the data will return.</param>
    /// <returns></returns>
    ResultDto<BookFeature> FindRecordByCondition(Expression<Func<BookFeature, bool>> condition);

    /// <summary>
    /// return all data with out filtering.
    /// </summary>
    /// <param name="count">how many data you need to return</param>
    /// <param name="skip">how many data you need to skip</param>
    /// <returns></returns>
    ListResultDto<BookFeature> GetAll(int count, int skip, int page);


    /// <summary>
    /// return list of data type of <typeparamref name="BookFeature"/> with condition and pagetion.
    /// </summary>
    /// <param name="count">how many data you need to return</param>
    /// <param name="skip">how many data you need to skip</param>
    /// <param name="expression">condition will filter the returned data.</param>
    /// <returns></returns>
    ListResultDto<BookFeature> GetByCondition(int count, int skip, int page, Expression<Func<BookFeature, bool>> expression);
}

public class BookFeatureService : IBookFeatureService
{

    private readonly IBookMSDbContext _context;
    private readonly ILogger _logger;

    public BookFeatureService(IBookMSDbContext context, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }

    public ResultDto<BookFeature> Create(BookFeature entity)
    {
        var result = new ResultDto<BookFeature>();

        try
        {
            _context.BookFeatures.Add(entity);

            var saveResult = _context.SaveChanges();


            return saveResult > 0 ? result.Onsuccss(ApplicationMessages.DefaultSucess, entity) : result.OnFailer(ApplicationMessages.DefaultFailer, entity);
        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(Create), nameof(BookFeature)) + $"Error Messge : {ex.Message}");
            return result.OnFailer(ApplicationMessages.DefaultExceptionError, null);
        }
    }

    public ResultDto<BookFeature> FindById(object id)
    {
        var result = new ResultDto<BookFeature>();

        try
        {
            var data = _context.BookFeatures.Find(id);

            return data != null ? result.Onsuccss(ApplicationMessages.DefaultSucess, data) : result.OnFailer(ApplicationMessages.NotFoundError, data);
        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(FindById), nameof(BookFeature)) + $"Error Messge : {ex.Message}");
            return result.OnFailer(ApplicationMessages.DefaultExceptionError, null);
        }
    }

    public ResultDto<BookFeature> FindRecordByCondition(Expression<Func<BookFeature, bool>> condition)
    {
        var result = new ResultDto<BookFeature>();

        try
        {
            var data = _context.BookFeatures.FirstOrDefault(condition);

            return data != null ? result.Onsuccss(ApplicationMessages.DefaultSucess, data) : result.OnFailer(ApplicationMessages.NotFoundError, data);
        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(FindById), nameof(BookFeature)) + $"Error Messge : {ex.Message}");
            return result.OnFailer(ApplicationMessages.DefaultExceptionError, null);
        }
    }

    public ListResultDto<BookFeature> GetAll(int count, int skip, int page)
    {
        var result = new ListResultDto<BookFeature>();

        try
        {
            var data = _context.BookFeatures.AsQueryable().Take(count).Skip(page - 1 * count).ToList();

            return data != null ? result.Onsuccss(ApplicationMessages.DefaultSucess, data) : result.OnFailer(ApplicationMessages.NotFoundError, data);
        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(FindById), nameof(BookFeature)) + $"Error Messge : {ex.Message}");
             return result.OnFailer(ApplicationMessages.DefaultExceptionError, null);
        }
    }

    public ListResultDto<BookFeature> GetByCondition(int count, int skip, int page, Expression<Func<BookFeature, bool>> expression)
    {
        var result = new ListResultDto<BookFeature>();

        try
        {
            var data = _context.BookFeatures.AsQueryable().Where(expression).Take(count).Skip(page - 1 * count).ToList();

            return data != null ? result.Onsuccss(ApplicationMessages.DefaultSucess, data) : result.OnFailer(ApplicationMessages.NotFoundError, data);
        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(FindById), nameof(BookFeature)) + $"Error Messge : {ex.Message}");
            return result.OnFailer(ApplicationMessages.DefaultExceptionError, null);
        }
    }

    public ResultDto<BookFeature> Update(BookFeature entity)
    {
        var result = new ResultDto<BookFeature>();

        try
        {
            _context.BookFeatures.Update(entity);

            var saveResult = _context.SaveChanges();


            return saveResult > 0 ? result.Onsuccss(ApplicationMessages.DefaultSucess, entity) : result.OnFailer(ApplicationMessages.DefaultFailer, entity);
        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(Create), nameof(BookFeature)) + $"Error Messge : {ex.Message}");
            return result.OnFailer(ApplicationMessages.DefaultExceptionError, null);
        }
    }
}
