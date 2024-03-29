using Hotel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Hotel.ViewModel;
using MaterialDesignThemes.Wpf;

namespace Hotel.View
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Page
    {
        public AuthWindow()
        {
            InitializeComponent();
            DataContext = new AuthWindowViewModel();
        }


        private bool IsValidLogin(string login)
        {
            return (login.Trim().Count() >= 5);
        }

        private void LoginBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*  if (LoginBox.Text.Trim().Count() == 0 || IsValidLogin(LoginBox.Text))
              {
                  LoginBox.ToolTip = "";
                  LoginBox.Background = Brushes.Transparent;
              }
              else
              {
                  LoginBox.ToolTip = "Это поле введено некорректно, длина логина должна быть не меньше 5 символов";
                  LoginBox.Background = Brushes.Pink;
              }*/
        }

        private bool IsValidPass(string pass)
        {
            return (pass.Count() >= 5);
        }

        private void PaswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            /*  if (PaswordBox.Password.Count() == 0 || IsValidPass(PaswordBox.Password))
              {
                  PaswordBox.ToolTip = "";
                  PaswordBox.Background = Brushes.Transparent;
              }
              else
              {
                  PaswordBox.ToolTip = "Это поле введено некорректно, длина логина должна быть не меньше 5 символов";
                  PaswordBox.Background = Brushes.Pink;
                  return;
              }*/
        }

        private void BtnRegClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            //mainWindow.Show();
            //Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                if (((dynamic)this.DataContext).IsAuth)
                {
                    var wnd = Window.GetWindow(this);
                    Grid g = (Grid)wnd.Content;
                    Frame f = (Frame)g.Children[0];
                    //DialogHost d = (DialogHost)g.Children[0];
                    //Frame f = (Frame)d.Content;

                    f.Source = new Uri("UsersPageWindow.xaml", UriKind.RelativeOrAbsolute);

                    //NavigationService.Navigate(new UsersPageWindow());
                }
            }
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
          //  MessageBox.Show("Вошло в событие");

              NavigationService.Navigate(new RegisterPage());
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).Password = ((PasswordBox)sender).SecurePassword;
            }
        }
    }
}
