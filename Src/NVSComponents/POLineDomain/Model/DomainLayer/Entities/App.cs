namespace Volvo.LAT.POLineDomain.DomainLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
    using Volvo.NVS.Persistence.NHibernate.Entities;
    using System.ComponentModel.DataAnnotations;
    public class App : GenericEntity
    {
        public App() {
            Polines = new List<POLine>();
        }
        public virtual System.Guid AppId { get; set; }
        [Required]
        public virtual DateTime TimeStamp { get; set; }
        [Required]
        public virtual int ApmId { get; set; }
        [Required]
        public virtual string Name { get; set; }
        public virtual string DeliveryManager { get; set; }
        public virtual string DeliveryUnit { get; set; }
        public virtual string Porfolio { get; set; }
        public virtual string SubPortfolio { get; set; }
public virtual IList<POLine> Polines { get; set; }
    }
}