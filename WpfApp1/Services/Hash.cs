using System;  
using System.Collections.Generic;  
using System.Linq; 
using System.Security.Cryptography;  
using System.Text;  
using System.Threading.Tasks;  

namespace WpfApp1  
{
    internal class HashPassword  
    {
        public string HashPassword1(string password)
        {
            // Использование конструкции using для автоматического управления ресурсами
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Преобразование входного пароля в массив байтов с использованием кодировки UTF8
                byte[] SourceSwitch = Encoding.UTF8.GetBytes(password);

                // Вычисление хеш-значения для массива байтов
                byte[] hashSourceBytePassw = sha256Hash.ComputeHash(SourceSwitch);

                // Преобразование массива байтов хеш-значения в строку в шестнадцатеричном формате
                // Метод BitConverter.ToString возвращает строку с разделителями "-", которые затем удаляются с помощью Replace
                string hashPassw = BitConverter.ToString(hashSourceBytePassw).Replace("-", string.Empty);

                // Возврат строки с хеш-значением
                return hashPassw;
            }
        }
    }
}