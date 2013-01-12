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
using EffixReportSystem.Helper.Classes.Report;
using EffixReportSystem.Views.Report.ReportTemplates;
using Telerik.Reporting;

namespace EffixReportSystem.Views.Report.Views
{
    /// <summary>
    /// Interaction logic for ViewReportView.xaml
    /// </summary>
    public partial class ViewReportView : UserControl
    {
        public ViewReportView()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var rBook = new ReportBook();
            var report1 =new HeadReport();
            rBook.Reports.Add(report1);
            using (var model=new EntitiesModel())
            {
                foreach (var report in model.EF_Publications.Select(publication => new ClippingReport(publication)))
                {
                    rBook.Reports.Add(report);
                }
            }
            rBook.Reports.Add(new DiagramReport());
            reportViewer.ReportSource = rBook;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var report = new HeadReport();
            reportViewer.ReportSource = report;
        }
    }
}
