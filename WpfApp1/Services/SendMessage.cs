using System.Linq;
using System.Net;
using System;
using System.Net.Mail;
using System.Windows;
using WpfApp1.Models;

namespace WpfApp1.Services
{
    internal class SendMessage
    {
        private static Пр4_Агентсво_недвижимостиEntities db;

        // Метод для отправки email клиенту
        // Проверяет наличие клиента в базе данных и отправляет сообщение, если клиент найден
        public void SendEmailToClient(string email, string subject, string body)
        {
            db = new Пр4_Агентсво_недвижимостиEntities();
            var client = db.Клиент.FirstOrDefault(c => c.Email == email); // Поиск клиента по email

            if (client != null)
            {
                SendEmail(email, subject, body); // Отправка email, если клиент найден
            }
            else
            {
                MessageBox.Show("Клиент с указанным email не найден."); // Сообщение, если клиент не найден
            }
        }

        // Метод для отправки email сотруднику
        // Проверяет наличие сотрудника в базе данных и отправляет сообщение, если сотрудник найден
        public void SendEmailToEmployee(string email, string subject, string body)
        {
            db = new Пр4_Агентсво_недвижимостиEntities();
            var employee = db.Сотрудник.FirstOrDefault(e => e.email == email); // Поиск сотрудника по email

            if (employee != null)
            {
                SendEmail(email, subject, body); // Отправка email, если сотрудник найден
            }
            else
            {
                MessageBox.Show("Сотрудник с указанным email не найден."); // Сообщение, если сотрудник не найден
            }
        }

        // Метод для фактической отправки email
        // Использует SMTP-клиент для отправки сообщения

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        private void SendEmail(string email, string subject, string body)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587) // Настройка SMTP-клиента для Gmail
            {
                EnableSsl = true, // Включение SSL для безопасного соединения
                UseDefaultCredentials = false, // Отключение использования стандартных учетных данных
                Credentials = new NetworkCredential("ehsonboboev09@gmail.com", "zhiwjosuvemzgnxq") // Учетные данные для аутентификации
            };

            MailMessage mailMessage = new MailMessage // Создание объекта сообщения
            {
                From = new MailAddress("ehsonboboev09@gmail.com"), // Адрес отправителя
                To = { email }, // Адрес получателя
                Subject = subject, // Тема сообщения
                Body = body // Текст сообщения
            };

            client.Send(mailMessage); // Отправка сообщения
            MessageBox.Show("Код верификации отправлен на почту"); // Сообщение об успешной отправке
        }
    }
}