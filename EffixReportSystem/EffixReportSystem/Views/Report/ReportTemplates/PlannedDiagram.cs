using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class PlannedReport : Report
    {
        private ObservableCollection<EF_Publication> _pList=new ObservableCollection<EF_Publication>();
        private string _firstLabel;
        private string _secondLabel;
        public PlannedReport()
        {
            InitializeComponent();
            chart1.NeedDataSource += DiagramReport_NeedDataSource;
        }

        public PlannedReport(IEnumerable<EF_Publication> list, string param, string firstLabel, string secondLabel)
        {
            InitializeComponent();
            _firstLabel = firstLabel;
            _secondLabel = secondLabel;
            _pList=new ObservableCollection<EF_Publication>(list);
            chart1.NeedDataSource += DiagramReport_NeedDataSource;
        }

        void DiagramReport_NeedDataSource(object sender, EventArgs e)
        {
            using (var model = new EntitiesModel())
            {
              //  var products = _pList;
                var first = _pList.Count(item1 => item1.Is_initiated == 1);
                var second = _pList.Count(item1 => item1.Is_initiated == 0);
                var procChart = (Telerik.Reporting.Processing.Chart) sender;
                var defChart = (Chart) procChart.ItemDefinition;
                defChart.IntelligentLabelsEnabled = false;
                var serie = new ChartSeries();
                serie.Type = ChartSeriesType.Pie;
                serie.Clear();
                serie.Appearance.LegendDisplayMode = ChartSeriesLegendDisplayMode.ItemLabels;

                    var item = new ChartSeriesItem
                        {
                            YValue = (double) first, 
                            Name = _firstLabel+"  "+first.ToString()};
                item.Appearance.Exploded = true;
                item.Label.TextBlock.Text = first.ToString() + " - #%";

                var item2 = new ChartSeriesItem
                {
                    YValue = (double)second,
                    Name = _secondLabel+" " + second.ToString()
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