using Microsoft.EntityFrameworkCore;
using ProductTest.Service;
using ProductTest.Context;
using ProductTest.Helper;
using System.Windows;
using System.Windows.Media;
using ProductTest.View;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ProductTest.Models;

namespace ProductTest
{
    public partial class MainWindow : Window
    {
        private ProductDbContext _db = new ProductDbContext();
        private DiscountService _discountService = new DiscountService();
        private MessageService _messageService = new MessageService();
        private FindSortFilterService _filterService = new FindSortFilterService();

        private string[] _sortTypes = new string[]
        {
            "По умолчанию", "По убыванию", "По возрастанию"
        };
        private List<string> _filterTypes = new List<string>()
        {
            "Все категории"
        };
        private List<ProductView> _allProducts = new List<ProductView>();
        public MainWindow(User? user = null)
        {
            InitializeComponent();
            userName.Text = user != null ? $"{user.Surname} {user.Name} {user.LastName}" : "Гость";
            LoadProduct();
            LoadData();
            toolPanel.Visibility = user != null ? Visibility.Visible : Visibility.Collapsed;
        }
        public void LoadData()
        {
            sortBox.ItemsSource = _sortTypes;
            var categories = _db.Categories.Select(c => c.Category1).ToList();
            foreach (var category in categories)
            {
                _filterTypes.Add(category);
                filterBox.ItemsSource = _filterTypes;
            }
        }
        private void LoadProduct()
        {
            var products = _db.Products
                .Include(p => p.IdCategoryNavigation)
                .Include(p => p.IdProductionNavigation)
                .Include(p => p.StockItems)
                .Include(p => p.ProductOrders).ThenInclude(po => po.IdOrderNavigation)
                .ToList();
            _allProducts = products.Select(p =>
            {
                int totalStock = p.StockItems.Sum(s => s.CountForOrder);
                return new ProductView
                {
                    Name = p.Name,
                    Production = p.IdProductionNavigation?.Prodaction,
                    PathImages = string.IsNullOrEmpty(p.PathImg) ? "/Resources/picture.png" : "/Resources/Images/" + p.PathImg.Trim(),
                    Category = p.IdCategoryNavigation?.Category1,
                    Structure = p.Structure,
                    TotalStock = (totalStock > 5 ? "Много " : "Мало") + $" ({totalStock})",
                    OldPrice = p.Price,
                    DiscountPrice = _discountService.GetDiscountedPrice(p),
                    Background = totalStock <= 3 ? Brushes.LightCoral : Brushes.White,
                    Description = p.Description
                };
            }).ToList();
            ProductList.ItemsSource = _allProducts;
        }

        private void OutBtn_Click(object sender, RoutedEventArgs e)
        {
            string message = "Вы точно хотите выйти?";
            if(!_messageService.Question(message))
                return;
            new LoginWindow().Show();
            Close();
        }

        private void FindSortFilter()
        {
            string? category = filterBox.SelectedIndex > 0 ? filterBox.SelectedItem.ToString() : null;
            var result = _filterService.Apply(_allProducts, findBox.Text, category, sortBox.SelectedIndex);
            ProductList.ItemsSource = result;
        }

        private void findBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            FindSortFilter();
        }

        private void sortBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            FindSortFilter();
        }

        private void filterBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            FindSortFilter();
        }
    }
}
