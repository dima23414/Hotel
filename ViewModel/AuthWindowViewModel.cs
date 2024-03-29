using Hotel.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using Hotel.View;

namespace Hotel.ViewModel
{
    public class AuthWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property, used to notify listeners.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public ICommand LoginCommand { get; set; }
        public ICommand RegCommand { get; set; }

        private string _Login;

        public string Login
        {
            get { return _Login; }
            set
            {
                _Login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        private SecureString _Password;

        public SecureString Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        /// <summary>
        /// Юзер залогинился
        /// </summary>
        public bool IsAuth
        {
            get
            {
                return Authenticate();
            }
        }

        public AuthWindowViewModel()
        {
            LoginCommand = new RelayCommand(x => Login_Click("ImportButton"));
            RegCommand = new RelayCommand(x => Reg_Click("ImportButton"));
        }

        private void Reg_Click(object obj)
        {
        }

        private void Login_Click(object obj)
        {
        }

        private bool Authenticate()
        {
            bool auth = false;

            string pass = SecureStringToString(_Password);

            User AuthUser;
            try
            {
                using (AppContext db = new AppContext())
                {
                    AuthUser = db.Users.FirstOrDefault(user => user.Login == _Login && user.Pass == pass);
                }

                if (AuthUser != null)
                {
                    return true;
                    //UsersPageWindow usersPageWindow = new UsersPageWindow();
                    //usersPageWindow.Show();
                    //Close();
                }
                else
                {
                    MessageBox.Show("Такой пользователь не обнаружен");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка аутентификации: " + ex.Message);
                return false;
            }

            return auth;
        }


        public static String SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
    }
}
