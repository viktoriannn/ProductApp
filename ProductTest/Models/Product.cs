namespace ProductTest.Models;

public partial class Product
{
    public int IdProduct { get; set; }

    public int? IdCategory { get; set; }

    public int? IdSubcategory { get; set; }

    public string PathImg { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int IdProduction { get; set; }

    public string Description { get; set; } = null!;

    public string Structure { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual Category? IdCategoryNavigation { get; set; }

    public virtual Production IdProductionNavigation { get; set; } = null!;

    public virtual Subcategory? IdSubcategoryNavigation { get; set; }

    public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();

    public virtual ICollection<StockItem> StockItems { get; set; } = new List<StockItem>();
}
