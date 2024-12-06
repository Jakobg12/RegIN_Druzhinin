using RegIN_Druzhinin.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        string OldLogin;
        int CountSetPassword = 2;
        bool IsCapture = false;

        public Login()
        {
            InitializeComponent();
            MainWindow.mainWindow.UserLogIn.HandlerCorrectLogin += CorrectLogin;
            MainWindow.mainWindow.UserLogIn.HandlerInCorrectLogin += InCorrectLogin;
            Capture.HandlerCorrectCapture += CorrectCapture;
        }

        void animation(bool flag)
        {
            BitmapImage img = new BitmapImage();
            ImageSource src = img;
            if (flag)
            {
                MemoryStream ms = new MemoryStream(MainWindow.mainWindow.UserLogIn.Image);
                img.BeginInit();
                img.StreamSource = ms;
                img.EndInit();
                src = img;
            }
            DoubleAnimation start = new DoubleAnimation();
            start.From = 1;
            start.To = 0;
            start.Duration = TimeSpan.FromSeconds(0.6);
            start.Completed += delegate
            {
                if (flag) IUser.Source = src;
                else IUser.Source = new BitmapImage(new Uri("pack://application:,,,/Images/ic-user.png"));
                DoubleAnimation end = new DoubleAnimation();
                end.From = 0;
                end.To = 1;
                end.Duration = TimeSpan.FromSeconds(1.2);
                IUser.BeginAnimation(OpacityProperty, start);
            };
            IUser.BeginAnimation(OpacityProperty, start);
        }

        public void CorrectLogin()
        {
            if (OldLogin != TbLogin.Text)
            {
                SetNotification("Hi, " + MainWindow.mainWindow.UserLogIn.Name, Brushes.Black);
                try
                {
                    animation(true);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                };
                OldLogin = TbLogin.Text;
            }
        }

        public void InCorrectLogin()
        {
            if (LNameUser.Content.ToString() != "")
            {
                LNameUser.Content = "";
                animation(false);
            }
            if (TbLogin.Text.Length > 0) SetNotification("Login is incorrect", Brushes.Red);
        }

        public void CorrectCapture()
        {
            Capture.IsEnabled = false;
            IsCapture = true;
        }

        private void SetPassword(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) SetPassword();
        }

        public void SetPassword()
        {
            if (MainWindow.mainWindow.UserLogIn.Password != String.Empty)
            {
                if (IsCapture)
                    if (MainWindow.mainWindow.UserLogIn.Password == TbPassword.Password) MainWindow.mainWindow.OpenPage(new Confirmation(Confirmation.TypeConfirmation.Login));
                    else
                    {
                        if (CountSetPassword > 0)
                        {
                            SetNotification($"Password is incorrect, {CountSetPassword} attempts left", Brushes.Red);
                            CountSetPassword--;
                        }
                        else
                        {
                            Thread TBlockAutorization = new Thread(BlockAuthorization);
                            TBlockAutorization.Start();
                            SendMail.SendMessage("An attempt was made to log into your account.", MainWindow.mainWindow.UserLogIn.Login);
                        }
                    }
            }
            else SetNotification($"Enter capture", Brushes.Red);
        }

        public void BlockAuthorization()
        {
            DateTime start = DateTime.Now.AddMinutes(3);
            Dispatcher.Invoke(() =>
            {
                TbLogin.IsEnabled = false;
                TbPassword.IsEnabled = false;
                Capture.IsEnabled = false;
            });
            for (int i = 0; i < 180; i++)
            {
                TimeSpan time = start.Subtract(DateTime.Now);
                string s_minutes = time.Minutes.ToString();
                if (time.Minutes < 10) s_minutes = "0" + s_minutes;
                string s_seconds = time.Seconds.ToString();
                if (time.Seconds < 10) s_seconds = "0" + s_seconds;
                Dispatcher.Invoke(() => SetNotification($"Reauthorization available in: {s_minutes}:{s_seconds}", Brushes.Red));
                Thread.Sleep(1000);
            }
            Dispatcher.Invoke(() =>
            {
                SetNotification("Hi, " + MainWindow.mainWindow.UserLogIn.Name, Brushes.Black);
                TbLogin.IsEnabled = true;
                TbPassword.IsEnabled = true;
                Capture.IsEnabled = true;
                Capture.CreateCapture();
                IsCapture = false;
                CountSetPassword = 2;
            });
        }

        void forSetLogin()
        {
            MainWindow.mainWindow.UserLogIn.GetUserLogin(TbLogin.Text);
            if (TbPassword.Password.Length > 0) SetPassword();
        }

        private void SetLogin(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) forSetLogin();
        }

        private void SetLogin(object sender, RoutedEventArgs e) => forSetLogin();

        public void SetNotification(string Message, SolidColorBrush _Color)
        {
            LNameUser.Content = Message;
            LNameUser.Foreground = _Color;
        }

        private void RecoveryPassword(object sender, MouseButtonEventArgs e) => MainWindow.mainWindow.OpenPage(new Recovery());

        private void OpenRegin(object sender, MouseButtonEventArgs e) => MainWindow.mainWindow.OpenPage(new Regin());
    }
}