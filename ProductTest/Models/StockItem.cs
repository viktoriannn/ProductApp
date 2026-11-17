namespace ProductTest.Models;

public partial class StockItem
{
    public int IdStockItem { get; set; }

    public int IdProduct { get; set; }

    public int IdProduction { get; set; }

    public int IdSize { get; set; }

    public int CountForOrder { get; set; }

    public virtual Product IdProductNavigation { get; set; } = null!;

    public virtual Production IdProductionNavigation { get; set; } = null!;

    public virtual Size IdSizeNavigation { get; set; } = null!;
}
