using System.Linq.Expressions;

namespace BookMS.Application.Interfaces;

public interface IBookImagesService
{
    /// <summary>
    /// Insert Data type of BookImages in data base
    /// </summary>
    /// <param name="entity">datat will save on the database</param>
    ResultDto<BookImages> Create(BookImages entity);

    /// <summary>
    /// update Data type of BookImages in data base
    /// </summary>
    /// <param name="entity">datat will save on the database</param>
    ResultDto<BookImages> Update(BookImages entity);

    /// <summary>
    /// find the record by id and return that.
    /// </summary>
    /// <param name="Id">uniq identitfier of data.</param>

    ResultDto<BookImages> FindById(object id);

    /// <summary>
    /// get record by expression and return that record.
    /// </summary>
    /// <param name="expression">the logic for filtering the data will return.</param>
    /// <returns></returns>
    ResultDto<BookImages> FindRecordByCondition(Expression<Func<BookImages, bool>> condition);

    /// <summary>
    /// return all data with out filtering.
    /// </summary>
    /// <param name="count">how many data you need to return</param>
    /// <param name="skip">how many data you need to skip</param>
    /// <returns></returns>
    ListResultDto<BookImages> GetAll(int count, int skip, int page);


    /// <summary>
    /// return list of data type of <typeparamref name="BookImages"/> with condition and pagetion.
    /// </summary>
    /// <param name="count">how many data you need to return</param>
    /// <param name="skip">how many data you need to skip</param>
    /// <param name="expression">condition will filter the returned data.</param>
    /// <returns></returns>
    ListResultDto<BookImages> GetByCondition(int count, int skip, int page, Expression<Func<BookImages, bool>> expression);
}

public class BookImageService : IBookImagesService
{

    private readonly ILogger _logger;
    private readonly DbSet<BookImages> _entity;
    private readonly IBookMSDbContext _context;
    public BookImageService(ILogger logger, IBookMSDbContext dbContext)
    {
        _logger = logger;
        _context = dbContext;
        _entity = dbContext.BookImages;
    }


    public ResultDto<BookImages> Create(BookImages entity)
    {
        ResultDto<BookImages> result = new();

        try
        {
            _entity.Add(entity);

            var saveresult = _context.SaveChanges();


            return saveresult > 0 ? result.Onsuccss(ApplicationMessages.DefaultSucess, entity) : result.Onsuccss(ApplicationMessages.NotSavedError, entity);

        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(Create), nameof(BookImages)) + "\n Error Messge : " + ex.Message);
            return result.OnFailer(ApplicationMessages.DefaultExceptionError, null);
        }
    }

    public ResultDto<BookImages> FindById(object id)
    {
        var result = new ResultDto<BookImages>();

        try
        {
            return result.Onsuccss(ApplicationMessages.DefaultSucess, _entity.Find(id));
        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(FindById), nameof(BookImages)) + $"Error Message : {ex.Message}");
            return result.OnFailer(ApplicationMessages.DefaultFailer, null);
        }
    }

    public ListResultDto<BookImages> GetAll(int count, int skip, int page)
    {
        var result = new ListResultDto<BookImages>();

        try
        {
            return result.Onsuccss(ApplicationMessages.DefaultSucess, _entity.AsQueryable().Take(count).Skip(page - 1 * count).ToList());
        }
        catch (Exception ex)
        {
            return result.OnFailer(ApplicationMessages.ExceptionMessage(nameof(GetAll), nameof(BookImages)) + $"Error message :{ex.Message}", null);
        }
    }

    public ResultDto<BookImages> FindRecordByCondition(Expression<Func<BookImages, bool>> condition)
    {
        var result = new ResultDto<BookImages>();

        try
        {
            var data = _context.BookImages.FirstOrDefault(condition);
            return result.Onsuccss(ApplicationMessages.DefaultSucess, data);
        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(FindById), nameof(BookImages)) + $"Error Message : {ex.Message}");
            return result.OnFailer(ApplicationMessages.DefaultFailer, null);
        }
    }

    public ListResultDto<BookImages> GetByCondition(int count, int skip, int page, Expression<Func<BookImages, bool>> expression)
    {
        var result = new ListResultDto<BookImages>();

        try
        {
            var data = _entity.AsQueryable().Where(expression).Take(count).Skip(page - 1 * count).ToList();

            return result.Onsuccss(ApplicationMessages.DefaultSucess, data);
        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(FindById), nameof(BookImages)) + $"Error Message : {ex.Message}");
            return result.OnFailer(ApplicationMessages.DefaultFailer, null);
        }

    }

    public ResultDto<BookImages> Update(BookImages entity)
    {
        ResultDto<BookImages> result = new();

        try
        {
            _entity.Update(entity);

            var saveresult = _context.SaveChanges();

            return result.Onsuccss(ApplicationMessages.DefaultSucess, entity);
        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(Create), nameof(BookImages)) + "\n Error Messge : " + ex.Message);
            return result.OnFailer(ApplicationMessages.DefaultFailer, null);
        }
    }
}
