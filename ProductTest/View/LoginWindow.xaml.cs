using Microsoft.EntityFrameworkCore;
using ProductTest.Context;
using ProductTest.Service;
using System.Windows;
using System.Windows.Input;

namespace ProductTest.View
{
    public partial class LoginWindow : Window
    {
        private ProductDbContext _db = new ProductDbContext();
        private MessageService _messageService = new MessageService();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void logTxt_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
        
        private void logBtn_Click(object sender, RoutedEventArgs e)
        {   
            if (string.IsNullOrEmpty(txtLog.Text.Trim()))
            {
                string message = "Введите логин";
                _messageService.Warning(message);
                return;
            }
            var user = _db.Users
                .Include(u => u.IdRoleNavigation)
                .FirstOrDefault(u => u.Login == txtLog.Text);
            if (user == null)
            {
                string message = "Пользователь не найден!";
                _messageService.Error(message);
                return;
            }
            new MainWindow(user).Show();
            Close();
        }
    }
}
