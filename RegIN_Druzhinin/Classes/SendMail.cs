﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RegIN_Druzhinin.Classes
{
    public class SendMail
    {
        /// <summary>
        /// Функция отправки сообщения
        /// </summary>
        /// <param name="Message">Сообщение которое необходимо отправить</param>
        /// <param name="To">Почта на которую отправляется сообщение</param>
        public static void SendMessage(string Message, string To)
        {
            // Создаём SMTP клиент, в качестве хоста указываем яндекс
            var smtpClient = new SmtpClient("smtp.yandex.ru")
            {
                // Указываем порт по которому передаём сообщение
                Port = 587,
                // Указываем почту, с которой будет отправляться сообщение,и пароль от этой почты
                Credentials = new NetworkCredential("zhmyshenckoal@yandex.ru", "kjedrpclifubhjod"),
                // Включаем поддержку SSL
                EnableSsl = true,
            };
            // Вызываем метод send, который отправляет письмо на указанный адресс
            smtpClient.Send("filippin.Alexander@yandex.ru", To, "Проект RegIn", Message);
        }
    }
}
