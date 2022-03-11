namespace OnlineBookStore.Common.Application;

public class ResultDto<TData>
{
    public string Message { get; private set; }
    public TData Data { get; private set; }
    public bool ISuccess { get; private set; }

    public ResultDto<TData> Onsuccss(string message, TData data)
    {
        Message = message;
        Data = data;
        ISuccess = true;
        return this;
    }

    public ResultDto<TData> OnFailer(string message, TData data)
    {
        Message = message;
        Data = data;
        ISuccess = false;
        return this;
    }
}


public class ListResultDto<TData>
{
    public string Message { get; private set; }
    public ICollection<TData> Data { get; private set; }
    public bool ISuccess { get; private set; }

    public ListResultDto<TData> Onsuccss(string message, ICollection<TData> data)
    {
        Message = message;
        Data = data;
        ISuccess = true;
        return this;
    }

    public ListResultDto<TData> OnFailer(string message, ICollection<TData> data)
    {
        Message = message;
        Data = data;
        ISuccess = false;
        return this;
    }
}
