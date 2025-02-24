﻿using System;
using System.Text;

namespace WpfApp1.Services
{
    internal class CapthchaGenerator
    {
        private static readonly Random random = new Random();
        private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        /// <summary>
        /// Генерирует случайный текст заданной длины для капчи.
        /// </summary>
        /// <param name="length">Длина текста капчи.</param>
        /// <returns>Случайный текст капчи.</returns>
        public static string GenerateCaptchaText(int length)
        {
            if (length <= 0)
                throw new ArgumentException("Длина текста капчи должна быть больше нуля.");

            StringBuilder captchaText = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(Characters.Length);
                captchaText.Append(Characters[index]);
            }

            return captchaText.ToString();
        }
    }
}