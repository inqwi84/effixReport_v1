namespace EffixReportSystem.Views.Report.ReportTemplates
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for GroupPageReport.
    /// </summary>
    public partial class GroupPageReport : Telerik.Reporting.Report
    {
        public GroupPageReport()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
        public GroupPageReport(string Name)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            htmlTextBox1.Value = Name;

            //
        }
    }
}