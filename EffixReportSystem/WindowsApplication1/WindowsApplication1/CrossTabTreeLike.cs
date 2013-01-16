namespace ClassLibrary1
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using System.Data;

    /// <summary>
    /// Summary description for Report1.
    /// </summary>
    public partial class CrossTabTreeLike : Telerik.Reporting.Report
    {
        public CrossTabTreeLike()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

       
    }
    public class MySampleData : DataTable
    {
        public MySampleData()
        {

            this.Columns.Add("Company", typeof(string));
            this.Columns.Add("Item", typeof(string));
            this.Columns.Add("Spend", typeof(int));
            this.Columns.Add("Unit", typeof(string));
            this.Columns.Add("Price", typeof(double));

            this.Rows.Add("Company One", "Item 1", 10, "kg", 10.2);
            this.Rows.Add("Company One", "Item 3", 15, "oz", 1.5);
            this.Rows.Add("Company One", "Item 8", 20, "grs", 50);
            this.Rows.Add("Company Two", "Item 2", 9, "kg", 10.2);
            this.Rows.Add("Company Two", "Item 3", 15, "oz", 1.5);
            this.Rows.Add("Company Two", "Item 7", 20, "grs", 50);

        }
    }

    public class MySampleData2 : DataTable
    {
        public MySampleData2()
        {
            this.Columns.Add("CompanyType", typeof(string));
            this.Columns.Add("Company", typeof(string));
            this.Columns.Add("Item", typeof(string));
            this.Columns.Add("Spend", typeof(int));
            this.Columns.Add("Unit", typeof(string));
            this.Columns.Add("Price", typeof(double));

            this.Rows.Add("Company TypeOne","Company One", "Item 1", 10, "kg", 10.2);
            this.Rows.Add("Company TypeOne", "Company One", "Item 3", 15, "oz", 1.5);
            this.Rows.Add("Company TypeOne", "Company One", "Item 8", 20, "grs", 50);
            this.Rows.Add("Company TypeTwo", "Company Two", "Item 2", 9, "kg", 10.2);
            this.Rows.Add("Company TypeTwo", "Company Two", "Item 3", 15, "oz", 1.5);
            this.Rows.Add("Company TypeTwo", "Company Two", "Item 7", 20, "grs", 50);

        }
    }
}