using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RegIN_Druzhinin.Pages
{
    /// <summary>
    /// Логика взаимодействия для Recovery.xaml
    /// </summary>
    public partial class Recovery : Page
    {
        string OldLogin;
        bool IsCapture = false;
        public Recovery()
        {
            InitializeComponent();
            MainWindow.mainWindow.UserLogIn.HandlerCorrectLogin += CorrectLogin;
            MainWindow.mainWindow.UserLogIn.HandlerInCorrectLogin += InCorrectLogin;
            Capture.HandlerCorrectCapture += CorrectCapture;
        }

        private void CorrectLogin()
        {
            if (OldLogin != TbLogin.Text)
            {
                SetNotification("Hi, " + MainWindow.mainWindow.UserLogIn.Name, Brushes.Black);
                try
                {
                    BitmapImage biImg = new BitmapImage();
                    MemoryStream ms = new MemoryStream(MainWindow.mainWindow.UserLogIn.Image);
                    // Сиганлизируем о начале инициализации
                    biImg.BeginInit();
                    // Указываем источник потока
                    biImg.StreamSource = ms;
                    // Сигнализируем о конце инициализации
                    biImg.EndInit();
                    // Получаем ImageSoruce
                    ImageSource imgSrc = biImg;
                    // Создаём анимацию старта
                    DoubleAnimation StartAnimation = new DoubleAnimation();
                    // Указываем значение от которого она выполняется
                    StartAnimation.From = 1;
                    // Указываем значение до которого она выполняется
                    StartAnimation.To = 0;
                    // Указываем продолжительность выполнения
                    StartAnimation.Duration = TimeSpan.FromSeconds(0.6);
                    // Присваиваем событие при конце анимации
                    StartAnimation.Completed += delegate
                    {
                        // Устанавливаем изображение
                        IUser.Source = imgSrc;
                        // Создаём анимацию конца
                        DoubleAnimation EndAnimation = new DoubleAnimation();
                        // Указываем значение от которого она выполняется
                        EndAnimation.From = 0;
                        // Указываем значение до которого она выполняется
                        EndAnimation.To = 1;
                        // Указываем продолжительность выполнения
                        EndAnimation.Duration = TimeSpan.FromSeconds(1.2);
                        // Запускаем анимацию плавной смены на изображении
                        IUser.BeginAnimation(Image.OpacityProperty, EndAnimation);
                    };
                    // Запускаем анимацию плавной смены на изображении
                    IUser.BeginAnimation(Image.OpacityProperty, StartAnimation);
                }
                catch (Exception exp)
                {
                    Debug.WriteLine(exp.Message);
                };
                OldLogin = TbLogin.Text;
                SendNewPassword();
            }
        }
        private void InCorrectLogin()
        {
            if (LNameUser.Content != "")
            {
                // Очищаем приветствие пользователя
                LNameUser.Content = "";
                // Создаём анимацию старта
                DoubleAnimation StartAnimation = new DoubleAnimation();
                // Указываем значение от которого она выполняется
                StartAnimation.From = 1;
                // Указываем значение до которого она выполняется
                StartAnimation.To = 0;
                // Указываем продолжительность выполнения
                StartAnimation.Duration = TimeSpan.FromSeconds(0.6);
                // Присваиваем событие при конце анимации
                StartAnimation.Completed += delegate
                {
                    // Указываем стандартный логотип в качестве изображения пользователя
                    IUser.Source = new BitmapImage(new Uri("pack://application:,,,/Images/ic-user.png"));
                    // Создаём анимацию конца
                    DoubleAnimation EndAnimation = new DoubleAnimation();
                    // Указываем значение от которого она выполняется
                    EndAnimation.From = 0;
                    // Указываем значение до которого она выполняется
                    EndAnimation.To = 1;
                    // Указываем продолжительность выполнения
                    EndAnimation.Duration = TimeSpan.FromSeconds(1.2);
                    // Запускаем анимацию плавной смены на изображении
                    IUser.BeginAnimation(OpacityProperty, EndAnimation);
                };
                // Запускаем анимацию плавной смены на изображении
                IUser.BeginAnimation(OpacityProperty, StartAnimation);
            }
            // сообщение о том что логин ввелён не парвильно
            if (TbLogin.Text.Length > 0)
                // Выводим сообщение о том, что логин введён не верно, цвет текста красный
                SetNotification("Login is incorrect", Brushes.Red);
        }
        private void CorrectCapture()
        {
            // Отключаем элемент капчи
            Capture.IsEnabled = false;
            // Запоминаем что ввод капчи осуществлён
            IsCapture = true;
            // Вызываем генерацию нового пароля
            SendNewPassword();
        }
        private void SetLogin(object sender, KeyEventArgs e)
        {
            // Если нажата клавиша Enter
            if (e.Key == Key.Enter)
                // Вызываем полуение данных пользователя по логину
                MainWindow.mainWindow.UserLogIn.GetUserLogin(TbLogin.Text);
        }
        private void SetLogin(object sender, RoutedEventArgs e) =>
        MainWindow.mainWindow.UserLogIn.GetUserLogin(TbLogin.Text);
        public void SendNewPassword()
        {
            // Если пройдена капча
            if (IsCapture)
            {
                // Если пароль не является пустым, а это значит пользователь ввёл правильную почту
                if (MainWindow.mainWindow.UserLogIn.Password != String.Empty)
                {
                    // Создаём анимацию старта
                    DoubleAnimation StartAnimation = new DoubleAnimation();
                    // Указываем значение от которого она выполняется
                    StartAnimation.From = 1;
                    // Указываем значение до которого она выполняется
                    StartAnimation.To = 0;
                    // Указываем продолжительность выполнения
                    StartAnimation.Duration = TimeSpan.FromSeconds(0.6);
                    // Присваиваем событие при конце анимации
                    StartAnimation.Completed += delegate
                    {
                        // Указываем стандартный логотип в качестве изображения пользователя
                        IUser.Source = new BitmapImage(new Uri("pack://application:,,,/Images/ic-mail.png"));
                        // Создаём анимацию конца
                        DoubleAnimation EndAnimation = new DoubleAnimation();
                        // Указываем значение от которого она выполняется
                        EndAnimation.From = 0;
                        // Указываем значение до которого она выполняется
                        EndAnimation.To = 1;
                        // Указываем продолжительность выполнения
                        EndAnimation.Duration = TimeSpan.FromSeconds(1.2);
                        // Запускаем анимацию плавной смены на изображении
                        IUser.BeginAnimation(OpacityProperty, EndAnimation);
                    };
                    // Запускаем анимацию плавной смены на изображении
                    IUser.BeginAnimation(OpacityProperty, StartAnimation);
                    // Выводим сообщение о том что новый пароль будет отправлен на почту
                    SetNotification("An email has been sent to your email.", Brushes.Black);
                    // Вызываем функцию создания нового пароля
                    MainWindow.mainWindow.UserLogIn.CrateNewPassword();
                }
            }
        }
        public void SetNotification(string Message, SolidColorBrush _Color)
        {
            LNameUser.Content = Message;
            LNameUser.Foreground = _Color;
        }
        private void OpenLogin(object sender, MouseButtonEventArgs e)
        {
            MainWindow.mainWindow.OpenPage(new Login());
        }

    }
}
