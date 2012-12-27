using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EffixReportSystem.Helper.Classes.Report
{
    [System.ComponentModel.DataObject]
    public partial class EF_Report : List<EF_Publication>
    {
        public EF_Report(EF_Publication publication)
        {
            
        }

        public EF_Report()
        {

        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<EF_Publication> GetReportByPeriod(long projectId, DateTime startDate,DateTime endDate)
        {
            var result = new List<EF_Publication>();
            using (var model = new EntitiesModel())
            {
                result.AddRange(
                    model.EF_Publications.Where(
                        pub =>
                        pub.Project_id == projectId && pub.Publication_date > startDate &&
                        pub.Publication_date < endDate));
            }
            return result;
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<EF_Publication> GetReportByEvent(long eventId)
        {
            // TODO: Implement your specific business logic here.
            return new System.Collections.Generic.List<EF_Publication>();
        }

        //[System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        //public List<EF_Publication> GetAllReports()
        //{
        //    var result = new List<EF_Publication>();
        //    using (var model = new EntitiesModel())
        //    {
        //        result.AddRange(
        //            model.EF_Publications);
        //    }
        //    return result;
        //}
        //[System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        //public List<TT> GetAllReports()
        //{
        //    var result = new List<TT>();

        //    result.Add(new TT(){AValue = "1"});
        //    result.Add(new TT() { AValue = "2" }); 
        //    result.Add(new TT() { AValue = "3" });
        //    return result;
        //}
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<ReportRS> GetAllReports()
        {
            var result = new List<ReportRS>();
            using (var model = new EntitiesModel())
            {
                int index = 1;
                foreach (var reportRs in model.EF_Publications)
                {
                    result.Add(new ReportRS(reportRs,index));
                    index++;
                }
            }
            return result;
        }
    }

    public class TT
    {
        public string AValue { get; set; }
    }
    public class ReportRS
    {
        public int Index { get; set; }
        public string MassMediaName { get; set; }
        public DateTime PublicationDate { get; set; }
        public string PublicationUrl { get; set; }
        public string PublicationTitle { get; set; }

        public ReportRS(EF_Publication publication,int index)
        {
            Index = index;
            MassMediaName = publication.EF_SMI.Smi_name;
            PublicationDate = (DateTime) publication.Publication_date;
            PublicationUrl = publication.Url_path;
            PublicationTitle = publication.Publication_name;
        }
    }
}
