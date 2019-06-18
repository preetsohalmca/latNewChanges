namespace Volvo.LAT.POLineDomain.DomainLayer.Entities
{
    using System;
    using Volvo.NVS.Persistence.NHibernate.Entities;
    using System.ComponentModel.DataAnnotations;
    public class WbsElement : GenericEntity
    {
        public WbsElement() { }
        public virtual System.Guid WbsElementID { get; set; }
        [Required]
        public virtual DateTime TimeStamp { get; set; }
        public virtual string AssignmentCode { get; set; }
        [Required]
        public virtual string WbsElementId { get; set; }
        [Required]
        public virtual string Name { get; set; }
        public virtual string Status { get; set; }
        public virtual string ApmId { get; set; }
        public virtual string ApmName { get; set; }
        public virtual string PpmId { get; set; }
        public virtual string PpmName { get; set; }
        public virtual string CostCenterPosted { get; set; }
        public virtual string CostCenterName { get; set; }
        public virtual string ItAccount { get; set; }

        [Required]
        public virtual decimal Rate { get; set; }
        [Required]
        public virtual int Year { get; set; }
        
    }
}