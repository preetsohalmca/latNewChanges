namespace Volvo.LAT.POLineDomain.InfrastructureLayer.Mappings
{
    using System;
    using Volvo.NVS.Persistence.NHibernate.Entities;
    using System.ComponentModel.DataAnnotations;
    using NHibernate.Mapping.ByCode.Conformist;
    using NHibernate.Mapping.ByCode;
    using DomainLayer.Entities;

    public class WbsElementMap : ClassMapping<WbsElement>
    {
        public WbsElementMap()
        {

            Table("WbsElement");

            Schema("dbo");
            Lazy(true);
            Id(x => x.WbsElementID, map => { map.Column("WbsElement_ID"); map.Generator(Generators.Assigned); });
            Property(x => x.TimeStamp, map => map.NotNullable(true));
            Property(x => x.Name);
            Property(x => x.AssignmentCode);
            Property(x => x.WbsElementId);
            Property(x => x.Status);
            Property(x => x.ApmId);
            Property(x => x.ApmName);
            Property(x => x.ItAccount);
            Property(x => x.PpmId);
            Property(x => x.PpmName);
            Property(x => x.CostCenterPosted);
            Property(x => x.CostCenterName);
        }
    }
}