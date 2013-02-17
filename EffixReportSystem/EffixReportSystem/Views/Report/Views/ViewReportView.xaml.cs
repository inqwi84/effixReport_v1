using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
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
using EffixReportSystem.Views.Report.ViewModels;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using Telerik.Reporting;
using Encoder = System.Text.Encoder;
using Image = System.Drawing.Image;

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


        ReportBook headBook = new ReportBook();
        ReportBook initiatedBook = new ReportBook();
        ReportBook notInitiatedBook = new ReportBook();
        ReportBook diagramBook = new ReportBook();
       ObservableCollection<ReportBook> bookColl= new ObservableCollection<ReportBook>(); 
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
          Dispatcher.BeginInvoke(new Func<bool>(()=> indicator.IsBusy = true));
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
                    rootBook = MakeAvtoALEA_JLR_Report22("avtoalea-honda", bDate, eDate);
                    break;
                case 6:
                    rootBook = MakeAvtoALEA_JLR_Report22("avtoalea-jaguar", bDate, eDate);
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
            Dispatcher.BeginInvoke(new Func<bool>(() => indicator.IsBusy = false));
           // reportViewer.ReportSource = rootBook;
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
            //var ctx = DataContext as ViewReportViewModel;
            //ctx.SetProjectIndex(ProjectsComboBox.SelectedIndex);
            //ctx.IsBusy = true;
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
                GC.Collect();
                int gIndex = 1;
                foreach (var rst in resultList)
                {
                    var ind = resultList.IndexOf(rst);
                    var report1 = new AvtoAleaJLRHeadReport(rst, resultNameList[ind],gIndex);
                    rBook.Reports.Add(report1);
                }
                GC.Collect();
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
                                    GC.Collect();
                                }
                                else
                                {
                                    var rpr = new ClippingReport_v2(bitmapImage, efPublication, false);
                                    rBook.Reports.Add(rpr);
                                    GC.Collect();
                                }
                                index++;
                            }
                        }
                    }
                }
                GC.Collect();
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
        private ReportBook MakeAvtoALEA_JLR_Report22(string projName, DateTime beginPeriod, DateTime endPeriod)
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
                    var report1 = new AvtoAleaJLRHeadReport(rst, resultNameList[ind], gIndex);
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
                                    var rpr = new ClippingReport_v22(bitmapImage, efPublication, true);
                                    rBook.Reports.Add(rpr);
                                }
                                else
                                {
                                    var rpr = new ClippingReport_v22(bitmapImage, efPublication, false);
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
                            item.Publication_date <= endPeriod)));
                //Фотография
                rBook.Reports.Add(
                    new TonalityReport(
                        model.EF_Publications.Where(
                            item =>
                            item.Project_name.Equals(projName) && item.Publication_date >= beginPeriod &&
                            item.Publication_date <= endPeriod)));

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
              //1 инициированные
                var initiatedGroups = allList.Where(item => item.Is_initiated == 1).OrderBy(item => item.Publication_date)
                                     .ThenBy(item => item.EF_SMI.Smi_name)
                           .GroupBy(item => item.EF_SMI.EF_MassMedium.Parent_type_id).OrderByDescending(item => item.Key);
                //2 неинециированные
               var nonInitiatedGroups =allList.Where(item => item.Is_initiated == 2).OrderBy(item => item.Publication_date)
                                     .ThenBy(item => item.EF_SMI.Smi_name)
                            .GroupBy(item => item.EF_SMI.EF_MassMedium.Parent_type_id).OrderByDescending(item => item.Key);

                int idx = 1;
                try
                {
                    foreach (var nonInitiatedGroup in nonInitiatedGroups)
                    {
                        var smiType =
                            model.EF_MassMedias.FirstOrDefault(item => item.Mass_media_type_id == nonInitiatedGroup.Key);
                        var report1 = new LifanHeadReport(smiType.Mass_media_type_name, nonInitiatedGroup.ToList(), idx);
                        rBook.Reports.Add(report1);
                        headBook.Reports.Add(report1);
                        idx += nonInitiatedGroup.Count();
                    }
                    GC.Collect();
                    foreach (var initiatedGroup in initiatedGroups)
                    {
                        var smiType =
                            model.EF_MassMedias.FirstOrDefault(item => item.Mass_media_type_id == initiatedGroup.Key);
                        var report1 = new LifanHeadReport(smiType.Mass_media_type_name, initiatedGroup.ToList(), idx);
                        rBook.Reports.Add(report1);
                        headBook.Reports.Add(report1);
                        idx += initiatedGroup.Count();
                    }
                    GC.Collect();
                    var report12 = new LifanHeadReport("Самиздат", samIzdatList, idx);
                    rBook.Reports.Add(report12);
                    headBook.Reports.Add(report12);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


                try
                {
                    foreach (var nonInitiatedGroup in nonInitiatedGroups)
                    {
                        var smiType =
    model.EF_MassMedias.FirstOrDefault(item => item.Mass_media_type_id == nonInitiatedGroup.Key);
                        rBook.Reports.Add(new GroupPageReport(smiType.Mass_media_type_name));
                        notInitiatedBook.Reports.Add(new GroupPageReport(smiType.Mass_media_type_name));
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
                                        notInitiatedBook.Reports.Add(rpr);
                                        GC.Collect();
                                    }
                                    else
                                    {
                                        var rpr = new ClippingReport_v2(bitmapImage, efPublication, false);
                                        rBook.Reports.Add(rpr);
                                        notInitiatedBook.Reports.Add(rpr);
                                        GC.Collect();
                                    }
                                    index++;
                                }
                            }
                        }
                        GC.Collect();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                GC.Collect();
                try
                {
                    foreach (var initiatedGroup in initiatedGroups)
                    {
                        var smiType =
model.EF_MassMedias.FirstOrDefault(item => item.Mass_media_type_id == initiatedGroup.Key);
                        rBook.Reports.Add(new GroupPageReport(smiType.Mass_media_type_name));
                        initiatedBook.Reports.Add(new GroupPageReport(smiType.Mass_media_type_name));
                        //var name = String.Empty;
                        //rBook.Reports.Add(new GroupPageReport(name));
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
                                        initiatedBook.Reports.Add(rpr);
                                        GC.Collect();
                                    }
                                    else
                                    {
                                        var rpr = new ClippingReport_v2(bitmapImage, efPublication, false);
                                        rBook.Reports.Add(rpr);
                                        initiatedBook.Reports.Add(rpr);
                                        GC.Collect();
                                    }
                                    index++;
                                }
                            }
                        }
                        GC.Collect();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                GC.Collect();
                //Тональность //1
                rBook.Reports.Add(
                    new ExclusivityReport(
                        model.EF_Publications.Where(
                            item =>
                            item.Project_name.Equals(projName) && item.Publication_date >= beginPeriod &&
                            item.Publication_date <= endPeriod)));
                diagramBook.Reports.Add(
                    new ExclusivityReport(
                        model.EF_Publications.Where(
                            item =>
                            item.Project_name.Equals(projName) && item.Publication_date >= beginPeriod &&
                            item.Publication_date <= endPeriod)));
                //Эксклюзивность
                rBook.Reports.Add(
                    new HasPhotoReport(
                        model.EF_Publications.Where(
                            item =>
                            item.Project_name.Equals(projName) && item.Publication_date >= beginPeriod &&
                            item.Publication_date <= endPeriod)));
                diagramBook.Reports.Add(new HasPhotoReport(
                        model.EF_Publications.Where(
                            item =>
                            item.Project_name.Equals(projName) && item.Publication_date >= beginPeriod &&
                            item.Publication_date <= endPeriod))); 
                //Фотография
                rBook.Reports.Add(
                    new DiagramReport(
                        model.EF_Publications.Where(
                            item =>
                            item.Project_name.Equals(projName) && item.Publication_date >= beginPeriod &&
                            item.Publication_date <= endPeriod), "", "", ""));
                diagramBook.Reports.Add(
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
                            item.Publication_date <= endPeriod)));
                diagramBook.Reports.Add(new InitiatedReport(
                        model.EF_Publications.Where(
                            item =>
                            item.Project_name.Equals(projName) && item.Publication_date >= beginPeriod &&
                            item.Publication_date <= endPeriod)));
                //Запланированные
                rBook.Reports.Add(
                    new PlannedReport(
                        model.EF_Publications.Where(
                            item =>
                            item.Project_name.Equals(projName) && item.Publication_date >= beginPeriod &&
                            item.Publication_date <= endPeriod)));
                diagramBook.Reports.Add(new PlannedReport(
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

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
        private string SetQualityLevel(string path,string outPath)
        {
            var result = String.Empty;
            try
            {
                // Get a bitmap.
                Bitmap bmp1 = new Bitmap(path);
                ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);

                // Create an Encoder object based on the GUID 
                // for the Quality parameter category.
                System.Drawing.Imaging.Encoder myEncoder =
                    System.Drawing.Imaging.Encoder.Quality;

                // Create an EncoderParameters object. 
                // An EncoderParameters object has an array of EncoderParameter 

                EncoderParameters myEncoderParameters = new EncoderParameters(1);
                var file = new FileInfo(path);
                if(file.Length>5088566)
                {
                    EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
                    myEncoderParameters.Param[0] = myEncoderParameter;
                    bmp1.Save(outPath.Replace(".png", ".jpg"), jgpEncoder, myEncoderParameters);  
                }
                else
                {
                    EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 70L);
                    myEncoderParameters.Param[0] = myEncoderParameter;
                    bmp1.Save(outPath.Replace(".png", ".jpg"), jgpEncoder, myEncoderParameters);
                }
                return outPath;
            }
            catch (Exception)
            {

            }

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
                    var tmpPath = "c:\\storage\\report.png";
                    var dir = new DirectoryInfo(filePath.Remove(filePath.LastIndexOf("\\")));
                    if (!dir.Exists)
                    {
                        dir.Create();
                    }
                    blockBlob.DownloadToFile(filePath);
                    //using (var ms = new MemoryStream())
                    //{
                    //    FileStream file = new FileStream(tmpPath, FileMode.Open, FileAccess.Read);
                    //    byte[] bytes = new byte[file.Length];
                    //    file.Read(bytes, 0, (int) file.Length);
                    //    ms.Write(bytes, 0, (int) file.Length);
                    //    file.Close();

                    //}
                    var imgPath = SetQualityLevel(filePath, filePath);
                   var bmp= Image.FromFile(imgPath);
                   result.Add(bmp as Bitmap);
                 //   bmp.Dispose();

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


        private int tmpInt = 0;
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ctx = 1;
            try
            {
                tmpInt++;
                //if (bw.IsBusy != true)
                //{
                //    bw.RunWorkerAsync();
                //}
                //bookColl.Add(headBook);
                //bookColl.Add(initiatedBook);
                //bookColl.Add(notInitiatedBook);
                //bookColl.Add(diagramBook);
                if (tmpInt == 1)
                {
                    GetValue(headBook,1);
                }
                if (tmpInt == 2)
                {
                    GetValue(initiatedBook,2);
                }
                if (tmpInt == 3)
                {
                    GetValue(notInitiatedBook,3);
                } if (tmpInt == 4)
                {
                    GetValue(diagramBook,4);
                    tmpInt = 0;
                    ctx = 1;
                }
                //Task.Factory.StartNew(()=>GetValue(headBook));
                //Task.Factory.StartNew(() => GetValue(initiatedBook));
                //Task.Factory.StartNew(() => GetValue(notInitiatedBook));
                //Task.Factory.StartNew(() => GetValue(diagramBook));
                //  GetValue(book);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

       }

        private static int ctx = 0;
        private static void GetValue(ReportBook book,int index)
        {
            var count = book.Reports.Count;
            if (count > 0)
            {
                var temp = SplitBooks(book.Reports);
                foreach (var item in temp)
                {
                    var tmpBook = new ReportBook();
                    foreach (var report in item)
                    {
                        tmpBook.Reports.Add(report);
                    }
                    Make50ReportsToRTF(tmpBook, ctx);
                    ctx++;
                }
            }
        }

        public static List<List<Telerik.Reporting.Report>> SplitBooks(ReportCollection source)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / 20)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }
        private static void Make50ReportsToRTF(ReportBook book,int idx)
        {
            try
            {
                Telerik.Reporting.Processing.ReportProcessor reportProcessor =
new Telerik.Reporting.Processing.ReportProcessor();

                //set any deviceInfo settings if necessary
                System.Collections.Hashtable deviceInfo =
                    new System.Collections.Hashtable();

                Telerik.Reporting.InstanceReportSource instanceReportSource =
                    new Telerik.Reporting.InstanceReportSource();

                instanceReportSource.ReportDocument = book;

                Telerik.Reporting.Processing.RenderingResult result =
                    reportProcessor.RenderReport("RTF", instanceReportSource, deviceInfo);

                string fileName = result.DocumentName + idx + "." + result.Extension;
                string path = "c:\\storage\\";
                string filePath = System.IO.Path.Combine(path, fileName);

                using (System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
                {
                    fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
                }
                MessageBox.Show("Отчёт создан");
            }
            catch (Exception)
            {

            }

        }

        private int kk = 0;
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                kk++;
                if (kk == 1)
                {
                    reportViewer.ReportSource = headBook;
                }
                if (kk == 2)
                {
                    reportViewer.ReportSource = initiatedBook;
                }
                if (kk == 3)
                {
                    reportViewer.ReportSource = notInitiatedBook;
                } if (kk == 4)
                {
                    reportViewer.ReportSource = diagramBook;
                    kk = 0;
                }
            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
