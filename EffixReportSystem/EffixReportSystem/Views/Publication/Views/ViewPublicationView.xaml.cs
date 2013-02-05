using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using CommonLibraries.Log;
using EffixReportSystem.Helper.Classes;
using EffixReportSystem.Views.Publication.Controls;
using EffixReportSystem.Views.Publication.ViewModels;
using Telerik.Windows.Controls;

namespace EffixReportSystem.Views.Publication.Views
{
    /// <summary>
    /// Interaction logic for ViewPublicationView.xaml
    /// </summary>
    public partial class ViewPublicationView : UserControl
    {
        public ViewPublicationView()
        {
            InitializeComponent();
        }

        private void RadWatermarkTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var ctx = DataContext as ViewPublicationViewModel;
                if (ctx != null) ctx.FilterString = (sender as TextBox).Text;
            }
            catch (Exception ex)
            {
                Logger.TraceError(ex.Message);
            }
        }

        private void ClearSearchTextBox(object sender, RoutedEventArgs e)
        {
          
        }

        private void EditPublicationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ctx = DataContext as ViewPublicationViewModel;
                if (ctx == null) return;
                (ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel = (ctx.ParentViewModel as PublicationViewModel).PageViewModels[1];
                ((ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel as EditPublicationViewModel).SetCurrentPublication(ctx.CurrentPublication.Publication_id);
            ((ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel as EditPublicationViewModel).GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.TraceError(ex.Message);
            }

        }

        private void NewPublicationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ctx = DataContext as ViewPublicationViewModel;
                if (ctx == null) return;
                (ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel = (ctx.ParentViewModel as PublicationViewModel).PageViewModels[2];
                var newPublicationViewModel = ((ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel as NewPublicationViewModel);
                newPublicationViewModel.GetData();
                newPublicationViewModel.CurrentDepartment = ctx.CurrentDepartment;
                newPublicationViewModel.ImageTileList=new ObservableCollection<DataHelper.ImageTile>();
                newPublicationViewModel.CurrentPublication = new EF_Publication(ctx.CurrentDepartment);
                newPublicationViewModel.CurrentProjectName = ctx.CurrentProjectName;
                newPublicationViewModel.CurrentProject = ctx.CurrentProject;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.TraceError(ex.Message);
            }
       }

        private void RemovePublicationButton_Click(object sender, RoutedEventArgs e)
        {
            var ctx = DataContext as ViewPublicationViewModel;
            if (ctx == null) return;
            ctx.RemoveCurrentPublication();
        }

        private void AddDepartmentItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ctx = DataContext as ViewPublicationViewModel;
                if (ctx == null) return;
                var currentDepartment = ctx.CurrentDepartment;
                switch (currentDepartment.Department_type.Trim())
                {
                    case "all":
                        var projectWindow = new AddProjectWindow();
                        projectWindow.DoneButton.Click += DoneButton_Click;
                        projectWindow.DataContext = new AddProjectViewModel();
                        projectWindow.ShowDialog();
                        break;
                    case "year":
                        var monthWindow = new AddMonthWindow();
                        monthWindow.ShowDialog();
                        break;
                    case "month":
                        var dayWindow = new AddDayWindow();
                        dayWindow.ShowDialog();
                        break;
                    case "project":
                        var yearWindow = new AddYearWindow();
                        yearWindow.ShowDialog();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.TraceError(ex.Message);
            }
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
           //обновить дерево
        }

        private void RemoveDepartmentMenuItem_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void RenameDepartmentMenuItem_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void TreeViewSelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            var ctx = DataContext as ViewPublicationViewModel;
            if (ctx == null) return;
            ctx.CurrentDepartment = (sender as RadTreeView).SelectedItem as EF_Department;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
