namespace Volvo.LAT.POLineDomain.InfrastructureLayer.Mappings
{
    using System;
    using Volvo.NVS.Persistence.NHibernate.Entities;
    using System.ComponentModel.DataAnnotations;
    using NHibernate.Mapping.ByCode.Conformist;
    using NHibernate.Mapping.ByCode;
    using DomainLayer.Entities;

    public class CostCenterMap : ClassMapping<CostCenter>
    {
        public CostCenterMap()
        {

            Table("CostCenter");

            Schema("dbo");
            Lazy(true);
            Id(x => x.CostCenterId, map => { map.Column("CostCenter_ID"); map.Generator(Generators.Assigned); });
            Property(x => x.Name, map => map.NotNullable(true));
            Property(x => x.TimeStamp, map => map.NotNullable(true));
            Property(x => x.FullName, map => map.NotNullable(true));
            Property(x => x.BaseCurrency, map => map.NotNullable(true));
            Property(x => x.CountryCode);           
        }
    }
}