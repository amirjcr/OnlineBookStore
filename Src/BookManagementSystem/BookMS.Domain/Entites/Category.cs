
namespace BookMS.Domain.Entites;

public class Category : BaseEntity<int>
{
    public Category(string titel, Category parent,int parentId)
    {
        Titel = titel;
        Parent = parent;
        ParentId = ParentId;
    }


    public Category()
    {

    }
    public string Titel { get; private set; }

    public int ParentId { get; private set; }
    public Category Parent { get; private set; }
    public ICollection<Category> Children { get; private set; }
    public ICollection<Book> Books { get; private set; }
}
