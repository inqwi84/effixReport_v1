﻿using System.Collections.Generic;
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
    public partial class InitiatedReport : Report
    {
        private ObservableCollection<EF_Publication> _pList=new ObservableCollection<EF_Publication>();
        public InitiatedReport()
        {
            InitializeComponent();
            chart1.NeedDataSource += DiagramReport_NeedDataSource;
        }

        public InitiatedReport(IEnumerable<EF_Publication> list)
        {
            InitializeComponent();
            _pList=new ObservableCollection<EF_Publication>(list);
            chart1.NeedDataSource += DiagramReport_NeedDataSource;
        }

        void DiagramReport_NeedDataSource(object sender, EventArgs e)
        {
            using (var model = new EntitiesModel())
            {
                var initiatedList = model.EF_Initiateds;
                var procChart = (Telerik.Reporting.Processing.Chart)sender;
                var defChart = (Chart)procChart.ItemDefinition;
                defChart.IntelligentLabelsEnabled = false;
                var serie = new ChartSeries();
                serie.Type = ChartSeriesType.Pie;
                serie.Clear();
                serie.Appearance.LegendDisplayMode = ChartSeriesLegendDisplayMode.ItemLabels;
                foreach (var item in initiatedList)
                {
                    if (item.IsInitiated.Contains("выбрано")) continue;
                    var listItem = item.ID;
                    var count = _pList.Count(item1 => item1.Is_initiated == listItem);
                    var chartS = new ChartSeriesItem
                    {
                        YValue = (double)count,
                        Name = item.IsInitiated + "  " + count.ToString()
                    };
                    chartS.Appearance.Exploded = true;
                    chartS.Label.TextBlock.Text = count.ToString() + " - #%";
                    serie.Items.Add(chartS);
                }
                defChart.Series.Add(serie);
            }
        }
    }
}