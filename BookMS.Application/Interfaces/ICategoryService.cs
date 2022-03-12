using System.Linq.Expressions;

namespace BookMS.Application.Interfaces;

public interface ICategoryService
{
    /// <summary>
    /// Insert Data type of Category in data base
    /// </summary>
    /// <param name="entity">datat will save on the database</param>
    ResultDto<Category> Create(Category entity);

    /// <summary>
    /// update Data type of Category in data base
    /// </summary>
    /// <param name="entity">datat will save on the database</param>
    ResultDto<Category> Update(Category entity);

    /// <summary>
    /// find the record by id and return that.
    /// </summary>
    /// <param name="Id">uniq identitfier of data.</param>

    ResultDto<Category> FindById(object id);

    /// <summary>
    /// get record by expression and return that record.
    /// </summary>
    /// <param name="expression">the logic for filtering the data will return.</param>
    /// <returns></returns>
    ResultDto<Category> FindRecordByCondition(Expression<Func<Category, bool>> condition);

    /// <summary>
    /// return all data with out filtering.
    /// </summary>
    /// <param name="count">how many data you need to return</param>
    /// <param name="skip">how many data you need to skip</param>
    /// <returns></returns>
    ListResultDto<Category> GetAll(int count, int skip, int page);

    /// <summary>
    /// return list of data type of <typeparamref name="Category"/> with condition and pagetion.
    /// </summary>
    /// <param name="count">how many data you need to return</param>
    /// <param name="skip">how many data you need to skip</param>
    /// <param name="expression">condition will filter the returned data.</param>
    /// <returns></returns>
    ListResultDto<Category> GetByCondition(int count, int skip, int page, Expression<Func<Category, bool>> expression);
}

public class CategoryService : ICategoryService
{
    private readonly ILogger _logger;
    private readonly IBookMSDbContext _context;
    public CategoryService(ILogger logger, IBookMSDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public ResultDto<Category> Create(Category entity)
    {
        var result = new ResultDto<Category>();

        try
        {
            _context.Categories.Add(entity);

            var saveresult = _context.SaveChanges();

            return saveresult > 0 ? result.Onsuccss(ApplicationMessages.DefaultSucess, entity) : result.OnFailer(ApplicationMessages.DefaultFailer, entity);
        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(Create), nameof(Category)) + $"Error Messge :{ex.Message}");
            return result.OnFailer(ApplicationMessages.DefaultExceptionError, null);
        }
    }

    public ResultDto<Category> FindById(object id)
    {
        var result = new ResultDto<Category>();

        try
        {
            var data = _context.Categories.Find(id);

            return data != null ? result.Onsuccss(ApplicationMessages.DefaultSucess, data) : result.OnFailer(ApplicationMessages.DefaultFailer, data);
        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(FindById), nameof(Category)) + $"Error Messge :{ex.Message}");
            return result.OnFailer(ApplicationMessages.DefaultExceptionError, null);
        }
    }

    public ResultDto<Category> FindRecordByCondition(Expression<Func<Category, bool>> condition)
    {
        var result = new ResultDto<Category>();

        try
        {
            var data = _context.Categories.AsQueryable().FirstOrDefault(condition);

            return data != null ? result.Onsuccss(ApplicationMessages.DefaultSucess, data) : result.OnFailer(ApplicationMessages.DefaultFailer, data);
        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(FindById), nameof(Category)) + $"Error Messge :{ex.Message}");
            return result.OnFailer(ApplicationMessages.DefaultExceptionError, null);
        }
    }

    public ListResultDto<Category> GetAll(int count, int skip, int page)
    {
        var result = new ListResultDto<Category>();

        try
        {
            var data = _context.Categories.AsQueryable().Take(count).Skip(page - 1 * count).ToList();

            return data != null ? result.Onsuccss(ApplicationMessages.DefaultSucess, data) : result.OnFailer(ApplicationMessages.DefaultFailer, data);
        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(GetAll), nameof(Category)) + $"Error Messge :{ex.Message}");
            return result.OnFailer(ApplicationMessages.DefaultExceptionError, null);
        }
    }

    public ListResultDto<Category> GetByCondition(int count, int skip, int page, Expression<Func<Category, bool>> expression)
    {
        var result = new ListResultDto<Category>();

        try
        {
            var data = _context.Categories.AsQueryable().Take(count).Skip(page - 1 * count).Where(expression).ToList();

            return data != null ? result.Onsuccss(ApplicationMessages.DefaultSucess, data) : result.OnFailer(ApplicationMessages.DefaultFailer, data);
        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(GetByCondition), nameof(Category)) + $"Error Messge :{ex.Message}");
            return result.OnFailer(ApplicationMessages.DefaultExceptionError, null);
        }
    }

    public ResultDto<Category> Update(Category entity)
    {
        var result = new ResultDto<Category>();

        try
        {
            _context.Categories.Update(entity);

            var saveresult = _context.SaveChanges();

            return saveresult > 0 ? result.Onsuccss(ApplicationMessages.DefaultSucess, entity) : result.OnFailer(ApplicationMessages.DefaultFailer, entity);
        }
        catch (Exception ex)
        {
            _logger.Error(ApplicationMessages.ExceptionMessage(nameof(Update), nameof(Category)) + $"Error Messge :{ex.Message}");
            return result.OnFailer(ApplicationMessages.DefaultExceptionError, null);
        }
    }
}
