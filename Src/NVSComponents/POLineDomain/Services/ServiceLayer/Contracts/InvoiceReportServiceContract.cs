using NHibernate;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;
using Volvo.LAT.POLineDomain.DomainLayer.Projections;
using System;

namespace Volvo.LAT.POLineDomain.ServiceLayer.Contracts
{
    [ContractClassFor(typeof(IInvoiceReportService))]
    public abstract class InvoiceReportServiceContract : IInvoiceReportService
    {
        /// <summary>
        /// GetApplicationName
        /// </summary>
        /// <returns>App</returns>
        public IEnumerable<InvoicingReport> GetInvoicingReports() => default(IEnumerable<InvoicingReport>);

        public InvoicingReport GetInvoicingReportById(string invoiceReportId) => default(InvoicingReport);

        public bool DeleteInvoicingReportById(string invoiceReportId) => default(bool);

        public bool SaveInvoicingReport(InvoicingReport reportObj) => default(bool);
    }
}
