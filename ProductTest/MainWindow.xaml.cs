using Microsoft.EntityFrameworkCore;
using ProductTest.Service;
using ProductTest.Context;
using ProductTest.Helper;
using System.Windows;
using System.Windows.Media;

namespace ProductTest
{
    public partial class MainWindow : Window
    {
        private ProductDbContext _db = new ProductDbContext();
        private DiscountService _discountService = new DiscountService();
        public MainWindow()
        {
            InitializeComponent();
            LoadProduct();
        }

        private void LoadProduct()
        {
            var products = _db.Products
                .Include(p => p.IdCategoryNavigation)
                .Include(p => p.IdProductionNavigation)
                .Include(p => p.StockItems)
                .Include(p => p.ProductOrders).ThenInclude(po => po.IdOrderNavigation)
                .ToList();
            ProductList.ItemsSource = products.Select(p =>
            {
                int totalStock = p.StockItems.Sum(s => s.CountForOrder);
                return new ProductView
                {
                    Name = p.Name,
                    Production = p.IdProductionNavigation?.Prodaction,
                    PathImages = string.IsNullOrEmpty(p.PathImg) ? "/Resources/picture.png" : "/Resources/Images/" + p.PathImg.Trim(),
                    Category = p.IdCategoryNavigation?.Category1,
                    Structure = p.Structure,
                    TotalStock = totalStock > 5 ? "Много" : "Мало",
                    Price = _discountService.GetDiscountedPrice(p),
                    Background = totalStock <= 3 ? Brushes.LightCoral : Brushes.White
                };
            }).ToList();
        }
    }
}
