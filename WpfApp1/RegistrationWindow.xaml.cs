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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        RegAuthDbModelContainer db = new RegAuthDbModelContainer();
        public RegistrationWindow()
        {
            InitializeComponent();
        }
        private void CloseWindow() {
            this.Close();
        }

        private void RegisterAction(object sender, RoutedEventArgs e)
        {
            string login = LoginField.Text;
            string password = PasswordField.Password;
            string first = FirstnameField.Text;
            string middle = SecondnameField.Text;
            string last = LastnameField.Text;
            if (login == "" || password == "" || first == "" || middle == "" || last == "") {
                MessageBox.Show("Все поля должны быть заполнены.", "Ошибка.");
                return;
            }
            if (db.UserSet.Select(item => item.Login).Contains(login)) {
                MessageBox.Show(" Такой логин уже существует.", "Ошибка.");
                return;
            }
            User newUser = new User() { 
            Login=login,
            Password = password,
            Firstname = first,
            Middlename = middle,
            Lastname = last
            };
            db.UserSet.Add(newUser);
            db.SaveChanges();
            MessageBox.Show("Вы успешно зарегистрированы!");
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void CancelAction(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
