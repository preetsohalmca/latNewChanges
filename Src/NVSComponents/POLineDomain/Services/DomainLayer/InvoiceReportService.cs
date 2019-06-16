#pragma warning disable SA1623 // Property summary documentation must match accessors

namespace Volvo.LAT.POLineDomain.DomainLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NHibernate;
    using UserDomain.ServiceLayer;
    using Volvo.LAT.POLineDomain.DomainLayer.Entities;
    using Volvo.LAT.POLineDomain.DomainLayer.ProjectionRepositoryInterfaces;
    using Volvo.LAT.POLineDomain.DomainLayer.Projections;
    using Volvo.LAT.POLineDomain.DomainLayer.RepositoryInterfaces;
    using Volvo.LAT.POLineDomain.ServiceLayer;
    using System.Transactions;

    public class InvoiceReportService : IInvoiceReportService
    {
        /// <summary>
        /// A Application repository used by the service.
        /// </summary>
        protected IInvoiceReportRepository InvoiceReportRepository { get; }

        public InvoiceReportService(IInvoiceReportRepository invoiceReportRepository)
        {
            this.InvoiceReportRepository = invoiceReportRepository;
        }

        public IEnumerable<InvoicingReport> GetInvoicingReports() => this.InvoiceReportRepository.GetAllInvoiceReports().ToList();


        private void DeleteInvoicingReport(InvoicingReport invoicingReport)
        {
            using (var scope = new TransactionScope())
            {
                InvoiceReportRepository.Remove(invoicingReport);
                scope.Complete();
            }

            //Log.Info($"User has been deleted (User Id: {user.Id}).");
        }

        public InvoicingReport GetInvoicingReportById(string invoiceReportId)
        {
            var record = new InvoicingReport();
            record = this.InvoiceReportRepository.GetInvoiceReportDataById(invoiceReportId);
            return record;
        }

        public bool DeleteInvoicingReportById(string invoiceReportId)
        {
            var record = GetInvoicingReportById(invoiceReportId);
            if (record != null)
            {
                DeleteInvoicingReport(record);
                return true;
            }
            return false;
        }
        public bool SaveInvoicingReport(InvoicingReport reportObj) => this.InvoiceReportRepository.SaveInvoicingReport(reportObj);
    }
}

#pragma warning restore SA1623 // Property summary documentation must match accessors