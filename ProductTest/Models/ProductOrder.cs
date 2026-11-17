namespace ProductTest.Models;

public partial class ProductOrder
{
    public int IdProductOrder { get; set; }

    public int IdProduct { get; set; }

    public int IdOrder { get; set; }

    public virtual Order IdOrderNavigation { get; set; } = null!;

    public virtual Product IdProductNavigation { get; set; } = null!;
}
