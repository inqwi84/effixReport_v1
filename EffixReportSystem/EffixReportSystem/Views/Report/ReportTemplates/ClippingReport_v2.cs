using System.Windows.Media.Imaging;

namespace EffixReportSystem.Views.Report.ReportTemplates
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for ClippingReport_v2.
    /// </summary>
    public partial class ClippingReport_v2 : Telerik.Reporting.Report
    {
        public ClippingReport_v2()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
        public ClippingReport_v2(Bitmap image,EF_Publication publication)
        {
            InitializeComponent();

            var t = Graphics.FromImage(image);
            var t1= image.Size.Height/t.DpiX;
            var t2 = image.Size.Width/t.DpiY;
            pictureBox1.Sizing=ImageSizeMode.ScaleProportional;
            var maxX = 6.5;
            var maxY = 8;
            var imageX = image.Size.Width/t.DpiX;
            var imageY = image.Size.Height/t.DpiY;


            if(imageY>maxY/2)
            {
                pictureBox1.Width= new Unit(maxX,UnitType.Inch);
                //pictureBox1.Height = new Unit(imageY,UnitType.Inch);
                pictureBox1.Height = new Unit(imageY, UnitType.Inch);
                pictureBox1.Sizing=ImageSizeMode.Stretch;
                pictureBox1.Value = image; 
            }
            else
            {
                pictureBox1.Width = new Unit(maxX, UnitType.Inch);
               // pictureBox1.Height = new Unit(imageY/2,UnitType.Inch);
                pictureBox1.Height = new Unit(maxY, UnitType.Inch);
                pictureBox1.Sizing = ImageSizeMode.Stretch;
                pictureBox1.Value = image; 
            }
            MassMediaNameTextBox.Value = publication.EF_SMI.Smi_name;

            try
            {
                PublicationDateTextBox.Value = string.Format("{0}: {1}", "Дата", publication.Publication_date.Value.ToShortDateString());
            }
            catch (Exception)
            {

            }
            try
            {
                UrlTextBox.Value = publication.Url_path.Trim();
            }
            catch (Exception)
            {
                
                throw;
            }
        
            //  detail.Items.Add(new Telerik.Reporting.PictureBox { Value = image });
        }
    }
}