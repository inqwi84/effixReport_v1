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
using CommonLibraries.Log;
using EffixReportSystem.Helper.Classes.Enums;

namespace EffixReportSystem.Views.MassMedia.Controls
{
    /// <summary>
    /// Interaction logic for AddMassMediaWindow.xaml
    /// </summary>
    public partial class AddMassMediaWindow : Window
    {
        private readonly EF_MassMedia _massMediaDepartment;
        private readonly ViewMode _mode;
        public AddMassMediaWindow(EF_MassMedia currentMassMedia, ViewMode mode)
        {
            InitializeComponent();
            SMIName.Text = String.Empty;
            _massMediaDepartment = currentMassMedia;
            _mode = mode;
            if (mode == ViewMode.EditMode)
            {
                SMIName.Text = _massMediaDepartment.Mass_media_type_name;
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void doneButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_mode == ViewMode.NewMode)
                {
                    var name = SMIName.Text;
                    using (var model = new EntitiesModel())
                    {
                        var id = model.EF_MassMedias.Max(item => item.Mass_media_type_id) + 1;
                        model.Add(new EF_MassMedia
                        {
                            Mass_media_type_id = id,
                            Children = new List<EF_MassMedia>(),
                            Parent_type_id = _massMediaDepartment.Mass_media_type_id,
                            Mass_media_type_name = name
                        });
                        model.SaveChanges();
                    }
                }
                else
                {
                    var name = SMIName.Text;
                    using (var model = new EntitiesModel())
                    {
                        var dept =
                            model.EF_MassMedias.FirstOrDefault(
                                item => item.Mass_media_type_id == _massMediaDepartment.Mass_media_type_id);
                        if (dept != null) dept.Mass_media_type_name = name;
                        model.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.TraceError(ex.Message);
            }      
            this.Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
