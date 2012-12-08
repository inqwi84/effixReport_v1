using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EffixReportSystem.Helper.Classes;
using EffixReportSystem.Views.Publication.ViewModels;

namespace EffixReportSystem.Views.Publication.Views
{
    /// <summary>
    /// Interaction logic for NewPublicationView.xaml
    /// </summary>
    public partial class NewPublicationView : UserControl
    {
        public NewPublicationView()
        {
            InitializeComponent();
        }

        private void ClearSearchTextBox(object sender, RoutedEventArgs e)
        {

        }

        private void MakeSnaphotsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var ctx = DataContext as NewPublicationViewModel;
            (ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel = (ctx.ParentViewModel as PublicationViewModel).PageViewModels[0];
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UrlTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void UrlTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            var ctx = DataContext as NewPublicationViewModel;
            if (ctx != null)
            {
                ctx.CurrentUrl = (sender as TextBox).Text;
                GetSnapshot();
            }
        }
        private void GetSnapshot()
        {
            var ctx = DataContext as NewPublicationViewModel;
            var hc = new HtmlCapture();
            hc.HtmlImageCapture +=
                hc_HtmlImageCapture;
            if (ctx != null)
            {
                hc.Create(ctx.CurrentUrl);
            }

        }

        private void hc_HtmlImageCapture(object sender, Uri url, Bitmap image)
        {
            var memoStream = new MemoryStream();
            image.Save(memoStream, ImageFormat.Png);
            var t = RHelper.MemoryStreamToBitmapImage(memoStream);
            var image2 = t;

            //(DataContext as ImageEditControlViewModel).UrlImageSource = image2;
            //ImageEditor.img.Source = image2;
            img.Source = image2;
        }

        private string GetUrl(string url)
        {
            var tmp = url.Split('/');
            var rst = ExtractDomainFromURL(url);
            return rst.Replace("www.", string.Empty);
        }
        private string ExtractDomainFromURL(string sURL)
        {

            Regex rg = new Regex("://(?<host>([a-z\\d][-a-z\\d]*[a-z\\d]\\.)*[a-z][-a-z\\d]+[a-z])");

            if (rg.IsMatch(sURL))
            {
                return rg.Match(sURL).Result("${host}");
            }
            else
            {
                return string.Empty;
            }
        }
    }
    }

