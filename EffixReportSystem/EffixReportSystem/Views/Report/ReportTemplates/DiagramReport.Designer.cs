namespace EffixReportSystem.Views.Report.ReportTemplates
{
    partial class DiagramReport
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.Charting.Styles.ChartMargins chartMargins1 = new Telerik.Reporting.Charting.Styles.ChartMargins();
            Telerik.Reporting.Charting.Styles.Corners corners1 = new Telerik.Reporting.Charting.Styles.Corners();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            this.detail = new Telerik.Reporting.DetailSection();
            this.chart1 = new Telerik.Reporting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(4.5000004768371582D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.chart1});
            this.detail.Name = "detail";
            // 
            // chart1
            // 
            this.chart1.BitmapResolution = 96F;
            chartMargins1.Bottom = new Telerik.Reporting.Charting.Styles.Unit(14D, Telerik.Reporting.Charting.Styles.UnitType.Pixel);
            chartMargins1.Left = new Telerik.Reporting.Charting.Styles.Unit(0D, Telerik.Reporting.Charting.Styles.UnitType.Percentage);
            chartMargins1.Right = new Telerik.Reporting.Charting.Styles.Unit(10D, Telerik.Reporting.Charting.Styles.UnitType.Pixel);
            chartMargins1.Top = new Telerik.Reporting.Charting.Styles.Unit(4D, Telerik.Reporting.Charting.Styles.UnitType.Percentage);
            this.chart1.ChartTitle.Appearance.Dimensions.Margins = chartMargins1;
            this.chart1.ChartTitle.Appearance.Position.AlignedPosition = Telerik.Reporting.Charting.Styles.AlignedPositions.Top;
            this.chart1.ChartTitle.TextBlock.Appearance.TextProperties.Color = System.Drawing.Color.Black;
            this.chart1.ChartTitle.TextBlock.Appearance.TextProperties.Font = new System.Drawing.Font("Verdana", 12F);
            this.chart1.ChartTitle.TextBlock.Text = "Инициированные";
            this.chart1.DefaultType = Telerik.Reporting.Charting.ChartSeriesType.Pie;
            this.chart1.ImageFormat = System.Drawing.Imaging.ImageFormat.Emf;
            this.chart1.Legend.Appearance.Border.Color = System.Drawing.Color.Black;
            this.chart1.Legend.Appearance.ItemTextAppearance.TextProperties.Font = new System.Drawing.Font("Verdana", 9F);
            this.chart1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D), Telerik.Reporting.Drawing.Unit.Inch(3.9339065551757812E-05D));
            this.chart1.Name = "chart1";
            this.chart1.PlotArea.Appearance.Border.Color = System.Drawing.Color.Gray;
            corners1.BottomLeft = Telerik.Reporting.Charting.Styles.CornerType.Round;
            corners1.BottomRight = Telerik.Reporting.Charting.Styles.CornerType.Round;
            corners1.RoundSize = 6;
            corners1.TopLeft = Telerik.Reporting.Charting.Styles.CornerType.Round;
            corners1.TopRight = Telerik.Reporting.Charting.Styles.CornerType.Round;
            this.chart1.PlotArea.Appearance.Corners = corners1;
            this.chart1.PlotArea.Appearance.FillStyle.FillType = Telerik.Reporting.Charting.Styles.FillType.Solid;
            this.chart1.PlotArea.Appearance.FillStyle.MainColor = System.Drawing.Color.Silver;
            this.chart1.PlotArea.EmptySeriesMessage.Appearance.Visible = true;
            this.chart1.PlotArea.EmptySeriesMessage.Visible = true;
            this.chart1.PlotArea.XAxis.Appearance.MajorGridLines.Color = System.Drawing.Color.DimGray;
            this.chart1.PlotArea.XAxis.Appearance.MajorGridLines.Width = 0F;
            this.chart1.PlotArea.XAxis.AxisLabel.TextBlock.Appearance.TextProperties.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.chart1.PlotArea.YAxis.Appearance.MajorGridLines.Color = System.Drawing.Color.Black;
            this.chart1.PlotArea.YAxis.Appearance.MinorGridLines.Width = 0F;
            this.chart1.PlotArea.YAxis.Appearance.MinorTick.Width = 0F;
            this.chart1.PlotArea.YAxis.AxisLabel.TextBlock.Appearance.TextProperties.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.chart1.PlotArea.YAxis2.AxisLabel.TextBlock.Appearance.TextProperties.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.chart1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.1000003814697266D), Telerik.Reporting.Drawing.Unit.Inch(4.4999213218688965D));
            this.chart1.Skin = "ExcelClassic";
            // 
            // DiagramReport
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detail});
            this.Name = "SnapshotReport";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Inch(1D);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Inch(1D);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Inch(1D);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Inch(1D);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1});
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(6.2000002861022949D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.Chart chart1;
    }
}