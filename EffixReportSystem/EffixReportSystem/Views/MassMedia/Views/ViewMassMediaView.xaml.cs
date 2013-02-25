using System;
using System.Windows;
using System.Windows.Controls;
using CommonLibraries.Log;
using EffixReportSystem.Helper.Classes.Enums;
using EffixReportSystem.Views.MassMedia.Controls;
using EffixReportSystem.Views.MassMedia.ViewModels;

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
                try
                {
                    ctx.SaveSmi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Logger.TraceError(ex.Message);
                }
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
                    try
                    {
                        ctx.SaveNewSmi();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        Logger.TraceError(ex.Message);
                    }
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

            if (ctx != null)
            {
                try
                {
                    ctx.RestoreSmi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Logger.TraceError(ex.Message);
                }
            }
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
            try
            {
                var ctx = DataContext as ViewMassMediaViewModel;
                if (ctx != null)
                {
                    ctx.Mode = ViewMode.NewMode;
                    ctx.CurrentSmi = new EF_SMI { Mass_media_id = ctx.CurrentMassMediaDepartament.Mass_media_type_id };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.TraceError(ex.Message);
            }
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
            EditButton.Visibility = Visibility.Collapsed;
            DoneButton.Visibility = Visibility.Visible;
            CancelButton.Visibility = Visibility.Visible;
        }

        private void RemovePublicationButton_Click(object sender, RoutedEventArgs e)
        {
            var ctx = DataContext as ViewMassMediaViewModel;
            ctx.RemoveMassMedia();
        }

        private void AddMassMediaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var ctx = DataContext as ViewMassMediaViewModel;
            if (ctx == null || ctx.CurrentMassMediaDepartament == null) return;
            try
            {
                var addMassMediaWindow = new AddMassMediaWindow(ctx.CurrentMassMediaDepartament, ViewMode.NewMode);
                addMassMediaWindow.ShowDialog();
                ctx.ReloadMassMedia();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.TraceError(ex.Message);
            }
        }

        private void RemoveMassMediaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var ctx = DataContext as ViewMassMediaViewModel;
            if (ctx == null || ctx.CurrentMassMediaDepartament == null) return;
            try
            {
                ctx.RemoveMassMedia();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.TraceError(ex.Message);
            }
        }

        private void RenameMassMediaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var ctx = DataContext as ViewMassMediaViewModel;
            if (ctx == null || ctx.CurrentMassMediaDepartament == null) return;
            try
            {
                var addMassMediaWindow = new AddMassMediaWindow(ctx.CurrentMassMediaDepartament, ViewMode.EditMode);
                addMassMediaWindow.ShowDialog();
                ctx.ReloadMassMedia();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.TraceError(ex.Message);
            }
        }
    }
}
