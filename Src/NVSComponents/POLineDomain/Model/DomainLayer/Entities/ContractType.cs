namespace Volvo.LAT.POLineDomain.DomainLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
    using Volvo.NVS.Persistence.NHibernate.Entities;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    public class ContractType : GenericEntity
    {
        public virtual System.Guid ContractTypeId { get; set; }
        [Required]
        public virtual string Name { get; set; }

        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Date")]
        [Required]
        public virtual DateTime TimeStamp { get; set; }

        public override bool Equals(object obj) => base.Equals(obj) && Equals(obj as ContractType);

        public override int GetHashCode() => base.GetHashCode();

        private bool Equals(ContractType contractType) => contractType != null
            && Name == contractType.Name  ;

    }

   
}