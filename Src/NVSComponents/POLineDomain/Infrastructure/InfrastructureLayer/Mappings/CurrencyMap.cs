namespace Volvo.LAT.POLineDomain.InfrastructureLayer.Mappings
{
    using System;
    using Volvo.NVS.Persistence.NHibernate.Entities;
    using System.ComponentModel.DataAnnotations;
    using NHibernate.Mapping.ByCode.Conformist;
    using NHibernate.Mapping.ByCode;
    using DomainLayer.Entities;

    public class CurrencyMap : ClassMapping<Currency>
    {
        public CurrencyMap()
        {

            Table("Currency");

            Schema("dbo");
            Lazy(true);
            Id(x => x.CurrencyID, map => { map.Column("Currency_ID"); map.Generator(Generators.Assigned); });
            Property(x => x.TimeStamp, map => map.NotNullable(true));
            Property(x => x.Name, map => map.NotNullable(true));
            Property(x => x.Rate, map => map.NotNullable(true));
            Property(x => x.Year, map => map.NotNullable(true));
        }
    }
}