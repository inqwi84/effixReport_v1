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
using EffixReportSystem.Views.MassMedia.ViewModels;
using Telerik.Windows.Controls;

namespace EffixReportSystem.Views.MassMedia.Views
{
    /// <summary>
    /// Interaction logic for ViewMassMediaView.xaml
    /// </summary>
    public partial class ViewMassMediaView : UserControl
    {
        public ViewMassMediaView()
        {
            InitializeComponent();
        }

        private void ClearSearchTextBox(object sender, RoutedEventArgs e)
        {

        }

        private void RadWatermarkTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PublicationListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            var ctx = DataContext as ViewMassMediaViewModel;
            ctx.SaveSmi();
            SMIDescription.IsEnabled = false;
            SMIEdition.IsEnabled = false;
            SMIEditionDescr.IsEnabled = false;
            SMIName.IsEnabled = false;
            SMIUrl.IsEnabled = false;
            EditButton.Visibility = Visibility.Visible;
            DoneButton.Visibility = Visibility.Collapsed;
            CancelButton.Visibility = Visibility.Collapsed;
        }

        private void EditPublicationButton_Click(object sender, RoutedEventArgs e)
        {
            SMIDescription.IsEnabled = true;
            SMIEdition.IsEnabled = true;
            SMIEditionDescr.IsEnabled = true;
            SMIName.IsEnabled = true;
            SMIUrl.IsEnabled = true;
            EditButton.Visibility=Visibility.Collapsed;
            DoneButton.Visibility=Visibility.Visible;
            CancelButton.Visibility=Visibility.Visible;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var ctx = DataContext as ViewMassMediaViewModel;
            ctx.RestoreSmi();
            SMIDescription.IsEnabled = false;
            SMIEdition.IsEnabled = false;
            SMIEditionDescr.IsEnabled = false;
            SMIName.IsEnabled = false;
            SMIUrl.IsEnabled = false;
            EditButton.Visibility = Visibility.Visible;
            DoneButton.Visibility = Visibility.Collapsed;
            CancelButton.Visibility = Visibility.Collapsed;
        }

        private void NewPublicationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemovePublicationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RadContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            var ctx = DataContext as ViewMassMediaViewModel;
            RadTreeViewItem clickedItemContainer = radContextMenu.GetClickedElement<RadTreeViewItem>();
            if (clickedItemContainer != null)
            {
                var clickedItem = clickedItemContainer.Item as EF_MassMedia;

                if (clickedItem != null)
                {
                    ctx.PreapreContextOperationsForItem(clickedItem);
                }
            }
            else
            {
                ctx.PreapreContextOperationsForItem(null);
            }
        }
    }
}
