using System.Collections.Generic;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;
using Volvo.NVS.Persistence.Repositories;

namespace Volvo.LAT.POLineDomain.DomainLayer.RepositoryInterfaces
{
    public interface IInvoiceReportRepository : IGenericRepository<InvoicingReport, long>
    {
        InvoicingReport FindInvoiceReportsById(long number);

        IEnumerable<InvoicingReport> GetAllInvoiceReports(string invoiceReportId = null);

        InvoicingReport GetInvoiceReportDataById(string invoiceReportId);

        bool SaveInvoicingReport(InvoicingReport reportObj);
    }
}
