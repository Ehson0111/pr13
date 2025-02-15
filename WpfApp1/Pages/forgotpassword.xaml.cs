using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
using WpfApp1.Models;
using System.Runtime.Serialization.Json;
using System.Data.SqlClient;
using WpfApp1.Services;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для forgotpassword.xaml
    /// </summary>
    public partial class forgotpassword : Page
    {
        string checkcode; // Хранит сгенерированный код для проверки
        HashPassword hashPassword; // Сервис для хеширования пароля
        SendMessage sendMessage; // Сервис для отправки сообщений
        private static Пр4_Агентсво_недвижимостиEntities db; // Контекст базы данных

        public forgotpassword()
        {
            InitializeComponent(); // Инициализация компонентов страницы
        }

        private void Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Обработчик изменения текста в поле Email (пока не используется)
        }

        private string GenerateFourDigitCode()
        {
            // Генерация 4-значного кода для восстановления пароля
            Random random = new Random();
            return random.Next(1000, 9999).ToString();
        }

        private void send_Click(object sender, RoutedEventArgs e)
        {
            // Обработчик нажатия кнопки "Отправить"
            db = new Пр4_Агентсво_недвижимостиEntities(); // Инициализация контекста базы данных
            sendMessage = new SendMessage(); // Инициализация сервиса отправки сообщений
            string Email1 = Email.Text.Trim(); // Получение введенного email

            // Поиск пользователя в базе данных по email или логину
            var user = db.Авторизация.FirstOrDefault(u => u.login == Email1 || u.Сотрудник.Any(c => c.email == Email1) || u.Клиент.Any(c => c.Email == Email1));
            if (user != null)
            {
                // Генерация кода и отправка его на email
                checkcode = GenerateFourDigitCode();
                if (user.Сотрудник.Any())
                {
                    // Отправка кода сотруднику
                    sendMessage.SendEmailToEmployee(user.Сотрудник.First().email, "Код восстановления пароля", $"Ваш код для восстановления пароля: {checkcode}");
                }
                else if (user.Клиент.Any())
                {
                    // Отправка кода клиенту
                    sendMessage.SendEmailToClient(user.Клиент.First().Email, "Код восстановления пароля", $"Ваш код для восстановления пароля: {checkcode}");
                }

                // Активация полей для ввода кода и кнопки проверки
                check.IsEnabled = true;
                code.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Пользователь с таким логином или почтой не найден.");
            }
        }
       ///
        private void check_Click(object sender, RoutedEventArgs e)
        {
            // Обработчик нажатия кнопки "Проверить"
            string tbcode = code.Text.Trim(); // Получение введенного кода
            if (tbcode == checkcode) // Проверка совпадения кода
            {
                // Скрытие ненужных элементов и отображение новых
                send.Visibility = Visibility.Hidden;
                send.IsEnabled = false;
                check.Visibility = Visibility.Hidden;
                check.IsEnabled = false;

                newcode.Visibility = Visibility.Visible;
                newcode.IsEnabled = true;

                checknewcode.Visibility = Visibility.Visible;
                checknewcode.IsEnabled = true;

                btnsave.IsEnabled = true;
                btnsave.Visibility = Visibility.Visible;

                newlabel.Visibility = Visibility.Visible;
                label.Visibility = Visibility.Visible;

                labelemail.Visibility = Visibility.Hidden;
                labelcode.Visibility = Visibility.Hidden;

                MessageBox.Show("Пароль верный.");
            }
            else
            {
                MessageBox.Show("Неверный пароль.");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            // Обработчик нажатия кнопки "Сохранить"
            string newpassword = newcode.Text.Trim(); // Получение нового пароля
            string confirmPassword = checknewcode.Text.Trim(); // Получение подтверждения пароля
            if (confirmPassword == newpassword) // Проверка совпадения паролей
            {
                string Email1 = Email.Text.Trim(); // Получение email
                var user = db.Авторизация.FirstOrDefault(x => x.login == Email1 || x.Сотрудник.Any(y => y.email == Email1)); // Поиск пользователя
                hashPassword = new HashPassword(); // Инициализация сервиса хеширования
                if (user != null)
                {
                    // Хеширование и сохранение нового пароля
                    user.password = hashPassword.HashPassword1(newpassword);
                    db.SaveChanges(); // Сохранение изменений в базе данных
                    MessageBox.Show("Пароль успешно изменен.");
                    NavigationService.Navigate(new Autho()); // Переход на страницу авторизации
                }
                else
                {
                    MessageBox.Show("Пользователь не найден.");
                }
            }
            else
            {
                MessageBox.Show("Пароли не совпадают.");
            }
        }
    }
}