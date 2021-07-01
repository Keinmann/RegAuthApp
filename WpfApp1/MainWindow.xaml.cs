using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RegAuthDbModelContainer db;
        public MainWindow()
        {
            InitializeComponent();
            db = new RegAuthDbModelContainer();
        }

        private bool ValidateMainWindowInput(TextBox loginfield, PasswordBox passwordfield) {
            string errorstring ="";
            string loginstr = loginfield.Text;
            string passstr = passwordfield.Password;
            if (loginstr != "" && passstr != "") {
                bool errorflag = false;
                var cyrillic = Enumerable.Range(1024, 256).Select(ch => (char)ch);
                for (int i = 0; i < loginstr.Length; i++) {
                    if (!Char.IsLetterOrDigit(loginstr[i])) { errorflag = true; break; }
                }
                for (int i = 0; i < passstr.Length; i++)
                {
                    if (!Char.IsLetterOrDigit(passstr[i])) { errorflag = true; break; }
                }
                if (errorflag) { errorstring += "Логин и пароль может содержать только цифры и буквы. ";}
                if (loginstr.Any(cyrillic.Contains)||passstr.Any(cyrillic.Contains)) {
                    errorflag = true; errorstring += "Логин и пароль могут содержать только латинские символы. ";
                }
                if (loginstr.Length < 3 && passstr.Length < 3) {
                    errorflag = true;
                    errorstring += "Длина логина и пароля не может быть меньше 3 символов. ";
                }
                if (errorflag== false)
                {
                    return true;
                }
                else {
                    MessageBox.Show(errorstring);
                    return false;
                }
                   
            }
            else {
                
                if (LoginField.Text == "") { errorstring += "Введите логин!"; }
                else if (PasswordField.Password =="") { errorstring += "Введите пароль!"; }
                MessageBox.Show(errorstring,"Ошибка!");
            }
            return false;
        }

        private void RegisterAction(object sender, RoutedEventArgs e)
        {
            RegistrationWindow reg = new RegistrationWindow();
            reg.Show();
            this.Close();
        }

        private void LoginAction(object sender, RoutedEventArgs e)
        {
            if (ValidateMainWindowInput(LoginField, PasswordField))
            {
                if (db.UserSet.Select(item => item.Login + " " + item.Password).Contains(LoginField.Text + " " + PasswordField.Password))
                {
                    MessageBox.Show("Вы авторизированы, " + LoginField.Text);
                }
                else {
                    MessageBox.Show("Ошибка! Такой пользователь не зарегистрирован.");
                }
            }
        }

        private void LoginInputAction(object sender, TextCompositionEventArgs e)
        {

        }

        private void PasswordInputAction(object sender, TextCompositionEventArgs e)
        {

        }
    }
}
