using System.Linq.Expressions;
using BookMS.Application.Interfaces;

namespace BookTypeMS.Application.Interfaces;

public interface IBookTypeService
{

    /// <summary>
    /// Insert Data type of BookType in data base
    /// </summary>
    /// <param name="entity">datat will save on the database</param>
    ResultDto<BookType> Create(BookType entity);

    /// <summary>
    /// update Data type of BookType in data base
    /// </summary>
    /// <param name="entity">datat will save on the database</param>
    ResultDto<BookType> Update(BookType entity);

    /// <summary>
    /// find the record by id and return that.
    /// </summary>
    /// <param name="Id">uniq identitfier of data.</param>

    ResultDto<BookType> FindById(object id);

    /// <summary>
    /// get record by expression and return that record.
    /// </summary>
    /// <param name="expression">the logic for filtering the data will return.</param>
    /// <returns></returns>
    ResultDto<BookType> FindRecordByCondition(Expression<Func<BookType, bool>> condition);

    /// <summary>
    /// return all data with out filtering.
    /// </summary>
    /// <param name="count">how many data you need to return</param>
    /// <param name="skip">how many data you need to skip</param>
    /// <returns></returns>
    ListResultDto<BookType> GetAll(int count, int skip, int page);


    /// <summary>
    /// return list of data type of <typeparamref name="BookType"/> with condition and pagetion.
    /// </summary>
    /// <param name="count">how many data you need to return</param>
    /// <param name="skip">how many data you need to skip</param>
    /// <param name="expression">condition will filter the returned data.</param>
    /// <returns></returns>
    ListResultDto<BookType> GetByCondition(int count, int skip, int page, Expression<Func<BookType, bool>> expression);
}

internal class BookTypeService : IBookTypeService
{

    private readonly IBookMSDbContext _context;
    private readonly ILogger _logger;
    public BookTypeService(ILogger logger, IBookMSDbContext context)
    {
        _context = context;
        _logger = logger;
    }

    public ResultDto<BookType> Create(BookType entity)
    {
        var result = new ResultDto<BookType>();

        try
        {
            _context.BookTypes.Add(entity);
            var saveresult = _context.SaveChanges();

            return saveresult > 0 ? result.Onsuccss(ApplicationMessages.DefaultSucess, entity) : result.OnFailer(ApplicationMessages.DefaultFailer, entity);
        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(Create), nameof(BookType)) + $"Error Message :{ex.Message}");
            return result.OnFailer(ApplicationMessages.DefaultExceptionError, null);
        }
    }

    public ResultDto<BookType> FindById(object id)
    {
        var result = new ResultDto<BookType>();

        try
        {
            var data = _context.BookTypes.Find(id);

            return data != null ? result.Onsuccss(ApplicationMessages.DefaultSucess, data) : result.OnFailer(ApplicationMessages.DefaultFailer, data);
        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(FindById), nameof(BookType)) + $"Error Message :{ex.Message}");
            return result.OnFailer(ApplicationMessages.DefaultExceptionError, null);
        }
    }

    public ResultDto<BookType> FindRecordByCondition(Expression<Func<BookType, bool>> condition)
    {
        var result = new ResultDto<BookType>();

        try
        {
            var data = _context.BookTypes.FirstOrDefault(condition);
            return data != null ? result.Onsuccss(ApplicationMessages.DefaultSucess, data) : result.OnFailer(ApplicationMessages.DefaultFailer, data);
        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(FindRecordByCondition), nameof(BookType)) + $"Error Message :{ex.Message}");
            return result.OnFailer(ApplicationMessages.DefaultExceptionError, null);
        }
    }

    public ListResultDto<BookType> GetAll(int count, int skip, int page)
    {
        var result = new ListResultDto<BookType>();

        try
        {
            var data = _context.BookTypes.AsQueryable().Take(count).Skip(page - 1 * count).ToList();
            return data != null ? result.Onsuccss(ApplicationMessages.DefaultSucess, data) : result.OnFailer(ApplicationMessages.DefaultFailer, data);
        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(GetAll), nameof(BookType)) + $"Error Message :{ex.Message}");
            return result.OnFailer(ApplicationMessages.DefaultExceptionError, null);
        }
    }

    public ListResultDto<BookType> GetByCondition(int count, int skip, int page, Expression<Func<BookType, bool>> expression)
    {
        var result = new ListResultDto<BookType>();

        try
        {
            var data = _context.BookTypes.AsQueryable().Take(count).Skip(page - 1 * count).Where(expression).ToList();
            return data != null ? result.Onsuccss(ApplicationMessages.DefaultSucess, data) : result.OnFailer(ApplicationMessages.DefaultFailer, data);
        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(GetByCondition), nameof(BookType)) + $"Error Message :{ex.Message}");
            return result.OnFailer(ApplicationMessages.DefaultExceptionError, null);
        }
    }

    public ResultDto<BookType> Update(BookType entity)
    {
        var result = new ResultDto<BookType>();

        try
        {
            _context.BookTypes.Update(entity);
            var saveresult = _context.SaveChanges();

            return saveresult > 0 ? result.Onsuccss(ApplicationMessages.DefaultSucess, entity) : result.OnFailer(ApplicationMessages.DefaultFailer, entity);
        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(Update), nameof(BookType)) + $"Error Message :{ex.Message}");
            return result.OnFailer(ApplicationMessages.DefaultExceptionError, null);
        }
    }
}
