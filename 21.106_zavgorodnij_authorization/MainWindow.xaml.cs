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
using _21._106_zavgorodnij_authorization.Model;

namespace _21._106_zavgorodnij_authorization
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Entities1 db = new Entities1();
        const string welcomeText = "Добро пожаловать! Ваша роль: ";
        const string failText = "Неверно введены логин или пароль!";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LogIn_btn_Click(object sender, RoutedEventArgs e)
        {
            string login = login_tb.Text;
            string password = password_pb.Password;

            try // На случай исключения
            {
                var user = db.Users.FirstOrDefault(p => p.login == login && p.password == password); // Находим пользователя по логину и паролю
                if (user != null) LoginSuccessfull(user); // Если пользователь найден
                else LoginFailed(); // Если пользователь не найден
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Отображает заданное сообщение по случаю успеха входа
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="Exception"></exception>
        private void LoginSuccessfull(Users user)
        {
            result_lbl.Visibility = Visibility.Visible;
            result_lbl.Foreground = Brushes.Green;

            var role = db.Roles.Where(p => p.roleID == user.roleID).First();

            if (role != null) result_lbl.Content = welcomeText + role.title;
            else throw new Exception("Не найдена роль");
        }

        /// <summary>
        /// Отображает заданное сообщение по случаю неудачи входа
        /// </summary>
        private void LoginFailed()
        {
            result_lbl.Visibility = Visibility.Visible;
            result_lbl.Foreground = Brushes.IndianRed;

            result_lbl.Content = failText;
        }
    }
}
