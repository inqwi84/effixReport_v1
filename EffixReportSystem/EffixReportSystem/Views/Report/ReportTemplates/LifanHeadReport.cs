﻿using System.Collections.Generic;
using System.Linq;
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
    public partial class LifanHeadReport : Telerik.Reporting.Report
    {
        //public HeadReport()
        //{
        //    InitializeComponent();
        //    var obj = new ObjectDataSource { DataSource = (new EF_Report().GetAllReports()) };
        //    this.DataSource = obj;
        //}

        public LifanHeadReport(string projName, DateTime beginPeriod, DateTime endPeriod)
        {
            InitializeComponent();
            var obj = new ObjectDataSource { DataSource = (new EF_Report().GetAllReports(projName, beginPeriod,endPeriod)) };
           // this.table2.DataSource = obj;
        }


        //public AvtoAleaJLRHeadReport(List<List<EF_Publication>> pGroupList, List<string> resultNameList)
        //{
        //     InitializeComponent();
        //    foreach (var group in pGroupList)
        //    {
        //        var ind = pGroupList.IndexOf(group);
        //        var obj = new ObjectDataSource { DataSource = (new EF_Report().GetSelectedReports(group)) };
        //        this.table2.DataSource = obj;
        //        this.htmlTextBox1.Value = resultNameList[ind];
        //    }
        //}

        public LifanHeadReport(List<EF_Publication> pGroupList, string resultNameList, int index)
        {
            InitializeComponent();
            var obj = new ObjectDataSource { DataSource = (new EF_Report().GetSelectedReports(pGroupList, index)) };
                this.table2.DataSource = obj;
                this.htmlTextBox1.Value = resultNameList;
        }

        public LifanHeadReport(string pGroupList, List<EF_Publication> resultNameList, int endPeriod)
        {
            InitializeComponent();
            var obj = new ObjectDataSource { DataSource = (new EF_Report().GetSelectedReports(resultNameList, endPeriod)) };
            this.table2.DataSource = obj;
            this.htmlTextBox1.Value = pGroupList;
        }
    }
}