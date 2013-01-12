using System.Collections.Generic;
using Telerik.Reporting.Charting;

namespace EffixReportSystem.Views.Report.ReportTemplates
{
    using System;
    using System.Linq;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for SnapshotReport.
    /// </summary>
    public partial class DiagramReport : Report
    {
        public DiagramReport()
        {
            InitializeComponent();
            chart1.NeedDataSource += DiagramReport_NeedDataSource;
        }

        void DiagramReport_NeedDataSource(object sender, EventArgs e)
        {
            using (var model = new EntitiesModel())
            {
                var products = model.EF_Publications;
                var first = products.Count(item1 => item1.Is_initiated == 1);
                var second = products.Count(item1 => item1.Is_initiated == 0);
                var procChart = (Telerik.Reporting.Processing.Chart) sender;
                var defChart = (Chart) procChart.ItemDefinition;
                defChart.IntelligentLabelsEnabled = false;
                var serie = new ChartSeries();
                serie.Type = ChartSeriesType.Pie;
                serie.Clear();
                serie.Appearance.LegendDisplayMode = Telerik.Reporting.Charting.ChartSeriesLegendDisplayMode.ItemLabels;

                    var item = new ChartSeriesItem
                        {
                            YValue = (double) first, 
                            Name = first.ToString()
                        };
                item.Appearance.Exploded = true;
                item.Label.TextBlock.Text = first.ToString() + " - #%";

                var item2 = new ChartSeriesItem
                {
                    YValue = (double)second,
                    Name = second.ToString()
                };
                item2.Appearance.Exploded = true;
                item2.Label.TextBlock.Text = second.ToString() + " - #%";

                    serie.Items.Add(item);
                    serie.Items.Add(item2);




                defChart.Series.Add(serie);
            }
        }
    }
}