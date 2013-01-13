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
            var ctx = DataContext as ViewPublicationViewModel;
            ctx.FilterString = (sender as TextBox).Text;
        }

        private void ClearSearchTextBox(object sender, RoutedEventArgs e)
        {
          
        }

        //
        private void PublicationListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ctx = DataContext as ViewPublicationViewModel;
            if (ctx != null) ctx.CurrentPublication = (sender as RadListBox).SelectedItem as EF_Publication;
        }


        private void EditPublicationButton_Click(object sender, RoutedEventArgs e)
        {
            var ctx = DataContext as ViewPublicationViewModel;
            (ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel = (ctx.ParentViewModel as PublicationViewModel).PageViewModels[1];
            ((ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel as EditPublicationViewModel).SetCurrentPublication(ctx.CurrentPublication.Publication_id);
        }

        private void NewPublicationButton_Click(object sender, RoutedEventArgs e)
        {
            var ctx = DataContext as ViewPublicationViewModel;
            (ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel = (ctx.ParentViewModel as PublicationViewModel).PageViewModels[2];
            var newPublicationViewModel=((ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel as NewPublicationViewModel);
            newPublicationViewModel.CurrentDepartment = ctx.CurrentDepartament;
            newPublicationViewModel.CurrentPublication = new EF_Publication(ctx.CurrentDepartament);
            newPublicationViewModel.CurrentProjectName = ctx.CurrentProjectName;
            newPublicationViewModel.CurrentProject = ctx.CurrentProject;

        }

        private void RemovePublicationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddDepartmentItem_Click(object sender, RoutedEventArgs e)
        {
            var ctx = DataContext as ViewPublicationViewModel;
            if (ctx == null) return;
            var currentDepartment = ctx.CurrentDepartament;
            switch (currentDepartment.Department_type.Trim())
            {
                case "all":
                    var projectWindow = new AddProjectWindow();
                    projectWindow.DoneButton.Click+=DoneButton_Click;
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
    }
}
