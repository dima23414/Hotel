using Hotel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Hotel.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        AppContext db;

        public RegisterPage()
        {
            InitializeComponent();
            db = new AppContext();
        }

        private void BtnRegistrationClick(object sender, RoutedEventArgs e)
        {
            if (!IsValidLogin(LoginBox.Text) || !IsValidEmail(Email.Text)
            || !IsValidPass(PaswordBox.Password) || !IsValidPass(PaswordBox2.Password)
            || PaswordBox2.Password != PaswordBox.Password)
            {
                MessageBox.Show("Данные введены не корректно", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string login = LoginBox.Text.Trim();
            string pass = PaswordBox.Password;
            string email = Email.Text.Trim();

            if (!db.Users.Any(x => x.Login == login))
            {
                User user = new User(login, pass, email);
                db.Users.Add(user);
                db.SaveChanges();

                MessageBox.Show("Пользователь успешно внес в базу данных", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                LoginBox.Text = "";
                PaswordBox.Password = "";
                PaswordBox2.Password = "";
                Email.Text = "";
            }
            else
            {
                MessageBox.Show("Пользователь с таким именим уже внесен в базу данных", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool IsValidLogin(string login)
        {
            return (login.Trim().Count() >= 5);
        }

        private void LoginBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LoginBox.Text.Trim().Count() == 0 || IsValidLogin(LoginBox.Text))
            {
                LoginBox.ToolTip = "";
                LoginBox.Background = Brushes.Transparent;
            }
            else
            {
                LoginBox.ToolTip = "Это поле введено некорректно, длина логина должна быть не меньше 5 символов";
                LoginBox.Background = Brushes.Pink;
            }
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";

            Regex regex = new Regex(pattern);

            return regex.IsMatch(email.Trim());
        }

        private void Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsValidEmail(Email.Text) && Email.Text.Trim().Count() > 0)
            {
                Email.ToolTip = "Это поле введено некорректно.";
                Email.Background = Brushes.Pink;
            }
            else
            {
                Email.ToolTip = "";
                Email.Background = Brushes.Transparent;
            }
        }

        private bool IsValidPass(string pass)
        {
            return (pass.Count() >= 5);
        }

        private void passwordsMatch()
        {
            if (PaswordBox.Password.Count() > 0 && PaswordBox2.Password.Count() > 0 && PaswordBox.Password != PaswordBox2.Password)
            {
                PaswordBox.ToolTip = "Пароли не сопадают";
                PaswordBox2.ToolTip = "Пароли не сопадают";
                PaswordBox.Background = Brushes.Pink;
                PaswordBox2.Background = Brushes.Pink;
            }
            else
            {
                PaswordBox.ToolTip = "";
                PaswordBox2.ToolTip = "";
                PaswordBox.Background = Brushes.Transparent;
                PaswordBox2.Background = Brushes.Transparent;
            }
        }

        private void PaswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PaswordBox.Password.Count() == 0 || IsValidPass(PaswordBox.Password))
            {
                PaswordBox.ToolTip = "";
                PaswordBox.Background = Brushes.Transparent;
            }
            else
            {
                PaswordBox.ToolTip = "Это поле введено некорректно, длина логина должна быть не меньше 5 символов";
                PaswordBox.Background = Brushes.Pink;
                return;
            }

            passwordsMatch();
        }

        private void PaswordBox2_PasswordChanged(object sender, RoutedEventArgs e)
        {

            if (PaswordBox2.Password.Count() == 0 || IsValidPass(PaswordBox2.Password))
            {
                PaswordBox2.ToolTip = "";
                PaswordBox2.Background = Brushes.Transparent;
            }
            else
            {
                PaswordBox2.ToolTip = "Это поле введено некорректно, длина логина должна быть не меньше 5 символов";
                PaswordBox2.Background = Brushes.Pink;
                return;
            }

            passwordsMatch();
        }

        private void Button_Auth_Enter_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthWindow());
        }
    }
}
