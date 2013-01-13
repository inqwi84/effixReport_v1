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
using System.Windows.Shapes;

namespace EffixReportSystem.Views.Publication.Controls
{
    /// <summary>
    /// Interaction logic for AddProjectWindow.xaml
    /// </summary>
    public partial class AddProjectWindow : Window
    {
        private int _errors = 0;
        public AddProjectWindow()
        {
            InitializeComponent();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void doneButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                _errors++;
            else
                _errors--;
        }

        private void Confirm_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _errors == 0;
            e.Handled = true;
        }

        private void Confirm_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                using (var model = new EntitiesModel())
                {
                    var newProject = new EF_Project { Project_name = ProjectName.Text, Project_descr = ProjectDescr.Text, Project_id = model.EF_Projects.Max(item => item.Project_id) + 1 };
                    model.Add(newProject);
                    var newDepartment = new EF_Department
                    {
                        Department_type = "project",
                        Department_name = newProject.Project_name,
                        Department_description = newProject.Project_descr,
                        Department_project_id = newProject.Project_id,
                        Department_parent_id = 56,
                        Department_id = model.EF_Departments.Max(item => item.Department_id) + 1
                    };
                    model.Add(newDepartment);
                    model.SaveChanges();
                }
                this.Close();
            }
            catch (Exception)
            {

            }

        }
    }
}
