namespace ProductTest.Models;

public partial class Size
{
    public int IdSize { get; set; }

    public decimal Size1 { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<StockItem> StockItems { get; set; } = new List<StockItem>();
}
