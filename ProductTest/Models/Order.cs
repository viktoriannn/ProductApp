namespace ProductTest.Models;

public partial class Order
{
    public int IdOrder { get; set; }

    public int NumberOrder { get; set; }

    public DateOnly DataOrder { get; set; }

    public int IdUser { get; set; }

    public int IdProduction { get; set; }

    public int IdSize { get; set; }

    public int Count { get; set; }

    public virtual Production IdProductionNavigation { get; set; } = null!;

    public virtual Size IdSizeNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();
}
