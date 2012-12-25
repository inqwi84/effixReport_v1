using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Windows.Forms;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using Application = System.Windows.Application;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace EffixReportSystem
{
    public enum ParentType
    {
        Project,
        Year,
        Month
    }

    public partial class EF_Department
    {
        private IList<EF_Department> _children;

        public virtual IList<EF_Department> Children
        {
            get { return this._children; }
            set
            {
                if (Equals(Children, value))
                    return;

                _children = value;
                this.OnPropertyChanged("Children");
            }
        }
    }

    public partial class EF_MassMedia 
    {
        private List<EF_MassMedia> _children;

        public virtual List<EF_MassMedia> Children
        {
            get { return this._children; }
            set
            {
                if (Equals(Children, value))
                    return;

                _children = value;
                this.OnPropertyChanged("Children");
            }
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}

namespace EffixReportSystem.Helper.Classes
{
    public static class DataHelper
    {
        public static List<EF_MassMedia> Traverse(this List<EF_MassMedia> c)
        {
            var result = new List<EF_MassMedia>();
            foreach (var rootItem in c)
            {
                result.Add(rootItem);
                result.AddRange(rootItem.Children.Traverse());
            }
            return result;
        }

        public static int Index = -1;
        public static List<EF_MassMedia> TreeToFlat(EF_MassMedia rootNode)
        {

            var organizations = new List<EF_MassMedia>();

            //объявляем коллекции родительских и дочерних узлов
            var parentNodes = new List<EF_MassMedia>();
            var childNodes = new List<EF_MassMedia>();

            rootNode.Mass_media_type_id = Index;
            Index--;
            foreach (var node in rootNode.Children)
            {
                node.Mass_media_type_id = Index;
                Index--;
                node.Parent_type_id = null;
                parentNodes.Add(node);
            }

            //цикл будет выполняться, пока не пуста коллекция родительских элементов
            while (parentNodes.Count > 0)
            {
                //проходим по родительским элементам
                foreach (EF_MassMedia parentNode in parentNodes)
                {
                    //у каждого родителя проходим по дочерним элементам
                    foreach (EF_MassMedia child in parentNode.Children)
                    {
                        child.Parent_type_id = parentNode.Mass_media_type_id;
                        childNodes.Add(child);
                    }
                }

                //добавляем в итоговую коллекцию
                organizations.AddRange(childNodes);

                //в этом месте делаем хитрый обмен, теперь дочерние элементы становятся родительскими
                List<EF_MassMedia> tempNodes = parentNodes;
                parentNodes = childNodes;
                childNodes = tempNodes;

                childNodes.Clear();
            }

            var result = new List<EF_MassMedia> { rootNode };
            return result.Traverse();
        }

        public static bool CheckIfPublicationExists(long projectId, long smiID, int pYear, int pMonth, int pDay)
        {
            try
            {
                using (var model = new EntitiesModel())
                {
                    var publication = model.EF_Publications.FirstOrDefault(pub => pub.Project_id == projectId &&
                                                                   pub.Smi_id == smiID &&
                                                                   pub.Publication_date.Value.Year == pYear &&
                                                                   pub.Publication_date.Value.Month == pMonth &&
                                                                   pub.Publication_date.Value.Day == pDay);
                    return publication!=null;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static EF_Department GetParentProject(long childId)
        {
            var result = new EF_Department();
            using (var model = new EntitiesModel())
            {
                var child = model.EF_Departments.FirstOrDefault(item => item.Department_id == childId);
                if (child == null) return null;
                switch (child.Department_type)
                {
                    case "project":
                        result = child;
                        break;

                            case "year":
                        result =
                            model.EF_Departments.FirstOrDefault(item => item.Department_id == child.Department_parent_id);
                                break;
                            case "month":
                        var year =model.EF_Departments.FirstOrDefault(item => item.Department_id == child.Department_parent_id);
                        result =
                            model.EF_Departments.FirstOrDefault(item => item.Department_id == year.Department_parent_id);
                        break;
                            case "day":
                        var monthDay = model.EF_Departments.FirstOrDefault(item => item.Department_id == child.Department_parent_id);
                        var yearDay = model.EF_Departments.FirstOrDefault(item => item.Department_id == monthDay.Department_parent_id);
                       result = model.EF_Departments.FirstOrDefault(item => item.Department_id == yearDay.Department_parent_id);
                                break;

                }
            }
            return result;
        }

        public static ObservableCollection<EF_Publication> GetPublicationByDepartmentId(long departmentId)
        {
            var result = new ObservableCollection<EF_Publication>();
            using (var model= new EntitiesModel())
            {
                var department = model.EF_Departments.FirstOrDefault(dept => dept.Department_id == departmentId);
                if (department != null)
                    switch (department.Department_type)
                    {
                        case "project":
                            result= new ObservableCollection<EF_Publication>(model.EF_Publications.Where(item => item.Project_id == department.Department_project_id));
                            break;
                        case "year":
                           // var project = GetParentDepartment((long) department.Department_parent_id);
                            result=new ObservableCollection<EF_Publication>(model.EF_Publications.Where(item=>item.Project_id==department.Department_parent_id&&item.P_year==department.Department_name));
                            break;
                        case "month":
                            var year = model.EF_Departments.FirstOrDefault(item => item.Department_id == department.Department_parent_id);
                            var project = model.EF_Departments.FirstOrDefault(item => item.Department_id == year.Department_parent_id);
                            result=new ObservableCollection<EF_Publication>(model.EF_Publications.Where(item=>item.Project_id==project.Department_project_id&&item.P_year==year.Department_name&&item.P_month==department.Department_name));
                            break;
                        case "day":
                            result = new ObservableCollection<EF_Publication>(model.EF_Publications.Where(item => item.Department_id==department.Department_id));
                            break;
                    }
            }
            return result;
        }

     

        public static Visual FindAncestor(Visual child, Type typeAncestor)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);
            while (parent != null && !typeAncestor.IsInstanceOfType(parent))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return (parent as Visual);
        }


        public class ImageTile : ObservableObject
        {
            private string _imageName;
            private string _imagePath;
            private BitmapImage _image;

            public string ImageName
            {
                get { return this._imageName; }
                set
                {
                    if (ImageName == value)
                        return;

                    _imageName = value;
                    this.OnPropertyChanged("ImageName");
                }
            }

            public string ImagePath
            {
                get { return this._imagePath; }
                set
                {
                    if (ImagePath == value)
                        return;

                    _imagePath = value;
                    this.OnPropertyChanged("ImagePath");
                }
            }

            public BitmapImage Image
            {
                get { return this._image; }
                set
                {
                    if (Image == value)
                        return;

                    _image = value;
                    this.OnPropertyChanged("Image");
                }
            }

        }

        private static List<UnmanagedMemoryStream> GetResourceNames(CultureInfo culture)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Stream s = asm.GetManifestResourceStream("StringDictionary");
            string resourceName = asm.GetName().Name + ".g";
            ResourceManager rm = new ResourceManager(resourceName, asm);
            ResourceSet resourceSet = rm.GetResourceSet(culture, true, true);
            List<string> resources = new List<string>();
            List<UnmanagedMemoryStream> resources2 = new List<UnmanagedMemoryStream>();
            foreach (DictionaryEntry resource in resourceSet)
            {
                if (((string) resource.Key).Contains("stringdictionary"))
                {
                    resources2.Add((UnmanagedMemoryStream) resource.Value);
                }
                resources.Add((string) resource.Key);
            }
            rm.ReleaseAllResources();
            return resources2;
        }

        public static string GetStringFromDictionary(string dictionaryName, string key)
        {
            try
            {
                var resource =
                    Application.Current.Resources.MergedDictionaries.First(
                        item => item.Source.ToString().ToLower().Contains(dictionaryName.ToLower()));
                return resource[key] as string;
            }
            catch (Exception)
            {
                return "";
            }

        }

        public static void SaveFilesInAzureStorage(string filePath)
        {
            var nameArray = filePath.Split(Convert.ToChar("\\"));
            var projectNameContainer = nameArray[2];
            var fileName = filePath.Replace("c:\\storage\\" + projectNameContainer, String.Empty);
            try
            {
                var cloudStorageAccount = CloudStorageAccount.Parse(
                    "DefaultEndpointsProtocol=http;AccountName=ctx;AccountKey=rCaek5ugmLbIaL2mXk3gaqMF4mzqPrUu6CXBUsXn1yrTdWBBTBsQpA2bDuyDC6BQx1NCeUhEl6p0vWT69ZNF+Q==");
                var blobClient = cloudStorageAccount.CreateCloudBlobClient();
                var blobContainer = blobClient.GetContainerReference(projectNameContainer);
                var blob = blobContainer.GetBlobReference(fileName);
                var fileStream = File.OpenRead(filePath);

                var byteArray = new byte[fileStream.Length];
                fileStream.Read(byteArray, 0, byteArray.Length);
                blob.UploadByteArray(byteArray);
                fileStream.Close();
            }
            catch (StorageClientException e)
            {
            }
            catch (Exception e)
            {
            }
        }
    }

    public class PublicationHelper
    {
        private static PublicationHelper _instance;
        public ObservableCollection<EF_SMI> Smi;
        public ObservableCollection<EF_Tonality> Tonalities;
        public ObservableCollection<EF_SMI_Type> SmiTypes;
        public ObservableCollection<EF_Exclusivity> Exclusivities;
        public ObservableCollection<EF_SMI_priority> Priorities;
        public ObservableCollection<EF_Initiated> Initiated;
        public ObservableCollection<EF_Planed> Planed;
        public ObservableCollection<EF_Photo> Photo;

        private PublicationHelper()
        {
            using (var model = new EntitiesModel())
            {
                Smi = new ObservableCollection<EF_SMI>(model.EF_SMIs);
                Tonalities = new ObservableCollection<EF_Tonality>(model.EF_Tonalities);
                SmiTypes = new ObservableCollection<EF_SMI_Type>(model.EF_SMI_Types);
                Exclusivities = new ObservableCollection<EF_Exclusivity>(model.EF_Exclusivities);
                Priorities = new ObservableCollection<EF_SMI_priority>(model.EF_SMI_priorities);
                Initiated =
                    new ObservableCollection<EF_Initiated>(model.EF_Initiateds);
                Planed =
                    new ObservableCollection<EF_Planed>(
                        model.EF_Planeds);
                Photo =
                    new ObservableCollection<EF_Photo>(
                        model.EF_Photos);
            }

        }

        public static PublicationHelper Instance
        {
            get { return _instance ?? (_instance = new PublicationHelper()); }
        }
    }

    public class HtmlCapture
    {
        private WebBrowser web;
        private Timer tready;
        private Rectangle screen;
        private Size? imgsize = null;

        //an event that triggers when the html document is captured
        public delegate void HtmlCaptureEvent(object sender,
                                              Uri url, Bitmap image);

        public event HtmlCaptureEvent HtmlImageCapture;

        //class constructor
        public HtmlCapture()
        {
            //initialise the webbrowser and the timer
            web = new WebBrowser();
            tready = new Timer();
            tready.Interval = 2000;
            screen = Screen.PrimaryScreen.Bounds;
            //set the webbrowser width and hight
            web.Width = screen.Width;
            web.Height = screen.Height;
            //suppress script errors and hide scroll bars
            web.ScriptErrorsSuppressed = true;
            web.ScrollBarsEnabled = false;
            //attached events
            web.Navigating +=
                new WebBrowserNavigatingEventHandler(web_Navigating);
            web.DocumentCompleted += new
                WebBrowserDocumentCompletedEventHandler(tready_Tick);
            tready.Tick += new EventHandler(tready_Tick);
        }
        public HtmlCapture(WebBrowser web)
        {
            //initialise the webbrowser and the timer
            tready = new Timer();
            tready.Interval = 2000;
            screen = Screen.PrimaryScreen.Bounds;
            //set the webbrowser width and hight
            web.Width = screen.Width;
            web.Height = screen.Height;
            //suppress script errors and hide scroll bars
            web.ScriptErrorsSuppressed = true;
            web.ScrollBarsEnabled = false;
            //attached events
            web.Navigating +=
                new WebBrowserNavigatingEventHandler(web_Navigating);
            web.DocumentCompleted += new
                WebBrowserDocumentCompletedEventHandler(tready_Tick);
            tready.Tick += new EventHandler(tready_Tick);
        }
        #region Public methods

        public void Create(string url)
        {
            imgsize = null;
            web.Navigate(url);
        }

        public void Create(string url, Size imgsz)
        {
            this.imgsize = imgsz;
            web.Navigate(url);
        }

        #endregion

        #region Events

        private void web_DocumentCompleted(object sender,
                                           WebBrowserDocumentCompletedEventArgs e)
        {
            //start the timer
            tready.Start();
        }

        private void web_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            //stop the timer   
            tready.Stop();
        }

        private void tready_Tick(object sender, EventArgs e)
        {
            //stop the timer
            tready.Stop();
            //get the size of the document's body
            Rectangle body = web.Document.Body.ScrollRectangle;

            //check if the document width/height is greater than screen width/height
            Rectangle docRectangle = new Rectangle()
                                         {
                                             Location = new Point(0, 0),
                                             Size = new Size(body.Width > screen.Width ? body.Width : screen.Width,
                                                             body.Height > screen.Height ? body.Height : screen.Height)
                                         };
            //set the width and height of the WebBrowser object
            web.Width = docRectangle.Width;
            web.Height = docRectangle.Height;

            //if the imgsize is null, the size of the image will 
            //be the same as the size of webbrowser object
            //otherwise  set the image size to imgsize
            Rectangle imgRectangle;
            if (imgsize == null)
                imgRectangle = docRectangle;
            else
                imgRectangle = new Rectangle()
                                   {
                                       Location = new Point(0, 0),
                                       Size = imgsize.Value
                                   };
            //create a bitmap object 
            Bitmap bitmap = new Bitmap(imgRectangle.Width, imgRectangle.Height);
            //get the viewobject of the WebBrowser
            IViewObject ivo = web.Document.DomDocument as IViewObject;

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                //get the handle to the device context and draw
                IntPtr hdc = g.GetHdc();
                ivo.Draw(1, -1, IntPtr.Zero, IntPtr.Zero,
                         IntPtr.Zero, hdc, ref imgRectangle,
                         ref docRectangle, IntPtr.Zero, 0);
                g.ReleaseHdc(hdc);
            }
            //invoke the HtmlImageCapture event
            HtmlImageCapture(this, web.Url, bitmap);
        }

        #endregion
    }

    [ComVisible(true), ComImport()]
    [Guid("0000010d-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IViewObject
    {
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int Draw([MarshalAs(UnmanagedType.U4)] uint dwDrawAspect, int lindex, IntPtr pvAspect, [In] IntPtr ptd,
                 IntPtr hdcTargetDev, IntPtr hdcDraw, [MarshalAs(UnmanagedType.Struct)] ref Rectangle lprcBounds,
                 [MarshalAs(UnmanagedType.Struct)] ref Rectangle lprcWBounds, IntPtr pfnContinue,
                 [MarshalAs(UnmanagedType.U4)] uint dwContinue);

        [PreserveSig]
        int GetColorSet([In, MarshalAs(UnmanagedType.U4)] int dwDrawAspect,
                        int lindex, IntPtr pvAspect, [In] IntPtr ptd,
                        IntPtr hicTargetDev, [Out] IntPtr ppColorSet);

        [PreserveSig]
        int Freeze([In, MarshalAs(UnmanagedType.U4)] int dwDrawAspect,
                   int lindex, IntPtr pvAspect, [Out] IntPtr pdwFreeze);

        [PreserveSig]
        int Unfreeze([In, MarshalAs(UnmanagedType.U4)] int dwFreeze);

        void SetAdvise([In, MarshalAs(UnmanagedType.U4)] int aspects,
                       [In, MarshalAs(UnmanagedType.U4)] int advf,
                       [In, MarshalAs(UnmanagedType.Interface)] IAdviseSink pAdvSink);

        void GetAdvise([In, Out, MarshalAs(UnmanagedType.LPArray)] int[] paspects,
                       [In, Out, MarshalAs(UnmanagedType.LPArray)] int[] advf,
                       [In, Out, MarshalAs(UnmanagedType.LPArray)] IAdviseSink[] pAdvSink);
    }

    public static class RHelper
    {

        public static BitmapImage Bitmap2BitmapImage(Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                ms.Position = 0;
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = ms;
                bi.EndInit();
                return bi;
            }
        }

        public static BitmapImage MemoryStreamToBitmapImage(MemoryStream memoStream)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = memoStream;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            bitmap.Freeze();
            return bitmap;
        }
    }

    public static class Imaging
    {
        public static BitmapSource CreateBitmapSourceFromBitmap(Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");

            if (Application.Current.Dispatcher == null)
                return null; // Is it possible?

            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // You need to specify the image format to fill the stream. 
                    // I'm assuming it is PNG
                    bitmap.Save(memoryStream, ImageFormat.Png);
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    // Make sure to create the bitmap in the UI thread
                    if (InvokeRequired)
                        return (BitmapSource) Application.Current.Dispatcher.Invoke(
                            new Func<Stream, BitmapSource>(CreateBitmapSourceFromBitmap),
                            DispatcherPriority.Normal,
                            memoryStream);

                    return CreateBitmapSourceFromBitmap(memoryStream);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static bool InvokeRequired
        {
            get { return Dispatcher.CurrentDispatcher != Application.Current.Dispatcher; }
        }

        private static BitmapSource CreateBitmapSourceFromBitmap(Stream stream)
        {
            BitmapDecoder bitmapDecoder = BitmapDecoder.Create(
                stream,
                BitmapCreateOptions.PreservePixelFormat,
                BitmapCacheOption.OnLoad);

            // This will disconnect the stream from the image completely...
            WriteableBitmap writable = new WriteableBitmap(bitmapDecoder.Frames.Single());
            writable.Freeze();

            return writable;
        }

        public static byte[] FileToByteArray(string fileName)
        {
            byte[] buffer = null;
            try
            {
                // Open file for reading
                var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                // attach filestream to binary reader
                var binaryReader = new BinaryReader(fileStream);
                // get total byte length of the file
                long totalBytes = new FileInfo(fileName).Length;
                // read entire file into buffer
                buffer = binaryReader.ReadBytes((Int32) totalBytes);
                // close file reader
                fileStream.Close();
                fileStream.Dispose();
                binaryReader.Close();
            }
            catch (Exception exception)
            {
            }
            return buffer;
        }
    }
}