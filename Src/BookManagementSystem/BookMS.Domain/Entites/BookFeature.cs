

namespace BookMS.Domain.Entites;

public class BookFeature : BaseEntity<long>
{

    public BookFeature()
    {

    }
    public BookFeature(string titel, string value, long bookId, Book book)
    {
        Titel = titel;
        Value = value;
        BookId = bookId;
        Book = book;
    }

    public string Titel { get; private set; }
    public string Value { get; private set; }


    public long BookId { get; private set; }
    public Book Book { get; private set;}
}
