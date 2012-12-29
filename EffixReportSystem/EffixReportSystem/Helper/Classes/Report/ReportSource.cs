using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace EffixReportSystem.Helper.Classes.Report
{
    [System.ComponentModel.DataObject]
    public partial class EF_Report : List<EF_Publication>
    {
        public EF_Report(EF_Publication publication)
        {
            
        }

        public EF_Report()
        {

        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<EF_Publication> GetReportByPeriod(long projectId, DateTime startDate,DateTime endDate)
        {
            var result = new List<EF_Publication>();
            using (var model = new EntitiesModel())
            {
                result.AddRange(
                    model.EF_Publications.Where(
                        pub =>
                        pub.Project_id == projectId && pub.Publication_date > startDate &&
                        pub.Publication_date < endDate));
            }
            return result;
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<EF_Publication> GetReportByEvent(long eventId)
        {
            // TODO: Implement your specific business logic here.
            return new System.Collections.Generic.List<EF_Publication>();
        }

        //[System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        //public List<EF_Publication> GetAllReports()
        //{
        //    var result = new List<EF_Publication>();
        //    using (var model = new EntitiesModel())
        //    {
        //        result.AddRange(
        //            model.EF_Publications);
        //    }
        //    return result;
        //}
        //[System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        //public List<TT> GetAllReports()
        //{
        //    var result = new List<TT>();

        //    result.Add(new TT(){AValue = "1"});
        //    result.Add(new TT() { AValue = "2" }); 
        //    result.Add(new TT() { AValue = "3" });
        //    return result;
        //}
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<ReportRS> GetAllReports()
        {
            var result = new List<ReportRS>();
            using (var model = new EntitiesModel())
            {
                int index = 1;
                foreach (var reportRs in model.EF_Publications)
                {
                    result.Add(new ReportRS(reportRs,index));
                    index++;
                }
            }
            return result;
        }
    }

    public class TT
    {
        public string AValue { get; set; }
    }

    public class ImageReport
    {
        public Bitmap Image { get; set; }
        public string Title { get; set; }
    }

    public class ImageReportsList :List<ImageReport>
    {
        public ImageReportsList ()
        {
           var report = new ImageReport() { Image = new Bitmap("c:\\ing.png"), Title = "ing.png" };
        this.Add(report);
           var  report2 = new ImageReport() { Image = new Bitmap("c:\\img.jpg"), Title = "img.jpg" };
            this.Add(report2);
        }
    }

    public class ReportRS
    {
        public int Index { get; set; }
        public string MassMediaName { get; set; }
        public DateTime PublicationDate { get; set; }
        public string PublicationUrl { get; set; }
        public string PublicationTitle { get; set; }
        public string MassMediaType { get; set; }
        public List<Bitmap> ImageList { get; set; }
        public List<string> ImagePathList { get; set; }
        public string TempUri { get; set; }

        private List<BitmapImage> GetImagesFromAzure(string imagePath,int? imageCount,string projectName)
        {
            return new List<BitmapImage>();
        }

        private ObservableCollection<Bitmap> GetBlobFromStorage(string blobPath, int? imageCount, string projectName)
        {
            var result = new ObservableCollection<Bitmap>();
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
                    // var blockBlob = container.GetBlockBlobReference(filePath.Replace("c:\\storage\\lifan", ""));
                    var blockBlob = container.GetBlockBlobReference(tmpStr);
                    // Save blob contents to a file.
                    var imageArray = blockBlob.DownloadByteArray();

                    result.Add(ImageFromBuffer(imageArray));
                }
                catch (Exception)
                {

                }
            }
            // Retrieve storage account from connection string.
            return result;
        }

        private ObservableCollection<string> GetBlobPathFromStorage(string blobPath, int? imageCount, string projectName)
        {
            var result = new ObservableCollection<string>();
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
                    // var blockBlob = container.GetBlockBlobReference(filePath.Replace("c:\\storage\\lifan", ""));
                    var blockBlob = container.GetBlockBlobReference(tmpStr);
                    // Save blob contents to a file.

                    result.Add(blockBlob.Uri.AbsoluteUri);
                }
                catch (Exception)
                {

                }
            }
            // Retrieve storage account from connection string.
            return result;
        }

        public Bitmap ImageFromBuffer(Byte[] bytes)
        {
            var stream = new MemoryStream(bytes);
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();
            BitmapImage bi = image; // Get bitmapimage from somewhere
            MemoryStream ms = new MemoryStream();
            BitmapEncoder encoder=new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bi));
            encoder.Save(ms);
            var bmp = new Bitmap(ms);
            return bmp;
        }


        public ReportRS(EF_Publication publication,int index)
        {
            Index = index;
            MassMediaName = publication.EF_SMI.Smi_name;
            PublicationDate = (DateTime) publication.Publication_date;
            PublicationUrl = publication.Url_path;
            PublicationTitle = publication.Publication_name;
            MassMediaType = publication.EF_SMI.EF_MassMedium.Mass_media_type_name;
            ImageList = GetBlobFromStorage(publication.Blob_path, publication.Image_count, publication.Project_name).ToList();
            ImagePathList = GetBlobPathFromStorage(publication.Blob_path, publication.Image_count, publication.Project_name).ToList();
            try
            {
                TempUri = ImagePathList[0];
            }
            catch (Exception)
            {

            }
        }
    }
    public class TileImages
    {
        
    }
}
