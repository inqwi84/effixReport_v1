using EffixReportSystem.Helper.Classes.Report;

namespace EffixReportSystem.Views.Report.ReportTemplates
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for HeaderReport.
    /// </summary>
    public partial class HeadReport : Telerik.Reporting.Report
    {
        //public HeadReport()
        //{
        //    InitializeComponent();
        //    var obj = new ObjectDataSource { DataSource = (new EF_Report().GetAllReports()) };
        //    this.DataSource = obj;
        //}

        public HeadReport(string projName,DateTime beginPeriod,DateTime endPeriod)
        {
            InitializeComponent();
            var obj = new ObjectDataSource { DataSource = (new EF_Report().GetAllReports(projName, beginPeriod,endPeriod)) };
            this.table2.DataSource = obj;
        }
    }
}