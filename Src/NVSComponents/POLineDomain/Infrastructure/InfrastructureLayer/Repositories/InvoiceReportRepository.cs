using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;
using Volvo.LAT.POLineDomain.DomainLayer.RepositoryInterfaces;
using Volvo.NVS.Persistence.NHibernate.Repositories;

namespace Volvo.LAT.PartDomain.InfrastructureLayer.Repositories
{
    public class InvoiceReportRepository : GenericRepository<InvoicingReport>, IInvoiceReportRepository
    {
        public InvoicingReport GetInvoiceReportDataById(string invoiceReportId)
        {
            var records = this.Session.QueryOver<InvoicingReport>().List();
            var matchedRecord = records.FirstOrDefault(x => x.InvoicingReportID.ToString() == invoiceReportId);
            return matchedRecord;
        }

        public InvoicingReport FindInvoiceReportsById(long number)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InvoicingReport> GetAllInvoiceReports(string invoiceReportId = null)
        {
            var records = this.Session.QueryOver<InvoicingReport>().List();
            return records;
        }

        public bool SaveInvoicingReport(InvoicingReport reportObj)
        {
            using (var transaction = this.Session.BeginTransaction())
            {
                try
                {
                    this.Session.SaveOrUpdate("InvoicingReport", reportObj);
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex) { return false; }
            }
        }
    }
}
