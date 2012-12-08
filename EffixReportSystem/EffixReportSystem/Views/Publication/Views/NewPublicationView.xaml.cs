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
using EffixReportSystem.Views.Publication.ViewModels;

namespace EffixReportSystem.Views.Publication.Views
{
    /// <summary>
    /// Interaction logic for NewPublicationView.xaml
    /// </summary>
    public partial class NewPublicationView : UserControl
    {
        public NewPublicationView()
        {
            InitializeComponent();
        }

        private void RadWatermarkTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ClearSearchTextBox(object sender, RoutedEventArgs e)
        {

        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var ctx = DataContext as NewPublicationViewModel;
            (ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel = (ctx.ParentViewModel as PublicationViewModel).PageViewModels[0];
            //((ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel as NewPublicationViewModel).
            //    CurrentPublication = ctx.CurrentPublication;
        }
    }
}
