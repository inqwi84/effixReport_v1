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
                    rootBook = MakeLifan_Report("lifan", bDate, eDate);
                    break;
                //артекс ягуар
                case 2:
                    rootBook = MakeAvtoALEA_JLR_Report("arteks-jaguar", bDate, eDate);
                    break;
                //артекс хюндай
                case 3:
                    rootBook = MakeAvtoALEA_JLR_Report("arteks-hyundai", bDate, eDate);
                    break;
                //авторусь
                case 4:
                    rootBook = MakeAvtoALEA_JLR_Report("avtorus", bDate, eDate);
                    break;
                //автоалея ХОнда
                case 5:
                    rootBook = MakeAvtoALEA_JLR_Report("avtoalea-honda", bDate, eDate);
                    break;
                case 6:
                    rootBook = MakeAvtoALEA_JLR_Report("avtoalea-jaguar", bDate, eDate);
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
        }
        private ReportBook MakeAvtoALEA_JLR_Report(string projName, DateTime beginPeriod, DateTime endPeriod)
        {
            var rBook = new ReportBook();
            using (var model = new EntitiesModel())
            {
                var resultList = new List<List<EF_Publication>>();
                var resultNameList = new List<string>();

                var pList = model.EF_Publications.Where(
                    item =>
                    item.Project_name.Equals(projName) && item.Publication_date >= beginPeriod &&
                    item.Publication_date <= endPeriod);
                var pGroupList = pList.GroupBy(item => item.EF_SMI.EF_MassMedium.Mass_media_type_name);

                //по каждой группе
                foreach (var group in pGroupList)
                {
                   resultList.Add(group.ToList()
                                    .OrderBy(item => item.Publication_date)
                                    .ThenBy(item => item.EF_SMI.Smi_name).ToList());
                   resultNameList.Add(group.Key);
                }
                int gIndex = 1;
                foreach (var rst in resultList)
                {
                    var ind = resultList.IndexOf(rst);
                    var report1 = new AvtoAleaJLRHeadReport(rst, resultNameList[ind],gIndex);
                    rBook.Reports.Add(report1);
                    gIndex += rst.Count;
                }

                foreach (var gList in resultList)
                {
                    var id = resultList.IndexOf(gList);
                    rBook.Reports.Add(new GroupPageReport(resultNameList[id]));
                    foreach (var efPublication in gList)
                    {
                        {
                            var imageColl = new ObservableCollection<Bitmap>();
                            imageColl = GetImageCollection(efPublication);
                            int index = 1;
                            foreach (var bitmapImage in imageColl)
                            {
                                if (index == imageColl.Count)
                                {
                                    var rpr = new ClippingReport_v2(bitmapImage, efPublication, true);
                                    rBook.Reports.Add(rpr);
                                }
                                else
                                {
                                    var rpr = new ClippingReport_v2(bitmapImage, efPublication, false);
                                    rBook.Reports.Add(rpr);
                                }
                                index++;
                            }
                        }
                    }
                }

                //Тональность //1
                rBook.Reports.Add(
                    new ExclusivityReport(
                        model.EF_Publications.Where(
                            item =>
                            item.Project_name.Equals(projName) && item.Publication_date >= beginPeriod &&
                            item.Publication_date <= endPeriod))); ;
                //Эксклюзивность
                rBook.Reports.Add(
                    new HasPhotoReport(
                        model.EF_Publications.Where(
                            item =>
                            item.Project_name.Equals(projName) && item.Publication_date >= beginPeriod &&
                            item.Publication_date <= endPeriod))); ;
                //Фотография
                rBook.Reports.Add(
                    new DiagramReport(
                        model.EF_Publications.Where(
                            item =>
                            item.Project_name.Equals(projName) && item.Publication_date >= beginPeriod &&
                            item.Publication_date <= endPeriod), "", "", ""));
                //Инициированные
                rBook.Reports.Add(
                    new InitiatedReport(
                        model.EF_Publications.Where(
                            item =>
                            item.Project_name.Equals(projName) && item.Publication_date >= beginPeriod &&
                            item.Publication_date <= endPeriod))); ;
                //Запланированные
                rBook.Reports.Add(
                    new PlannedReport(
                        model.EF_Publications.Where(
                            item =>
                            item.Project_name.Equals(projName) && item.Publication_date >= beginPeriod &&
                            item.Publication_date <= endPeriod)));
                //Приоритет
                if (projName.Contains("arteks"))
                {
                    rBook.Reports.Add(
                        new PriorityReport(
                            model.EF_Publications.Where(
                                item =>
                                item.Project_name.Equals(projName) && item.Publication_date >= beginPeriod &&
                                item.Publication_date <= endPeriod)));
                }
            }
            return rBook;
        }
        private ReportBook MakeLifan_Report(string projName, DateTime beginPeriod, DateTime endPeriod)
        {
            var rBook = new ReportBook();
            using (var model = new EntitiesModel())
            {
                    var allList = model.EF_Publications.Where(
                    item =>
                    item.Project_name.Equals(projName) && item.Publication_date >= beginPeriod &&
                    item.Publication_date <= endPeriod);


               var samIzdatList = allList.Where(item => item.EF_SMI.EF_MassMedium.Mass_media_type_name == "Самиздат").ToList();
               var initiatedGroups = allList.Where(item => item.Is_initiated == 1).OrderBy(item => item.Publication_date)
                                     .ThenBy(item => item.EF_SMI.Smi_name)
                           .GroupBy(item => item.EF_SMI.EF_MassMedium.Mass_media_type_name).OrderBy(item => item.Key);
               var nonInitiatedGroups =
                   allList.Where(item => item.Is_initiated == 0).OrderBy(item => item.Publication_date)
                                     .ThenBy(item => item.EF_SMI.Smi_name)
                           .GroupBy(item => item.EF_SMI.EF_MassMedium.Mass_media_type_name).OrderBy(item => item.Key);

                int idx = 1;
                foreach (var nonInitiatedGroup in nonInitiatedGroups)
                {
                        var report1 = new LifanHeadReport(nonInitiatedGroup.Key, nonInitiatedGroup.ToList(), idx);
                        rBook.Reports.Add(report1);
                        idx+=nonInitiatedGroup.Count();
                }
                foreach (var initiatedGroup in initiatedGroups)
                {
                    var report1 = new LifanHeadReport(initiatedGroup.Key, initiatedGroup.ToList(), idx);
                        rBook.Reports.Add(report1);
                        idx += initiatedGroup.Count();
                }
                var report12 = new LifanHeadReport("Самиздат", samIzdatList, idx);
                    rBook.Reports.Add(report12);


                    foreach (var nonInitiatedGroup in nonInitiatedGroups)
                    {
                        rBook.Reports.Add(new GroupPageReport(nonInitiatedGroup.Key));
                        foreach (var efPublication in nonInitiatedGroup)
                        {
                            {
                                var imageColl = new ObservableCollection<Bitmap>();
                                imageColl = GetImageCollection(efPublication);
                                int index = 1;
                                foreach (var bitmapImage in imageColl)
                                {
                                    if (index == imageColl.Count)
                                    {
                                        var rpr = new ClippingReport_v2(bitmapImage, efPublication, true);
                                        rBook.Reports.Add(rpr);
                                    }
                                    else
                                    {
                                        var rpr = new ClippingReport_v2(bitmapImage, efPublication, false);
                                        rBook.Reports.Add(rpr);
                                    }
                                    index++;
                                }
                            }
                        }
                    }
                    foreach (var initiatedGroup in initiatedGroups)
                    {
                        rBook.Reports.Add(new GroupPageReport(initiatedGroup.Key));
                        foreach (var efPublication in initiatedGroup)
                        {
                            {
                                var imageColl = new ObservableCollection<Bitmap>();
                                imageColl = GetImageCollection(efPublication);
                                int index = 1;
                                foreach (var bitmapImage in imageColl)
                                {
                                    if (index == imageColl.Count)
                                    {
                                        var rpr = new ClippingReport_v2(bitmapImage, efPublication, true);
                                        rBook.Reports.Add(rpr);
                                    }
                                    else
                                    {
                                        var rpr = new ClippingReport_v2(bitmapImage, efPublication, false);
                                        rBook.Reports.Add(rpr);
                                    }
                                    index++;
                                }
                            }
                        }
                    }

                //Тональность //1
                rBook.Reports.Add(
                    new ExclusivityReport(
                        model.EF_Publications.Where(
                            item =>
                            item.Project_name.Equals(projName) && item.Publication_date >= beginPeriod &&
                            item.Publication_date <= endPeriod))); ;
                //Эксклюзивность
                rBook.Reports.Add(
                    new HasPhotoReport(
                        model.EF_Publications.Where(
                            item =>
                            item.Project_name.Equals(projName) && item.Publication_date >= beginPeriod &&
                            item.Publication_date <= endPeriod))); ;
                //Фотография
                rBook.Reports.Add(
                    new DiagramReport(
                        model.EF_Publications.Where(
                            item =>
                            item.Project_name.Equals(projName) && item.Publication_date >= beginPeriod &&
                            item.Publication_date <= endPeriod), "", "", ""));
                //Инициированные
                rBook.Reports.Add(
                    new InitiatedReport(
                        model.EF_Publications.Where(
                            item =>
                            item.Project_name.Equals(projName) && item.Publication_date >= beginPeriod &&
                            item.Publication_date <= endPeriod))); ;
                //Запланированные
                rBook.Reports.Add(
                    new PlannedReport(
                        model.EF_Publications.Where(
                            item =>
                            item.Project_name.Equals(projName) && item.Publication_date >= beginPeriod &&
                            item.Publication_date <= endPeriod)));
                //Приоритет
                if (projName.Contains("arteks"))
                {
                    rBook.Reports.Add(
                        new PriorityReport(
                            model.EF_Publications.Where(
                                item =>
                                item.Project_name.Equals(projName) && item.Publication_date >= beginPeriod &&
                                item.Publication_date <= endPeriod)));
                }
            }
            //rBook.Reports.Add(new DiagramReport());
            //rBook.Reports.Add(new DiagramReport());
            // reportViewer.ReportSource = rBook;
            return rBook;
        }
        private ReportBook MakeReport(string projName, DateTime beginPeriod, DateTime endPeriod)
        {
            var rBook = new ReportBook();
            using (var model = new EntitiesModel())
            {
                var report1 = new HeadReport(projName, beginPeriod, endPeriod);
                rBook.Reports.Add(report1);
                foreach (
                    var item in
                        model.EF_Publications.Where(
                            item =>
                            item.Project_name.Equals(projName) && item.Publication_date >= beginPeriod &&
                            item.Publication_date <= endPeriod)
                             .OrderBy(dt => dt.Publication_date)
                             .ThenBy(nm => nm.EF_SMI.Smi_name))
                {
                    var imageColl = new ObservableCollection<Bitmap>();
                    imageColl = GetImageCollection(item);
                    int index = 1;
                    foreach (var bitmapImage in imageColl)
                    {
                        if (index == imageColl.Count)
                        {
                            var rpr = new ClippingReport_v2(bitmapImage, item, true);
                            rBook.Reports.Add(rpr);
                        }
                        else
                        {
                            var rpr = new ClippingReport_v2(bitmapImage, item, false);
                            rBook.Reports.Add(rpr);
                        }
                        index++;
                    }
                }

                //Тональность //1
                rBook.Reports.Add(
                    new ExclusivityReport(
                        model.EF_Publications.Where(
                            item =>
                            item.Project_name.Equals(projName) && item.Publication_date >= beginPeriod &&
                            item.Publication_date <= endPeriod)));;
                //Эксклюзивность
                rBook.Reports.Add(
                    new HasPhotoReport(
                        model.EF_Publications.Where(
                            item =>
                            item.Project_name.Equals(projName) && item.Publication_date >= beginPeriod &&
                            item.Publication_date <= endPeriod)));;
                //Фотография
                rBook.Reports.Add(
                    new DiagramReport(
                        model.EF_Publications.Where(
                            item =>
                            item.Project_name.Equals(projName) && item.Publication_date >= beginPeriod &&
                            item.Publication_date <= endPeriod), "", "", ""));
                //Инициированные
                rBook.Reports.Add(
                    new InitiatedReport(
                        model.EF_Publications.Where(
                            item =>
                            item.Project_name.Equals(projName) && item.Publication_date >= beginPeriod &&
                            item.Publication_date <= endPeriod)));;
                //Запланированные
                rBook.Reports.Add(
                    new PlannedReport(
                        model.EF_Publications.Where(
                            item =>
                            item.Project_name.Equals(projName) && item.Publication_date >= beginPeriod &&
                            item.Publication_date <= endPeriod)));
                //Приоритет
                if (projName.Contains("arteks"))
                {
                    rBook.Reports.Add(
                        new PriorityReport(
                            model.EF_Publications.Where(
                                item =>
                                item.Project_name.Equals(projName) && item.Publication_date >= beginPeriod &&
                                item.Publication_date <= endPeriod)));
                }
            }
            //rBook.Reports.Add(new DiagramReport());
            //rBook.Reports.Add(new DiagramReport());
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
