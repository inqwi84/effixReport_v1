using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using EffixReportSystem.Helper.Classes;
using EffixReportSystem.Helper.Interfaces;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace EffixReportSystem.Views.Publication.ViewModels
{
    class EditPublicationViewModel:ObservableObject, IPageViewModel
    {
        private long _publicationId;
        private readonly EntitiesModel _model = new EntitiesModel();
        private readonly string _tempDirectory = String.Empty;
        private string _baseDirectory = String.Empty;
        public List<EF_SMI> Smi { set; get; }
        public List<EF_Tonality> Tonalities { get; set; }
        public List<EF_SMI_Type> SmiTypes { get; set; }
        public List<EF_Exclusivity> Exclusivities { get; set; }
        public List<EF_SMI_priority> Priorities { get; set; }
        public List<EF_Initiated> Initiated { get; set; }
        public List<EF_Planed> Planed { get; set; }
        public List<EF_Photo> Photo { get; set; }

        public void SetCurrentPublication(long publicationId)
        {
            _publicationId = publicationId;
            CurrentPublication = _model.EF_Publications.FirstOrDefault(item => item.Publication_id == publicationId);
            ImageTileList= GetBlobFromStorage(CurrentPublication.Blob_path,int.Parse(CurrentPublication.Image_count.Value.ToString().Trim()),CurrentPublication.Project_name);
            //загрузить рисунки
        }
        public void RestoreDefaultVlues()
        {
            try
            {
                CurrentPublication = _model.EF_Publications.FirstOrDefault(item => item.Publication_id == _publicationId);
                if (CurrentPublication != null)
                {
                    try
                    {
                        var destinationDirectory =
    new DirectoryInfo(_baseDirectory + "\\" +
                      CurrentPublication.Project_name + "\\" +
                      CurrentPublication.P_year + "\\" +
                      CurrentPublication.P_month + "\\" +
                      CurrentPublication.P_day);
                        if (!destinationDirectory.Exists)
                        {
                            destinationDirectory.Create();
                        }
                        foreach (var imageTile in ImageTileList)
                        {
                            var file = new FileInfo(imageTile.ImagePath);
                            if (file.Exists)
                            {
                                file.Delete();
                            }
                            using (var fs = file.Create())
                            {
                                var info = BufferFromImage(imageTile.Image);
                                fs.Write(info, 0, info.Length);
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }
            try
            {
                ImageTileList.Clear();
                ClearDirectory(_tempDirectory);
            }
            catch (Exception)
            {

            }


        }

        public Byte[] BufferFromImage(BitmapImage imageSource)
        {
            var stream = imageSource.StreamSource;
            Byte[] buffer = null;
            if (stream != null && stream.Length > 0)
            {
                using (var br = new BinaryReader(stream))
                {
                    buffer = br.ReadBytes((Int32)stream.Length);
                }
            }

            return buffer;
        }



        public void SaveCurrentPublication()
        {
            if (CurrentPublication != null)
            {
                try
                {
                    var date = CurrentPublication.Publication_date.Value;
                    var destinationDirectory =
                        new DirectoryInfo(_baseDirectory + "\\" +
                                          CurrentPublication.Project_name + "\\" +
                                          date.Year + "\\" +
                                          date.Month + "\\" +
                                          date.Day);


                    var sourceDirectory = new DirectoryInfo(_tempDirectory);

                    var files = sourceDirectory.GetFiles("*.*");

                    var index = 0;
                    foreach (var fileInfo in files)
                    {
                        if (!destinationDirectory.Exists)
                        {
                            destinationDirectory.Create();
                        }
                        var filePath = destinationDirectory + "\\" + CurrentPublication.EF_SMI.Smi_descr.Replace('.', '_') +
                                       "_" +
                                       fileInfo.Name;
                        var fInfo = new FileInfo(filePath);
                        if (fInfo.Exists)
                        {
                            fInfo.Delete();
                        }
                        fileInfo.MoveTo(filePath);
                        index++;
                        DataHelper.SaveFilesInAzureStorage(filePath);
                        if (index <= 1)
                        {
                            CurrentPublication.Blob_path = filePath;
                        }
                    }
                    CurrentPublication.Image_count = index;
                }
                catch (Exception)
                {
                }
           
               try
                {
                    _model.Add(CurrentPublication);
                    _model.SaveChanges();
                }
                catch (Exception)
                {

                }
                try
                {
                    ImageTileList.Clear();
                    ClearDirectory(_tempDirectory);
                }
                catch (Exception)
                {

                }
            }

        }
        public void ClearDirectory(string dirPath)
        {
            try
            {
                var directory = new DirectoryInfo(dirPath);
                var files = directory.GetFiles("*.*");
                foreach (var fileInfo in files)
                {
                    fileInfo.Delete();
                }
            }
            catch (Exception)
            {

            }

        }

        private ObservableCollection<DataHelper.ImageTile> GetBlobFromStorage(string blobPath, int imageCount,string projectName)
        {
            var result = new ObservableCollection<DataHelper.ImageTile>();
            var splitPath = blobPath.Split(Convert.ToChar("\\"));
            ClearDirectory(_tempDirectory);
            for(var i=0;i<imageCount;i++)
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
                    var tmpStr = filePath.Remove(0, filePath.IndexOf(projectName, StringComparison.Ordinal)+projectName.Length);
                   // var blockBlob = container.GetBlockBlobReference(filePath.Replace("c:\\storage\\lifan", ""));
                    var blockBlob = container.GetBlockBlobReference(tmpStr);
                    var path = _tempDirectory + "\\" + i + ".png";
                    // Save blob contents to a file.
                    var imageArray=blockBlob.DownloadByteArray();
                    blockBlob.DownloadToFile(path);
                    var directoryInfo = new DirectoryInfo(filePath.Remove(filePath.LastIndexOf("\\", StringComparison.Ordinal)));
                   if(!directoryInfo.Exists)
                   {
                       directoryInfo.Create();
                   }
                    blockBlob.DownloadToFile(filePath);
                    result.Add(new DataHelper.ImageTile
                                   {
                                       ImageName = i.ToString(CultureInfo.InvariantCulture),
                                       Image = ImageFromBuffer(imageArray),
                                       ImagePath = path
                                   });
                }
                catch (Exception)
                {

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
        public string Name { get; private set; }
        private ObservableCollection<DataHelper.ImageTile> _imageTileList;
        public ObservableCollection<DataHelper.ImageTile> ImageTileList
        {
            get { return _imageTileList; }
            set
            {
                if (ImageTileList == value)
                    return;

                _imageTileList = value;
                this.OnPropertyChanged("ImageTileList");
            }
        }
        private EF_Publication _currentPublication;
        public EF_Publication CurrentPublication
        {
            get { return this._currentPublication; }
            set
            {
                    if (CurrentPublication == value)
                        return;

                    _currentPublication = value;
                    this.OnPropertyChanged("CurrentPublication");
            }
        }

        private EF_Project _currentProject;
        public EF_Project CurrentProject
        {
            get { return this._currentProject; }
            set
            {
                if (CurrentProject == value)
                    return;

                _currentProject = value;
                this.OnPropertyChanged("CurrentProject");
            }
        }
        private DataHelper.ImageTile _currentImageTile;
        public DataHelper.ImageTile CurrentImageTile
        {
            get { return _currentImageTile; }
            set
            {
                if (CurrentImageTile == value)
                    return;

                _currentImageTile = value;
                this.OnPropertyChanged("CurrentImageTile");
            }
        }

        public IPageViewModel ParentViewModel;
        public EditPublicationViewModel(IPageViewModel parentViewModel)
        {
            //_tempDirectory = "c:\\storage\\temp";
            //_baseDirectory = "c:\\storage";
            _tempDirectory = Properties.Settings.Default.TempDirectory;
            _baseDirectory = Properties.Settings.Default.BaseDirectory;
            ParentViewModel = parentViewModel;
            //Smi = new List<EF_SMI>(_model.EF_SMIs);
            Tonalities = new List<EF_Tonality>(_model.EF_Tonalities);
            SmiTypes = new List<EF_SMI_Type>(_model.EF_SMI_Types);
            Exclusivities = new List<EF_Exclusivity>(_model.EF_Exclusivities);
            Priorities = new List<EF_SMI_priority>(_model.EF_SMI_priorities);
            Initiated =
                new List<EF_Initiated>(_model.EF_Initiateds);
            Planed =
                new List<EF_Planed>(
                    _model.EF_Planeds);
            Photo =
                new List<EF_Photo>(
                    _model.EF_Photos);
        }
        public void OpenInPaint(DataHelper.ImageTile tile)
        {
            CurrentImageTile = tile;
            //Process.Start("C:\\Program Files (x86)\\PhotoshopPortable\\PhotoshopCS4Portable.exe", CurrentImageTile.ImagePath);
            Process.Start(Properties.Settings.Default.PhotoshopExecutable, CurrentImageTile.ImagePath);
        }
        public void GetData()
        {
            try
            {
                Smi = new List<EF_SMI>(_model.EF_SMIs.OrderBy(item => item.Smi_name));
                Tonalities = new List<EF_Tonality>(_model.EF_Tonalities);
                SmiTypes = new List<EF_SMI_Type>(_model.EF_SMI_Types);
                Exclusivities = new List<EF_Exclusivity>(_model.EF_Exclusivities);
                Priorities = new List<EF_SMI_priority>(_model.EF_SMI_priorities);
                Initiated = new List<EF_Initiated>(_model.EF_Initiateds);
                Planed = new List<EF_Planed>(_model.EF_Planeds);
                Photo = new List<EF_Photo>(_model.EF_Photos);
            }
            catch (Exception)
            {

            }

        }
    }
}
