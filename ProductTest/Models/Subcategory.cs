namespace ProductTest.Models;

public partial class Subcategory
{
    public int IdSubcategory { get; set; }

    public string Subcategory1 { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
