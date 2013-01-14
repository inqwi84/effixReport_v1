using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private BackgroundWorker bw = new BackgroundWorker();
        ReportBook rootBook = new ReportBook();
        public ViewReportView()
        {
            InitializeComponent();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            var rBook = new ReportBook();
            var report1 = new HeadReport();
            rBook.Reports.Add(report1);
            using (var model = new EntitiesModel())
            {
                var initiatedPublications = model.EF_Publications.Where(item => item.Is_initiated == 1).GroupBy(gr => gr.EF_SMI.EF_SMI_Type.Smi_type_name);
                var notInitiatedPublications = model.EF_Publications.Where(item => item.Is_initiated == 0).GroupBy(gr => gr.EF_SMI.EF_SMI_Type.Smi_type_name);
                int init = 0;
                int notInit = 0;
                foreach (var grouped in initiatedPublications)
                {
                    foreach (var efPublication in grouped)
                    {
                        if (init == 0)
                        {
                            var rpr = new ClippingReport(efPublication,
                                                         efPublication.EF_SMI.EF_SMI_Type.Smi_type_name);
                            rBook.Reports.Add(rpr);
                            init++;
                        }
                        else
                        {
                            var rpr = new ClippingReport(efPublication);
                            rBook.Reports.Add(rpr);
                        }
                    }
                    init = 0;
                }
                foreach (var grouped in notInitiatedPublications)
                {
                    foreach (var efPublication in grouped)
                    {
                        if (notInit == 0)
                        {
                            var rpr = new ClippingReport(efPublication,
                                                         efPublication.EF_SMI.EF_SMI_Type.Smi_type_name);
                            rBook.Reports.Add(rpr);
                            notInit++;
                        }
                        else
                        {
                            var rpr = new ClippingReport(efPublication);
                            rBook.Reports.Add(rpr);
                        }
                    }
                    notInit = 0;
                }
            }
            rBook.Reports.Add(new DiagramReport());
            rootBook = rBook;
        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            reportViewer.ReportSource = rootBook;
            if ((e.Cancelled == true))
            {
               // this.tbProgress.Text = "Canceled!";
            }

            else if (!(e.Error == null))
            {
               // this.tbProgress.Text = ("Error: " + e.Error.Message);
            }

            else
            {
              //  this.tbProgress.Text = "Done!";
            }
        }
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
           // this.tbProgress.Text = (e.ProgressPercentage.ToString() + "%");
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
            //var rBook = new ReportBook();
            //var report1 = new HeadReport();
            //rBook.Reports.Add(report1);
            //using (var model = new EntitiesModel())
            //{
            //    var initiatedPublications = model.EF_Publications.Where(item => item.Is_initiated == 1).GroupBy(gr=>gr.EF_SMI.EF_SMI_Type.Smi_type_name);
            //    var notInitiatedPublications = model.EF_Publications.Where(item => item.Is_initiated == 0).GroupBy(gr => gr.EF_SMI.EF_SMI_Type.Smi_type_name); 
            //    int init = 0;
            //    int notInit = 0;
            //    foreach (var grouped in initiatedPublications)
            //    {
            //        foreach (var efPublication in grouped)
            //        {
            //            if (init == 0)
            //            {
            //                var rpr = new ClippingReport(efPublication,
            //                                             efPublication.EF_SMI.EF_SMI_Type.Smi_type_name);
            //                rBook.Reports.Add(rpr);
            //                init++;
            //            }
            //            else
            //            {
            //                var rpr = new ClippingReport(efPublication);
            //                rBook.Reports.Add(rpr);
            //            }
            //        }
            //        init = 0;
            //    }
            //    foreach (var grouped in notInitiatedPublications)
            //    {
            //        foreach (var efPublication in grouped)
            //        {
            //              if (notInit == 0)
            //              {
            //                  var rpr = new ClippingReport(efPublication,
            //                                               efPublication.EF_SMI.EF_SMI_Type.Smi_type_name);
            //                  rBook.Reports.Add(rpr);
            //                  notInit++;
            //              }
            //              else
            //              {
            //                  var rpr = new ClippingReport(efPublication);
            //                  rBook.Reports.Add(rpr);
            //              }
            //        }
            //        notInit = 0;
            //    }
            //}
            //rBook.Reports.Add(new DiagramReport());
            //reportViewer.ReportSource = rBook;
            if (bw.IsBusy != true)
            {
                bw.RunWorkerAsync();
            }
        }
    }
}
