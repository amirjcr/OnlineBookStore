

using System.Linq.Expressions;
using Serilog;

namespace BookMS.Application.Interfaces;

public interface IBookService
{
    /// <summary>
    /// Insert Data type of Book in data base
    /// </summary>
    /// <param name="entity">datat will save on the database</param>
    ResultDto<Book> Create(Book entity);

    /// <summary>
    /// update Data type of Book in data base
    /// </summary>
    /// <param name="entity">datat will save on the database</param>
    ResultDto<Book> Update(Book entity);

    /// <summary>
    /// find the record by id and return that.
    /// </summary>
    /// <param name="Id">uniq identitfier of data.</param>

    ResultDto<Book> FindById(object id);

    /// <summary>
    /// get record by expression and return that record.
    /// </summary>
    /// <param name="expression">the logic for filtering the data will return.</param>
    /// <returns></returns>
    ResultDto<Book> FindRecordByCondition(Expression<Func<Book, bool>> condition);

    /// <summary>
    /// return all data with out filtering.
    /// </summary>
    /// <param name="count">how many data you need to return</param>
    /// <param name="skip">how many data you need to skip</param>
    /// <returns></returns>
    ListResultDto<Book> GetAll(int count, int skip, int page);


    /// <summary>
    /// return list of data type of <typeparamref name="Book"/> with condition and pagetion.
    /// </summary>
    /// <param name="count">how many data you need to return</param>
    /// <param name="skip">how many data you need to skip</param>
    /// <param name="expression">condition will filter the returned data.</param>
    /// <returns></returns>
    ListResultDto<Book> GetByCondition(int count, int skip, int page, Expression<Func<Book, bool>> expression);

}

public class BookService : IBookService
{

    private readonly IBookMSDbContext _cotnext;
    private readonly ILogger _logger;
    public BookService(IBookMSDbContext context, ILogger logger)
    {
        _cotnext = context;
        _logger = logger;
    }

    public ResultDto<Book> Create(Book entity)
    {
        var result = new ResultDto<Book>();

        try
        {
            _cotnext.Books.Add(entity);
            var saveresult = _cotnext.SaveChanges();

            return saveresult > 0 ? result.Onsuccss(ApplicationMessages.DefaultSucess, entity) : result.OnFailer(ApplicationMessages.DefaultFailer, entity);

        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(Create), nameof(Book)) + "\n Error Messge : " + ex.Message);
            return result.OnFailer(ApplicationMessages.DefaultExceptionError, null);
        }
    }

    public ResultDto<Book> FindById(object id)
    {
        var result = new ResultDto<Book>();

        try
        {
            var data = _cotnext.Books.Find(id);

            return data != null ? result.Onsuccss(ApplicationMessages.DefaultSucess, data) : result.OnFailer(ApplicationMessages.NotFoundError, data);

        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(FindById), nameof(Book)) + "\n Error Messge : " + ex.Message);
            return result.OnFailer(ApplicationMessages.DefaultExceptionError, null);
        }
    }

    public ResultDto<Book> FindRecordByCondition(Expression<Func<Book, bool>> condition)
    {
        var result = new ResultDto<Book>();

        try
        {
            var data = _cotnext.Books.FirstOrDefault(condition);

            return data != null ? result.Onsuccss(ApplicationMessages.DefaultSucess, data) : result.OnFailer(ApplicationMessages.DefaultFailer, data);

        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(FindRecordByCondition), nameof(Book)) + "\n Error Messge : " + ex.Message);
            return result.OnFailer(ApplicationMessages.DefaultExceptionError, null);
        }
    }

    public ListResultDto<Book> GetAll(int count, int skip, int page)
    {
        var result = new ListResultDto<Book>();

        try
        {
            var data = _cotnext.Books.AsQueryable().Take(count).Skip(page - 1 * count).ToList();

            return data != null ? result.Onsuccss(ApplicationMessages.DefaultSucess, data) : result.OnFailer(ApplicationMessages.NotFoundError, data);

        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(GetAll), nameof(Book)) + "\n Error Messge : " + ex.Message);
            return result.OnFailer(ApplicationMessages.DefaultExceptionError, null);
        }
    }

    public ListResultDto<Book> GetByCondition(int count, int skip, int page, Expression<Func<Book, bool>> expression)
    {
        var result = new ListResultDto<Book>();

        try
        {
            var data = _cotnext.Books.AsQueryable().Where(expression).Take(count).Skip(page - 1 * count).ToList();

            return data != null ? result.Onsuccss(ApplicationMessages.DefaultSucess, data) : result.OnFailer(ApplicationMessages.NotFoundError, data);

        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(GetByCondition), nameof(Book)) + "\n Error Messge : " + ex.Message);
            return result.OnFailer(ApplicationMessages.DefaultExceptionError, null);
        }
    }

    public ResultDto<Book> Update(Book entity)
    {
        var result = new ResultDto<Book>();

        try
        {
            _cotnext.Books.Update(entity);
            var saveresult = _cotnext.SaveChanges();

            return saveresult > 0 ? result.Onsuccss(ApplicationMessages.DefaultSucess, entity) : result.OnFailer(ApplicationMessages.DefaultFailer, entity);

        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(Update), nameof(Book)) + "\n Error Messge : " + ex.Message);
            return result.OnFailer(ApplicationMessages.DefaultExceptionError, null);
        }
    }
}
