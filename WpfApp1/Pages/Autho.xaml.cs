using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Threading;
using WpfApp1.Models;
using WpfApp1.Services;

namespace WpfApp1.Pages
{
    public partial class Autho : Page
    {
        int click; // Счетчик попыток входа
        int clicn; // Счетчик подтверждений кода
        int n; // Счетчик неудачных попыток
        private DispatcherTimer timer; // Таймер для блокировки входа
        private int timeLeft; // Оставшееся время блокировки
        SendMessage sendMessage = new SendMessage(); // Сервис отправки сообщений
        string checkCode; // Код для двухэтапной аутентификации

        private Авторизация _currentUser; // Текущий пользователь

        // Генерация 4-значного кода для двухэтапной аутентификации
        private string GenerateFourDigitCode()
        {
            Random random = new Random();
            return random.Next(1000, 9999).ToString();
        }

        public Autho()
        {
            InitializeComponent();
            click = 0; // Инициализация счетчиков
            n = 0;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Интервал таймера
            timer.Tick += Timer_Tick; // Обработчик события таймера
        }

        // Обработчик события таймера
        private void Timer_Tick(object sender, EventArgs e)
        {
            timeLeft--;
            if (timeLeft > 0)
            {
                tbTimeLeft.Text = $"Подождите {timeLeft} секунд";
            }
            else
            {
                timer.Stop(); // Остановка таймера
                tbTimeLeft.Visibility = Visibility.Hidden; // Скрытие текста таймера
                tbLogin.IsEnabled = true; // Разблокировка полей
                tbPassword.IsEnabled = true;
                btnEnterGuest.IsEnabled = true;
                tbCaptcha.IsEnabled = true;
                btnEnter.IsEnabled = true;
                n = 0; // Сброс счетчика неудачных попыток
            }
        }

        // Обработчик нажатия кнопки входа
        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            click += 1;
            string login = tbLogin.Text.Trim();
            string password = tbPassword.Text.Trim();
            HashPassword hash = new HashPassword();
            password = hash.HashPassword1(password); // Хэширование пароля
            db = new Пр4_Агентсво_недвижимостиEntities();

            // Поиск пользователя в базе данных
            var user = db.Авторизация.Where(x => x.login == login && x.password == password).FirstOrDefault();

//Двух этапная 
            //if (user != null)
            //{
            //    _currentUser = user;
      //  authentication(user); // Запуск двухэтапной аутентификации
            //    return;
            //}

            if (click == 1)
            {
                if (user != null)
                {
                    MessageBox.Show("Вы вошли под: " + user.role.role1.ToString());
                    LoadPage(user.role.role1.ToString(), user); // Переход на страницу в зависимости от роли
                }
                else
                {
                    MessageBox.Show("Вы ввели логин или пароль неверно!");
                    GenerateCapctcha(); // Генерация капчи
                    tbLogin.Text = " ";
                    tbPassword.Text = " ";
                    tbCaptcha.Text = " ";
                    n++;
                }
            }
            else if (click > 1)
            {
                if (user != null && tbCaptcha.Text.Trim() == tblCaptcha.Text.Trim())
                {
                    MessageBox.Show("Вы вошли под: " + user.role.role1.ToString());
                    LoadPage(user.role.role1.ToString(), user);
                }
                else
                {
                    if (n >= 3)
                    {
                        locked(); // Блокировка входа
                        Forgotpassword.Visibility = Visibility.Visible; // Показ кнопки "Забыли пароль?"
                    }
                    else
                    {
                        MessageBox.Show("Введите данные заново!");
                        GenerateCapctcha();
                        n++;
                        tbLogin.Text = " ";
                        tbPassword.Text = " ";
                        tbCaptcha.Text = " ";
                    }
                }
            }
        }

        // Блокировка входа
        private void locked()
        {
            MessageBox.Show("Блокировка: Слишком много неудачных попыток входа.");
            tbLogin.IsEnabled = false;
            tbPassword.IsEnabled = false;
            btnEnter.IsEnabled = false;
            btnEnterGuest.IsEnabled = false;
            tbCaptcha.IsEnabled = false;
            tbTimeLeft.Visibility = Visibility.Visible;
            timeLeft = 10;
            tbTimeLeft.Text = $"Подождите {timeLeft} секунд";
            timer.Start(); // Запуск таймера
        }

        // Генерация капчи
        private void GenerateCapctcha()
        {
            tbCaptcha.Visibility = Visibility.Visible;
            tblCaptcha.Visibility = Visibility.Visible;
            string capctchaText = CapthchaGenerator.GenerateCaptchaText(6);
            tblCaptcha.Text = capctchaText;
            tblCaptcha.TextDecorations = TextDecorations.Strikethrough;
        }

        private static Пр4_Агентсво_недвижимостиEntities db;

        // Обработчик входа как гость
        private void btnEnterGuests_Click(object sender1, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Client(null, null));
        }

        // Двухэтапная аутентификация
        private void authentication(Авторизация user)
        {
            labelcode.Visibility = Visibility.Visible;
            tbcode.Visibility = Visibility.Visible;
            bntconfirm.Visibility = Visibility.Visible;

            checkCode = GenerateFourDigitCode(); // Генерация кода

            if (user != null)
            {
                if (user.Клиент.Any())
                {
                    sendMessage.SendEmailToClient(user.Клиент.First().Email, "Двухэтапная аутентификация", $"Ваш код: {checkCode}");
                }
                else if (user.Сотрудник.Any())
                {
                    sendMessage.SendEmailToEmployee(user.Сотрудник.First().email, "Двухэтапная аутентификация", $"Ваш код: {checkCode}");
                }
            }
        }
                                                            
        // Загрузка страницы в зависимости от роли
        private void LoadPage(string _role, Авторизация user)
        {
            click = 0;

            //if (tbcode.Text == checkCode)
            //{
            //    clicn += 1;

                switch (_role)
                {
                    case "Клиент":
                        NavigationService.Navigate(new Client(user, _role));
                        break;

                    case "Сотрудник":
                        NavigationService.Navigate(new Sotrudnik(user, _role));
                        break;
                }
            //}
        }

        // Обработчик входа как гость
        private void btnEnterGuest_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы вошли как гость: ");
            NavigationService.Navigate(new Client(null, null));
        }

        // Переход на страницу восстановления пароля
        private void Forgotpassword_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new forgotpassword());
        }

        // Обработчик подтверждения кода
        private void bntconfirm_Click(object sender, RoutedEventArgs e)
        {
            if (tbcode.Text == checkCode)
            {
                LoadPage(_currentUser.role.role1.ToString(), _currentUser);
            }
            else
            {
                MessageBox.Show("Неверный код! Попробуйте снова.");
                tbcode.Clear();
            }
        }
    }
}