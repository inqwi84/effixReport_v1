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
            ((ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel as EditPublicationViewModel).
                CurrentPublication = ctx.CurrentPublication;
        }

        private void NewPublicationButton_Click(object sender, RoutedEventArgs e)
        {
            var ctx = DataContext as ViewPublicationViewModel;
            (ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel = (ctx.ParentViewModel as PublicationViewModel).PageViewModels[2];
            ((ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel as NewPublicationViewModel).
                CurrentPublication = new EF_Publication();
       }

        private void RemovePublicationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ProjectsTreeView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ctx = DataContext as ViewPublicationViewModel;
            var radTreeView = sender as RadTreeView;
            if (radTreeView != null)
            {
                var selectedItem = radTreeView.SelectedItem;
                if (selectedItem != null)
                {
                    var project = selectedItem as EF_Project;
                    if(project!=null)
                    {
                        ctx.PublicationList =new ObservableCollection<EF_Publication>(ctx.AllPublications.Where(item => item.Project_id == project.Project_id));
                    }
                    var year = selectedItem as Year;
                    if (year != null)
                    {
                        ctx.PublicationList = new ObservableCollection<EF_Publication>(ctx.AllPublications.Where(item => item.P_year == year.Name&&item.Project_id==year.Parent.Project_id));
                    }
                    var month = selectedItem as Month;
                    if (month != null)
                    {
                        ctx.PublicationList = new ObservableCollection<EF_Publication>(ctx.AllPublications.Where(item => item.P_month == month.Name && item.P_year == month.Parent.Name && item.Project_id == month.Parent.Parent.Project_id));
                    }
                    var day = selectedItem as Day;
                    if (day != null)
                    {
                        ctx.PublicationList = new ObservableCollection<EF_Publication>(ctx.AllPublications.Where(item =>item.P_day==day.Name&& item.P_month == day.Parent.Name && item.P_year == day.Parent.Parent.Name && item.Project_id == day.Parent.Parent.Parent.Project_id));
                    }
                }
            }
        }

        private void ComboBox_DropDownOpened(object sender, EventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
