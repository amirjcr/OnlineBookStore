namespace OnlineBookStore.Common.Domain;


public abstract class BaseEntity<TId>
{
    public TId Id { get; private set; }
    public DateTime CretetionDate { get; private set; }
}