namespace EffixReportSystem.Views.Report.ReportTemplates
{
    partial class ClippingReport
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.TableGroup tableGroup1 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup2 = new Telerik.Reporting.TableGroup();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClippingReport));
            this.detail = new Telerik.Reporting.DetailSection();
            this.list1 = new Telerik.Reporting.List();
            this.panel1 = new Telerik.Reporting.Panel();
            this.pictureBox2 = new Telerik.Reporting.PictureBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.MassMediaNameTextBox = new Telerik.Reporting.HtmlTextBox();
            this.shape1 = new Telerik.Reporting.Shape();
            this.PublicationDateTextBox = new Telerik.Reporting.HtmlTextBox();
            this.InitiatedTextBox = new Telerik.Reporting.HtmlTextBox();
            this.SmiTypeTextBox = new Telerik.Reporting.HtmlTextBox();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.UrlTextBox = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(8.8207941055297852D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.list1});
            this.detail.Name = "detail";
            // 
            // list1
            // 
            this.list1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(7D)));
            this.list1.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Inch(8.82071590423584D)));
            this.list1.Body.SetCellContent(0, 0, this.panel1);
            tableGroup1.Name = "ColumnGroup";
            this.list1.ColumnGroups.Add(tableGroup1);
            this.list1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.panel1});
            this.list1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.18334579467773438D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.list1.Name = "list1";
            tableGroup2.Groupings.AddRange(new Telerik.Reporting.Grouping[] {
            new Telerik.Reporting.Grouping(null)});
            tableGroup2.Name = "DetailGroup";
            this.list1.RowGroups.Add(tableGroup2);
            this.list1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7D), Telerik.Reporting.Drawing.Unit.Inch(8.82071590423584D));
            // 
            // panel1
            // 
            this.panel1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pictureBox2});
            this.panel1.Name = "panel1";
            this.panel1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7D), Telerik.Reporting.Drawing.Unit.Inch(8.82071590423584D));
            // 
            // pictureBox2
            // 
            this.pictureBox2.Bindings.Add(new Telerik.Reporting.Binding("Value", "=Fields.Image"));
            this.pictureBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.9999213218688965D), Telerik.Reporting.Drawing.Unit.Inch(8.6999225616455078D));
            this.pictureBox2.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.Normal;
            // 
            // textBox1
            // 
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.9999213218688965D), Telerik.Reporting.Drawing.Unit.Inch(1D));
            this.textBox1.StyleName = "";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.383385181427002D), Telerik.Reporting.Drawing.Unit.Inch(7.8837074397597462E-05D));
            this.pictureBox1.MimeType = "image/jpeg";
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.9000003337860107D), Telerik.Reporting.Drawing.Unit.Inch(0.39162763953208923D));
            this.pictureBox1.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.ScaleProportional;
            this.pictureBox1.Value = ((object)(resources.GetObject("pictureBox1.Value")));
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.39170646667480469D);
            this.pageFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pictureBox1});
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(1.2000001668930054D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.MassMediaNameTextBox,
            this.shape1,
            this.PublicationDateTextBox,
            this.InitiatedTextBox,
            this.SmiTypeTextBox});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // MassMediaNameTextBox
            // 
            this.MassMediaNameTextBox.Anchoring = Telerik.Reporting.AnchoringStyles.Right;
            this.MassMediaNameTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.083543144166469574D), Telerik.Reporting.Drawing.Unit.Inch(0.70000004768371582D));
            this.MassMediaNameTextBox.Name = "MassMediaNameTextBox";
            this.MassMediaNameTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.0999603271484375D), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582D));
            this.MassMediaNameTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.MassMediaNameTextBox.Value = "";
            // 
            // shape1
            // 
            this.shape1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.60000008344650269D), Telerik.Reporting.Drawing.Unit.Inch(0.90000009536743164D));
            this.shape1.Name = "shape1";
            this.shape1.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.5999603271484375D), Telerik.Reporting.Drawing.Unit.Inch(0.10000014305114746D));
            this.shape1.Style.LineStyle = Telerik.Reporting.Drawing.LineStyle.Solid;
            this.shape1.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Point(2D);
            // 
            // PublicationDateTextBox
            // 
            this.PublicationDateTextBox.Anchoring = Telerik.Reporting.AnchoringStyles.Right;
            this.PublicationDateTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.083543144166469574D), Telerik.Reporting.Drawing.Unit.Inch(1D));
            this.PublicationDateTextBox.Name = "PublicationDateTextBox";
            this.PublicationDateTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.0999603271484375D), Telerik.Reporting.Drawing.Unit.Inch(0.1789696216583252D));
            this.PublicationDateTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.PublicationDateTextBox.Value = "";
            // 
            // InitiatedTextBox
            // 
            this.InitiatedTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.4999997615814209D), Telerik.Reporting.Drawing.Unit.Inch(0.099999986588954926D));
            this.InitiatedTextBox.Name = "InitiatedTextBox";
            this.InitiatedTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.6833460330963135D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.InitiatedTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.InitiatedTextBox.Value = "";
            // 
            // SmiTypeTextBox
            // 
            this.SmiTypeTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.4999997615814209D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.SmiTypeTextBox.Name = "SmiTypeTextBox";
            this.SmiTypeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.6834249496459961D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.SmiTypeTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.SmiTypeTextBox.Value = "";
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20007865130901337D);
            this.reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.UrlTextBox});
            this.reportFooterSection1.Name = "reportFooterSection1";
            // 
            // UrlTextBox
            // 
            this.UrlTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.UrlTextBox.Name = "UrlTextBox";
            this.UrlTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.1833457946777344D), Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672D));
            this.UrlTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.UrlTextBox.Value = "textBox2";
            // 
            // ClippingReport
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detail,
            this.pageFooterSection1,
            this.pageHeaderSection1,
            this.reportFooterSection1});
            this.Name = "ClippingReport";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Cm(1D);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Cm(1.5D);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Cm(1D);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Cm(1D);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(7.2834644317626953D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.List list1;
        private Telerik.Reporting.Panel panel1;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.PictureBox pictureBox2;
        private Telerik.Reporting.PictureBox pictureBox1;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.HtmlTextBox MassMediaNameTextBox;
        private Telerik.Reporting.ReportFooterSection reportFooterSection1;
        private Telerik.Reporting.Shape shape1;
        private Telerik.Reporting.HtmlTextBox PublicationDateTextBox;
        private Telerik.Reporting.TextBox UrlTextBox;
        private Telerik.Reporting.HtmlTextBox InitiatedTextBox;
        private Telerik.Reporting.HtmlTextBox SmiTypeTextBox;
    }
}