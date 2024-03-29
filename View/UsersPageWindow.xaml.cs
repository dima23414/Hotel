using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq.Dynamic.Core;
using Hotel;
using Hotel.ViewModel;

namespace Hotel.View
{
    /// <summary>
    /// Логика взаимодействия для UsersPageWindow.xaml
    /// </summary>
    public partial class UsersPageWindow : Page
    {
        public UsersPageWindow()
        {
            InitializeComponent();
            DataContext = new UsersPageWindowViewModel();
           // Hotel.AppContext db = new Hotel.AppContext();
           // listOfUser.ItemsSource = db.Users.ToList();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://www.fletschhorn.ch/";
            Hyperlink hyperlink = new Hyperlink();
            hyperlink.NavigateUri = new Uri(url);
            hyperlink.Inlines.Add(url);

            if (hyperlink != null)
            {
                // Получаем URI ссылки
                Uri uri = hyperlink.NavigateUri;

                // Открываем URL в стандартном браузере
                Process.Start(new ProcessStartInfo(uri.AbsoluteUri) { UseShellExecute = true });

                // Пометить событие как обработанное, чтобы избежать дальнейшей обработки встроенным обработчиком гиперссылки
                e.Handled = true;
            }
        }

        //        text FilterTextChanged



        private void FilterTextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            Hotel.AppContext db = new Hotel.AppContext();
         //   listOfUser.ItemsSource = db.Users.Where(QueryTestLINQ(new List<string> { "Login", "Email" }, FilterText.Text)).ToList();
        }

        private void FilterWarehouseChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            Hotel.AppContext db = new Hotel.AppContext();
          //  GridWarehouse.ItemsSource = db.ViewWarehouse.Where(QueryTestLINQ(new List<string> { "Id", "nameProduct" }, FilterWarehouse.Text)).ToList();
        }


        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show("Кнопка нажата id " + (sender as Button).Tag.ToString());

            UsersPageWindowViewModel usersPageWindowViewModel = new UsersPageWindowViewModel();

            usersPageWindowViewModel.AddNewItemToDirectory("Тест", 1);

        }
    }
}
