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
using EffixReportSystem.Helper.Classes.Enums;
using EffixReportSystem.Views.MassMedia.Controls;
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

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {

            var ctx = DataContext as ViewMassMediaViewModel;
            if (ctx.Mode == ViewMode.EditMode)
            {
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
            else
            {
                if (ctx.Mode == ViewMode.NewMode)
                {
                    ctx.SaveNewSmi();
                    SMIDescription.IsEnabled = false;
                    SMIEdition.IsEnabled = false;
                    SMIEditionDescr.IsEnabled = false;
                    SMIName.IsEnabled = false;
                    SMIUrl.IsEnabled = false;
                    EditButton.Visibility = Visibility.Visible;
                    DoneButton.Visibility = Visibility.Collapsed;
                    CancelButton.Visibility = Visibility.Collapsed; 
                }
            }
        }

        private void EditPublicationButton_Click(object sender, RoutedEventArgs e)
        {
            var ctx = DataContext as ViewMassMediaViewModel;
            ctx.Mode=ViewMode.EditMode;
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
            var ctx = DataContext as ViewMassMediaViewModel;
            ctx.Mode=ViewMode.NewMode;
            ctx.CurrentSmi=new EF_SMI {Mass_media_id = ctx.CurrentMassMediaDepartament.Mass_media_type_id};
            //ctx.CurrentSmi.EF_MassMedium = ctx.CurrentMassMediaDepartament;
            SMIDescription.Text = String.Empty;
            SMIDescription.IsEnabled = true;
            SMIEdition.Text = String.Empty;
            SMIEdition.IsEnabled = true;
            SMIEditionDescr.Text = String.Empty;
            SMIEditionDescr.IsEnabled = true;
            SMIName.Text = String.Empty;
            SMIName.IsEnabled = true;
            SMIUrl.Text = String.Empty;
            SMIUrl.IsEnabled = true;
            EditButton.Visibility=Visibility.Collapsed;
            DoneButton.Visibility=Visibility.Visible;
            CancelButton.Visibility=Visibility.Visible;
        }

        private void RemovePublicationButton_Click(object sender, RoutedEventArgs e)
        {

        }

       // private void RadContextMenu_Opened(object sender, RoutedEventArgs e)
        //{
        //    var ctx = DataContext as ViewMassMediaViewModel;
        //    RadTreeViewItem clickedItemContainer = radContextMenu.GetClickedElement<RadTreeViewItem>();
        //    if (clickedItemContainer != null)
        //    {
        //        var clickedItem = clickedItemContainer.Item as EF_MassMedia;

        //        if (clickedItem != null)
        //        {
        //            ctx.PreapreContextOperationsForItem(clickedItem);
        //        }
        //    }
        //    else
        //    {
        //        ctx.PreapreContextOperationsForItem(null);
        //    }
       // }
        private void AddMassMediaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var ctx = DataContext as ViewMassMediaViewModel;
            if (ctx.CurrentMassMediaDepartament != null)
            {
                var addMassMediaWindow = new AddMassMediaWindow(ctx.CurrentMassMediaDepartament, ViewMode.NewMode);
                addMassMediaWindow.ShowDialog();
                ctx.ReloadMassMedia();
            }
          
        }

        private void RemoveMassMediaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var ctx = DataContext as ViewMassMediaViewModel;
            if (ctx.CurrentMassMediaDepartament != null)
            {
                ctx.RemoveMassMedia();
            }
        }

        private void RenameMassMediaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var ctx = DataContext as ViewMassMediaViewModel;
            if (ctx.CurrentMassMediaDepartament != null)
            {
                var addMassMediaWindow = new AddMassMediaWindow(ctx.CurrentMassMediaDepartament,ViewMode.EditMode);
                addMassMediaWindow.ShowDialog();
                ctx.ReloadMassMedia();
            }
        }
    }
}
