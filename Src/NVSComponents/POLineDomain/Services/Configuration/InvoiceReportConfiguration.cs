namespace Volvo.LAT.PartDomain.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Practices.Unity;
    using Volvo.LAT.PartDomain.InfrastructureLayer.Repositories;
    using Volvo.LAT.POLineDomain.DomainLayer;
    using Volvo.LAT.POLineDomain.DomainLayer.RepositoryInterfaces;
    using Volvo.LAT.POLineDomain.ServiceLayer;
    using Volvo.NVS.Core.Unity.Configuration;

   public class InvoiceReportConfiguration : IContainerConfigurator
    {
        public void Configure(IUnityContainer container) => container
           
           .RegisterType<IInvoiceReportService, InvoiceReportService>()
           .RegisterType<IInvoiceReportRepository, InvoiceReportRepository>();
    }
}   

