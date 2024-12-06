using System;
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
        public static void SendMessage(string message, string to)
        {
            var smptClient = new SmtpClient("smtp.yandex.ru")
            {
                Port = 587,
                Credentials = new NetworkCredential("zhmyshenckoal@yandex.ru", "kjedrpclifubhjod"),
                EnableSsl = true,
            };
            smptClient.Send("zhmyshenckoal@yandex.ru", to, "Проект RegIn", message);
        }
    }
}
    