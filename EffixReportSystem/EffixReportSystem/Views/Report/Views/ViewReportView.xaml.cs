﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
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
using CommonLibraries.Log;
using EffixReportSystem.Helper.Classes;
using EffixReportSystem.Helper.Classes.Report;
using EffixReportSystem.Views.Report.ReportTemplates;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using Telerik.Reporting;

namespace EffixReportSystem.Views.Report.Views
{
    /// <summary>
    /// Interaction logic for ViewReportView.xaml
    /// </summary>
    public partial class ViewReportView : UserControl
    {
        private int index = 0;
        private DateTime bDate;
        private DateTime eDate;
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
            switch (index)
            {
                //не выбрано
                case 0:
                    MessageBox.Show("Не выбран проект");
                    break;
                //лифан
                case 1:
                 rootBook= MakeReport("lifan", bDate, eDate);
                    break;
                //артекс ягуар
                case 2:
                    rootBook = MakeReport("arteks-jaguar", bDate, eDate);
                    break;
                //артекс хюндай
                case 3:
                    rootBook = MakeReport("arteks-hyundai", bDate, eDate);
                    break;
                //авторусь
                case 4:
                    rootBook = MakeReport("avtorus", bDate, eDate);
                    break;
                //автоалея ХОнда
                case 5:
                    rootBook = MakeReport("avtoalea-honda", bDate, eDate);
                    break;
            }
            //var rBook = new ReportBook();
            //var report1 = new HeadReport();
            //rBook.Reports.Add(report1);
            //using (var model = new EntitiesModel())
            //{
            //    var initiatedPublications = model.EF_Publications.Where(item => item.Is_initiated == 1).GroupBy(gr => gr.EF_SMI.EF_SMI_Type.Smi_type_name);
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
            //            if (notInit == 0)
            //            {
            //                var rpr = new ClippingReport(efPublication,
            //                                             efPublication.EF_SMI.EF_SMI_Type.Smi_type_name);
            //                rBook.Reports.Add(rpr);
            //                notInit++;
            //            }
            //            else
            //            {
            //                var rpr = new ClippingReport(efPublication);
            //                rBook.Reports.Add(rpr);
            //            }
            //        }
            //        notInit = 0;
            //    }
            //}
            //rBook.Reports.Add(new DiagramReport());
            //rootBook = rBook;
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
            if (bw.IsBusy != true)
            {
                index = ProjectsComboBox.SelectedIndex;
                bDate = BeginPeriod.SelectedDate.Value;
                eDate = EndPeriod.SelectedDate.Value;
                bw.RunWorkerAsync();
            }
            //var rBook = new ReportBook();
            //var report1 =new HeadReport();
            //rBook.Reports.Add(report1);
            //using (var model=new EntitiesModel())
            //{
            //    foreach (var report in model.EF_Publications.Select(publication => new ClippingReport(publication)))
            //    {
            //        rBook.Reports.Add(report);
            //    }
            //}
            //rBook.Reports.Add(new DiagramReport());
            //reportViewer.ReportSource = rBook;
            //switch (ProjectsComboBox.SelectedIndex)
            //{
            //        //не выбрано
            //    case 0:
            //        MessageBox.Show("Не выбран проект");
            //        break;
            //        //лифан
            //    case 1:
            //        Task.Factory.StartNew(() =>
            //            {
            //                MakeReport("lifan", BeginPeriod.SelectedDate.Value, EndPeriod.SelectedDate.Value);
            //            });
            //        break;
            //        //артекс ягуар
            //    case 2:
            //        Task.Factory.StartNew(() =>
            //        {
            //            MakeReport("arteks-jaguar", BeginPeriod.SelectedDate.Value, EndPeriod.SelectedDate.Value);
            //        });
            //        break;
            //        //артекс хюндай
            //    case 3:
            //        Task.Factory.StartNew(() =>
            //        {
            //            MakeReport("arteks-hyundai", BeginPeriod.SelectedDate.Value, EndPeriod.SelectedDate.Value);
            //        });
            //        break;
            //        //авторусь
            //    case 4:
            //        Task.Factory.StartNew(() =>
            //        {
            //            MakeReport("avtorus", BeginPeriod.SelectedDate.Value, EndPeriod.SelectedDate.Value);
            //        });
            //        break;
            //        //автоалея ХОнда
            //    case 5:
            //        Task.Factory.StartNew(() =>
            //        {
            //            MakeReport("avtoalea-honda", BeginPeriod.SelectedDate.Value, EndPeriod.SelectedDate.Value);
            //        });
            //        break;
            //}
        }

        private ReportBook MakeReport(string projName, DateTime beginPeriod, DateTime endPeriod)
        {
            var rBook = new ReportBook();
            var report1 = new HeadReport();
            rBook.Reports.Add(report1);
            using (var model=new EntitiesModel())
            {
                var tst =
                    model.EF_Publications.Where(
                        item =>
                        item.Project_name.ToLower().Contains(projName.ToLower()) && item.Publication_date.Value >= beginPeriod &&
                        item.Publication_date <= endPeriod);
                foreach (var item in model.EF_Publications.Where(item => item.Project_name.Equals(projName)&&item.Publication_date>=beginPeriod&&item.Publication_date<=endPeriod))
                {
                    var imageColl= new ObservableCollection<Bitmap>();
                    imageColl = GetImageCollection(item);
                    foreach (var bitmapImage in imageColl)
                    {
                        var rpr = new ClippingReport_v2(bitmapImage,item);
                        rBook.Reports.Add(rpr);
                    }
                }
            }
            rBook.Reports.Add(new DiagramReport());
           // reportViewer.ReportSource = rBook;
            return rBook;
        }
        private ObservableCollection<Bitmap>GetImageCollection(EF_Publication publication)
        {
            var result = GetBlobFromStorage(publication.Blob_path,publication.Image_count.Value,publication.Project_name);
            return result;
        }
        private ObservableCollection<Bitmap> GetBlobFromStorage(string blobPath, int imageCount, string projectName)
        {
            var result = new ObservableCollection<Bitmap>();
            //var splitPath = blobPath.Split(Convert.ToChar("\\"));
            for (var i = 0; i < imageCount; i++)
            {
                try
                {
                    var extension = blobPath.LastIndexOf("_", StringComparison.Ordinal);
                    var filePath = blobPath.Remove(extension + 1) + i + ".png";
                    var storageAccount = CloudStorageAccount.Parse(
                        "DefaultEndpointsProtocol=http;AccountName=ctx;AccountKey=rCaek5ugmLbIaL2mXk3gaqMF4mzqPrUu6CXBUsXn1yrTdWBBTBsQpA2bDuyDC6BQx1NCeUhEl6p0vWT69ZNF+Q==");

                    // Create the blob client.
                    var blobClient = storageAccount.CreateCloudBlobClient();

                    // Retrieve reference to a previously created container.
                    var container = blobClient.GetContainerReference(projectName);

                    // Retrieve reference to a blob named "photo1.jpg".
                    var tmpStr = filePath.Remove(0, filePath.IndexOf(projectName, StringComparison.Ordinal) + projectName.Length);
                    var blockBlob = container.GetBlockBlobReference(tmpStr);

                    var imageArray = blockBlob.DownloadByteArray();
                    TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
                    Bitmap bitmap1 = (Bitmap)tc.ConvertFrom(imageArray);
                   // var image = ImageFromBuffer(imageArray);
                    result.Add(bitmap1);
                }
                catch (Exception ex)
                {
                    Logger.TraceError(ex.Message);
                }
            }
            // Retrieve storage account from connection string.
            return result;
        }

        public BitmapImage ImageFromBuffer(Byte[] bytes)
        {
            var stream = new MemoryStream(bytes);
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();
            return image;
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (bw.IsBusy != true)
            {
                bw.RunWorkerAsync();
            }
        }
    }
}
