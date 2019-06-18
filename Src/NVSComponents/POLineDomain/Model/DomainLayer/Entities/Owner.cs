namespace Volvo.LAT.POLineDomain.DomainLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
    using Volvo.NVS.Persistence.NHibernate.Entities;
    using System.ComponentModel.DataAnnotations;
    public class Owner : GenericEntity
    {
        public Owner() { }
        public virtual System.Guid OwnerId { get; set; }
        [Required]
        public virtual DateTime TimeStamp { get; set; }
        public virtual string Name { get; set; }
        //public override bool Equals(object obj) => base.Equals(obj) && Equals(obj as Owner);

        //public override int GetHashCode() => base.GetHashCode();

        //private bool Equals(Owner owner) => owner != null
            //&& Name == owner.Name ;

    }
}