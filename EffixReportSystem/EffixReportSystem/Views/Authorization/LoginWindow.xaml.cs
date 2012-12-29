using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using EffixReportSystem.Helper.Classes;


namespace EffixReportSystem.Views.Authorization
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        #region Fields

        #region Configuration Parameters

        /// <summary>
        /// ip address СУБД
        /// </summary>
        private string dbHost;
        /// <summary>
        /// Источник данных
        /// </summary>
        private string dataSource;
        /// <summary>
        /// Порт БД
        /// </summary>
        private string dataBasePort;
        /// <summary>
        /// Имя пользователя
        /// </summary>
        private string dbUserName;
        /// <summary>
        /// Пароль
        /// </summary>
        private string dbPassword;
        /// <summary>
        /// Использовать SID или Service_Name для подключения к Oracle
        /// </summary>
        private bool dbUseSid;
        /// <summary>
        /// Модуль программного обеспечения
        /// </summary>
        private int programModule = 3;

        #endregion Configuration Parameters

        /// <summary>
        /// Пользователь
        /// </summary>
        //private process_user_loginResultSet2 user;
        /// <summary>
        /// Идентификатор сессии
        /// </summary>
        private string sessionId;
        /// <summary>
        /// IP адрес PC
        /// </summary>
        private string ipAddress = "";
        /// <summary>
        /// Основное рабочее окно приложения
        /// </summary>
        private ApplicationView app;
        /// <summary>
        /// Контролируем, что можно входить в систему
        /// </summary>
        private bool canLogin = true;
        /// <summary>
        /// Запущен только один экземпляр приложения
        /// </summary>
        static bool onlyInstance;
        /// <summary>
        /// Используется при определении, что запущен только один экземпляр приложения
        /// </summary>
        Mutex mtx = new Mutex(true, System.Reflection.Assembly.GetEntryAssembly().FullName, out onlyInstance);
        /// <summary>
        /// Таймер обновления сессии, в случае проблемы канала пытается обновить сессию чаще
        /// </summary>
        private DispatcherTimer updateTimer;

        private bool needLogout = true;

        #endregion Fields

        private void DynamicLoadStyles()
        {
       
           string fileName;

            fileName = "C:\\Project\\AMS\\ams_win_client\\AMSClient\\Dictionaries\\StringDictionary.xaml";

            if (File.Exists(fileName))
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                {
                    // Read in ResourceDictionary File
                    ResourceDictionary dic =
                       (ResourceDictionary)XamlReader.Load(fs);
                    // Clear any previous dictionaries loaded
                    Resources.MergedDictionaries.Clear();
                    // Add in newly loaded Resource Dictionary
                    Resources.MergedDictionaries.Add(dic);
                }
            }
        }
        public LoginWindow()
        {

        }

        private void LoginWindow_KeyDown(object sender, KeyEventArgs e)
        {
           if(e.Key==Key.Enter)
           {
               UserLogin();
           }
            if(e.Key==Key.Escape)
            {
                Close();
            }
        }

        /// <summary>
        /// Загружает сертификаты пользователя
        /// </summary>
        private void GetCertificates()
        {
            var store = new X509Store("My", StoreLocation.CurrentUser);
            store.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadOnly);
            certListBox.DataContext = store.Certificates;
        }
        /// <summary>
        /// Проверяем, что другой экземпляр данного приложения не запущен
        /// </summary>
        private void CheckOnlyOneInstance()
        {
            if (!onlyInstance)
            {
                
                MessageBox.Show(DataHelper.GetStringFromDictionary("StringDictionary", "AlreadyStarted"));
                Environment.Exit(1);
            }
        }

        private void entryButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UserLogin()
        {

        }

        /// <summary>
        /// Выполнить вход в систему, запуск в потоке входа в систему
        /// </summary>
        private void Login()
        {
          
        }

        /// <summary>
        /// Сохранить имя пользователя и пароль на диске
        /// </summary>
        private void SavePassword()
        {

        }
        /// <summary>
        /// Удалить имя пользователя и пароль с диска
        /// </summary>
        private void DeletePassword()
        {

        }

        /// <summary>
        /// Понимаем какой интерфейс является локальным
        /// </summary>
        private void GetLocalIpAddress()
        {

        }

        /// <summary>
        /// Найден локальный ip address для работы с системой, он будет использован при логине
        /// </summary>
        /// <param name="message">Локальный ip address</param>
        void addressResolution_CompleteGetLocalIp(string message)
        {
            ipAddress = message;
            this.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate()
            {
                entryButton.IsEnabled = true;
            });
        }

        /// <summary>

        private void workWind_Closing(object sender, CancelEventArgs e)
        {

        }

        private void workWind_Logout(object sender, EventArgs e)
        {
         
        }

        private void certificateRBtn_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void passwordRBtn_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Settings.Instance.LoadSettingsFromConfig();
            //try
            //{
            //    Settings.Instance.LoadSettingsFromDataBase();
            //    Login();
            //}
            //catch (Exception)
            //{

            //}
        }

        private void checkSavePassword_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void checkSavePassword_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
