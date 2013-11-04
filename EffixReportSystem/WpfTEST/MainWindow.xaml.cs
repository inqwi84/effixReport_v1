using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Telerik.Reporting;
using Image = System.Drawing.Image;

namespace WpfTEST
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var book = new ReportBook();
            for (int i = 0; i < 16; i++)
            {
                book.Reports.Add(new Report());
            }
            var rst = SplitBooks(book.Reports.ToList());
            var tt= (int)0.85;
           // var file =File.ReadAllBytes("c:\\storage\\test.png");
           // var tmp = ConvertPngToJpg(file);
            VaryQualityLevel();
        }
        public static List<List<Report>> SplitBooks(List<Report> source)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / 5)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }
        private void VaryQualityLevel()
        {
            // Get a bitmap.
            Bitmap bmp1 = new Bitmap("c:\\storage\\test.png");
            ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);

            // Create an Encoder object based on the GUID 
            // for the Quality parameter category.
            System.Drawing.Imaging.Encoder myEncoder =
                System.Drawing.Imaging.Encoder.Quality;

            // Create an EncoderParameters object. 
            // An EncoderParameters object has an array of EncoderParameter 
            // objects. In this case, there is only one 
            // EncoderParameter object in the array.
            EncoderParameters myEncoderParameters = new EncoderParameters(1);

            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bmp1.Save("c:\\storage\\test1.jpg", jgpEncoder, myEncoderParameters);

            myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bmp1.Save("c:\\storage\\test1.jpg", jgpEncoder, myEncoderParameters);

            // Save the bitmap as a JPG file with zero quality level compression.
            myEncoderParameter = new EncoderParameter(myEncoder, 80L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bmp1.Save("c:\\storage\\test1.jpg", jgpEncoder, myEncoderParameters);

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
        public static byte[] ConvertPngToJpg(byte[] pngByteArray)
        {
            Image img;
            using (MemoryStream msPng = new MemoryStream(pngByteArray))
            {
                img = Image.FromStream(msPng);
            }

            byte[] jpgArray;
            using (MemoryStream msJpeg = new MemoryStream())
            {
                img.Save(msJpeg, ImageFormat.Jpeg);
                jpgArray = msJpeg.ToArray();
            }

            return jpgArray;
        }

        private void SMIBase_OnClick(object sender, RoutedEventArgs e)
        {
            var smiColl = GetSmiColl("G:\\LoadSMI.csv");
           using (var conn = new SqlConnection("server=tcp:m6ufktgcjh.database.windows.net,1433;database=Effix;user id=EF_Admin@m6ufktgcjh;password=Effix1984"))
            {
                conn.Open();
                foreach (var smi in smiColl)
                {
                    var command = conn.CreateCommand();
                    command.CommandText = "Update EF_Smi set mass_media_id="+smi.SmiType+" where smi_id="+smi.Id;
                    command.ExecuteNonQuery();
                }
               conn.Close();
            }

        }

        private ObservableCollection<Smi> GetSmiColl(string path)
        {
            var result = new ObservableCollection<Smi>();
            var sr = new StreamReader(path, new UnicodeEncoding());
            while (!sr.EndOfStream)
            {
                result.Add(new Smi(sr.ReadLine()));
            }
            return result;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var result = new ObservableCollection<string>();
            var sr = new StreamReader("C:\\storage\\smi.csv");
            while (!sr.EndOfStream)
            {
                result.Add(sr.ReadLine());
            }
            using (var conn = new SqlConnection("server=tcp:m6ufktgcjh.database.windows.net,1433;database=Effix;user id=EF_Admin@m6ufktgcjh;password=Effix1984"))
            {
                conn.Open();
                foreach (var item in result)
                {
                    var selCommand = conn.CreateCommand();
                    selCommand.CommandText = "select  COUNT(*) from EF_Smi where smi_descr='" + item + "'";
                    var ct=  selCommand.ExecuteScalar();
                    var count = (int) ct;
                    if (count < 1)
                    {
                        var command = conn.CreateCommand();
                        command.CommandText = "insert into EF_Smi (smi_name,smi_descr,smi_url,mass_media_id) VALUES ('" + item + "','" + item + "','" + item + "',101)";
                        command.ExecuteNonQuery();
                    }

                }
                conn.Close();
            }
        }
        
    }
    public class Smi
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public  long SmiType { get; set; }

        public Smi(string str)
        {
            var tmp = str.Split(';');
                Id = long.Parse(tmp[0]);
                FirstName = tmp[1].Trim();
                SecondName=tmp[2].Trim();
            if (SecondName.Length == 0)
            {
                switch (FirstName)
                {
                    case "Автомобильные Интернет СМИ":
                        SmiType = 100;
                        break;
                    case "Информационные Интернет СМИ":
                        SmiType = 101;
                        break;
                    case "Самиздат":
                        SmiType = 102;
                        break;
                    case "Печатные СМИ":
                        SmiType = 103;
                        break;
                    case "Информационные порталы":
                        SmiType = 104;
                        break;
                    case "Печатные издания":
                        SmiType = 103;
                        break;
                }
            }
            else
            {
                switch (SecondName)
                {
                    case "Автомобильные Интернет СМИ":
                        SmiType = 100;
                        break;
                    case "Информационные Интернет СМИ":
                        SmiType = 101;
                        break;
                    case "Самиздат":
                        SmiType = 102;
                        break;
                    case "Печатные СМИ":
                        SmiType = 103;
                        break;
                    case "Информационные порталы":
                        SmiType = 104;
                        break;
                    case "Печатные издания":
                        SmiType = 103;
                        break;
                }
            }

        }
    }
}
