namespace ProductTest.Models;

public partial class Category
{
    public int IdCategory { get; set; }

    public string Category1 { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
