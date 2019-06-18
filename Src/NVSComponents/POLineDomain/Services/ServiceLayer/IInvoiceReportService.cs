namespace Volvo.LAT.POLineDomain.ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using Volvo.LAT.POLineDomain.DomainLayer.Entities;
    using Volvo.LAT.POLineDomain.DomainLayer.Projections;
    using Volvo.LAT.POLineDomain.ServiceLayer.Contracts;

    /// <summary>
    /// Service interface for POLineDomain component
    /// </summary>
    [ContractClass(typeof(InvoiceReportServiceContract))]
    public interface IInvoiceReportService
    {
        /// <summary>
        /// GetApplicationName
        /// </summary>
        /// <returns>WERWR</returns>
        IEnumerable<InvoicingReport> GetInvoicingReports();

        InvoicingReport GetInvoicingReportById(string invoiceReportId);

        bool DeleteInvoicingReportById(string invoiceReportId);

        bool SaveInvoicingReport(InvoicingReport reportObj);
    }
}
