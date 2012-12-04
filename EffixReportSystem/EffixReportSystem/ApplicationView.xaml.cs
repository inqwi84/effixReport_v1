using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EffixReportSystem.Helper.Classes;
using EffixReportSystem.Helper.Interfaces;

namespace EffixReportSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ApplicationView : Window,IPageViewModel
    {
        public ApplicationView()
        {
            InitializeComponent();
            var t = "logo.png";
            var strUri2 = String.Format(@"pack://application:,,,/EffixReportSystem;component/Skins/Images/{0}", t);
            imgTitle.Source = new BitmapImage(new Uri(strUri2));
            txtTitle.Text = DataHelper.GetStringFromDictionary("StringDictionary", "bSystem");//"";
            txtTitleDetails.Text = DataHelper.GetStringFromDictionary("StringDictionary", "sNotification");//"";
            DataContext = new ApplicationViewModel();
        }

        #region Events

        #region Windows Events

        /// <summary>
        /// Свернуть окно по нажатию на кнопку управления окном
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void minimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        /// <summary>
        /// Нажатие на кнопку разворачивания/сворачивания окна на весь экран
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void maximizeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Normal)
                this.WindowState = System.Windows.WindowState.Maximized;
            else
                this.WindowState = System.Windows.WindowState.Normal;
        }

        /// <summary>
        /// Закрыть окно по нажатию на кнопку закрытия окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Движение окна при перетаскивании мышью по любой части окна 
        /// и сворачивания из максимального состояния при перетаскивании в верхней части окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayoutRoot_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.PrimaryDevice.LeftButton == MouseButtonState.Pressed && e.OriginalSource.Equals(LayoutRoot))
            {
                if (this.WindowState == System.Windows.WindowState.Maximized && Mouse.PrimaryDevice.GetPosition(this).Y < 200)
                {
                    //this.WindowState = System.Windows.WindowState.Normal;
                    this.WindowState = System.Windows.WindowState.Maximized;
                    this.Top = 0;
                }
                this.DragMove();

            }
        }

        #endregion Windows Events

        #endregion Events

        private void PublicationRadioButton_Checked(object sender, RoutedEventArgs e)
        {
          
        }

        private void MassMediaRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void PersonRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void ReportRadioButton_Checked(object sender, RoutedEventArgs e)
        {
           
        }

        private void SettingsRadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
