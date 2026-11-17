namespace ProductTest.Models;

public partial class Production
{
    public int IdProduction { get; set; }

    public string Prodaction { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<StockItem> StockItems { get; set; } = new List<StockItem>();
}
