﻿using EffixReportSystem.Helper.Classes.Report;

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
        public HeadReport()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            var obj = new ObjectDataSource { DataSource = (new EF_Report().GetAllReports()) };
            this.DataSource = obj;
            var groupHeaderSection = this.Items.Find("groupHeaderSection1", true)[0];
            Image image1 = Image.FromFile(@"c:\\ing.png");
            groupHeaderSection.Items.Add(new Telerik.Reporting.PictureBox() { Value = image1 });
            groupHeaderSection.Items.RemoveByKey("table2");
            groupHeaderSection.Items.Add(new Telerik.Reporting.PictureBox(){Value=image1});
        }
    }
}