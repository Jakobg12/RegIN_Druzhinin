using RegIN_Druzhinin.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace RegIN_Druzhinin
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Переменная которая ссылается на окно MainWindow
        /// </summary>
        public static MainWindow mainWindow;
        /// <summary>
        /// Авторизированный пользователь
        /// </summary>
        public User UserLogIn = new User();
        public MainWindow()
        {
            InitializeComponent();
            // Указываем ссылку на окно MainWindow
            mainWindow = this;
            // Вызываем функцию открытия страницы Login
            OpenPage(new Pages.Login());
        }
        /// <summary>
        /// Функция открытия страницы
        /// </summary>
        /// <param name="page">Страница которую необходимо открыть</param>
        public void OpenPage(Page page)
        {
            // Создаём стартовую анимацию
            DoubleAnimation StartAnimation = new DoubleAnimation();
            // Указываем значение от которого происходит анимация
            StartAnimation.From = 1;
            // Указываем значение до которого происходит аниция
            StartAnimation.To = 0;
            // Указываем продолжительность
            StartAnimation.Duration = TimeSpan.FromSeconds(0.6);
            // Указываем событие выполнения
            StartAnimation.Completed += delegate
            {
                // Меням страницу
                frame.Navigate(page);
                // Создаём конечную анимцию
                DoubleAnimation EndAnimation = new DoubleAnimation();
                // Указываем значение от которого происходит анимация
                EndAnimation.From = 0;
                // Указываем значение до которого происходит аниция
                EndAnimation.To = 1;
                // Указываем продолжительность
                EndAnimation.Duration = TimeSpan.FromSeconds(1.2);
                // Запускаем анимацию прозрачности для фрейма на сцене
                frame.BeginAnimation(Frame.OpacityProperty, EndAnimation);
            };
            // Запускаем анимацию прозрачности для фрейма на сцене
            frame.BeginAnimation(Frame.OpacityProperty, StartAnimation);
        }
    }
}
