

namespace BookMS.Domain.Entites;

public class BookFeature : BaseEntity<long>
{

    public string Titel { get; private set; }
    public string Value { get; private set; }
    public ICollection<Book> Books { get; private set; }
}
