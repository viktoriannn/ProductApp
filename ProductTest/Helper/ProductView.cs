using System.Windows.Media;

namespace ProductTest.Helper
{
    public class ProductView
    {
        public string? Name { get; set; }
        public string? Production { get; set; }
        public string PathImages { get; set; } = "";
        public string? Category { get; set; }
        public string? Structure { get; set; }
        public string? TotalStock { get; set; }
        public decimal Price { get; set; }
        public Brush Background { get; set; } = Brushes.White;
    }
}
