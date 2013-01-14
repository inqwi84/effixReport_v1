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
    /// Summary description for ClippingReport.
    /// </summary>
    public partial class ClippingReport : Report
    {
        public ClippingReport(EF_Publication publication)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            list1.DataSource = new ImageReportsList(publication);
            MassMediaNameTextBox.Value = publication.EF_SMI.Smi_name;
            PublicationDateTextBox.Value = publication.Publication_date.Value.ToShortDateString();
            if (String.IsNullOrEmpty(publication.Url_path)) return;
            UrlTextBox.Value = publication.Url_path;
       //     var navUrl = new NavigateToUrlAction {Url = publication.Url_path};
           // UrlTextBox.Action = navUrl;
        }
        public ClippingReport(EF_Publication publication, string smiType)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            list1.DataSource = new ImageReportsList(publication);
            MassMediaNameTextBox.Value = publication.EF_SMI.Smi_name;
            SmiTypeTextBox.Value = publication.EF_SMI.EF_SMI_Type.Smi_type_name;
            InitiatedTextBox.Value = publication.Is_initiated==0 ? "Неинициированные публикации" : "Инициированные публикации";
            PublicationDateTextBox.Value = publication.Publication_date.Value.ToShortDateString();
            if (String.IsNullOrEmpty(publication.Url_path)) return;
            UrlTextBox.Value = publication.Url_path;
            //     var navUrl = new NavigateToUrlAction {Url = publication.Url_path};
            // UrlTextBox.Action = navUrl;
        }
    }
}