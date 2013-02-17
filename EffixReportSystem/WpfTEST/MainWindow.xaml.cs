using System;
using System.Collections.Generic;
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
    }

}
