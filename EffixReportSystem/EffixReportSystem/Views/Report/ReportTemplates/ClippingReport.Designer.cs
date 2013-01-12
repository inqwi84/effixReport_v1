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
            Telerik.Reporting.Drawing.FormattingRule formattingRule1 = new Telerik.Reporting.Drawing.FormattingRule();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClippingReport));
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            this.detail = new Telerik.Reporting.DetailSection();
            this.list1 = new Telerik.Reporting.List();
            this.panel1 = new Telerik.Reporting.Panel();
            this.pictureBox2 = new Telerik.Reporting.PictureBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.htmlTextBox1 = new Telerik.Reporting.HtmlTextBox();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.textBox2 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(3.2082939147949219D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.list1});
            this.detail.Name = "detail";
            // 
            // list1
            // 
            this.list1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(7D)));
            this.list1.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Inch(3.20825457572937D)));
            this.list1.Body.SetCellContent(0, 0, this.panel1);
            tableGroup1.Name = "ColumnGroup";
            this.list1.ColumnGroups.Add(tableGroup1);
            this.list1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.panel1});
            this.list1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.list1.Name = "list1";
            tableGroup2.Groupings.AddRange(new Telerik.Reporting.Grouping[] {
            new Telerik.Reporting.Grouping(null)});
            tableGroup2.Name = "DetailGroup";
            this.list1.RowGroups.Add(tableGroup2);
            this.list1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7D), Telerik.Reporting.Drawing.Unit.Inch(3.20825457572937D));
            // 
            // panel1
            // 
            formattingRule1.Filters.AddRange(new Telerik.Reporting.Filter[] {
            new Telerik.Reporting.Filter("=RowNumber() % 2", Telerik.Reporting.FilterOperator.Equal, "0")});
            formattingRule1.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(231)))), ((int)(((byte)(203)))));
            this.panel1.ConditionalFormatting.AddRange(new Telerik.Reporting.Drawing.FormattingRule[] {
            formattingRule1});
            this.panel1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pictureBox2});
            this.panel1.Name = "panel1";
            this.panel1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7D), Telerik.Reporting.Drawing.Unit.Inch(3.20825457572937D));
            // 
            // pictureBox2
            // 
            this.pictureBox2.Bindings.Add(new Telerik.Reporting.Binding("Value", "=Fields.Image"));
            this.pictureBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.0999608039855957D), Telerik.Reporting.Drawing.Unit.Inch(3.2081756591796875D));
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
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.39170709252357483D);
            this.pageFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pictureBox1});
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20003938674926758D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.htmlTextBox1});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // htmlTextBox1
            // 
            this.htmlTextBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.699960470199585D), Telerik.Reporting.Drawing.Unit.Inch(3.9339065551757812E-05D));
            this.htmlTextBox1.Name = "htmlTextBox1";
            this.htmlTextBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.4000003337860107D), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582D));
            this.htmlTextBox1.Value = "ghjghjgjgjg";
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.19999949634075165D);
            this.reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox2});
            this.reportFooterSection1.Name = "reportFooterSection1";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.383385181427002D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929D), Telerik.Reporting.Drawing.Unit.Inch(0.19999949634075165D));
            this.textBox2.Value = "endreport";
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
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1});
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(7.2000002861022949D);
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
        private Telerik.Reporting.HtmlTextBox htmlTextBox1;
        private Telerik.Reporting.ReportFooterSection reportFooterSection1;
        private Telerik.Reporting.TextBox textBox2;
    }
}