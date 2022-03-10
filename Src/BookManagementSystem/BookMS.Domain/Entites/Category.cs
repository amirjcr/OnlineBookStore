
namespace BookMS.Domain.Entites;

public class Category:BaseEntity<int>
{
    public Category(string titel, Category parentId)
    {
        Titel = titel;
        ParentId = parentId;
    }

    public string Titel { get; private set; }
    
    public Category ParentId { get; private set; }
    public ICollection<Category> Children { get; private set; }
}
