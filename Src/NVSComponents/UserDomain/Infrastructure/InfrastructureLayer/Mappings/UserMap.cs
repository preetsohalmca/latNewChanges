using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using Volvo.LAT.UserDomain.DomainLayer.Entities;

namespace Volvo.LAT.UserDomain.InfrastructureLayer.Mappings
{
    public class UserMap : ClassMapping<User>
    {
        public UserMap()
        {
            this.Table("[Users]");
            this.Schema("[dbo]");
            this.Id(x => x.UserID, map => {
                map.Column("User_ID");
                map.Generator(Generators.Assigned);
            });
            this.Property(x => x.Username);
            this.Property(x => x.Name);
            this.Property(x => x.TimeStamp);

            this.ManyToOne(x => x.UserRole, map =>
            {
                map.Column("Role_ID");
                map.Lazy(LazyRelation.NoLazy);
                map.Cascade(Cascade.None);
            });
            //Table("Users");

            //Schema("[dbo]");

            //Lazy(true);
            //Id(
            //    x => x.Id,
            //    m => m.Generator(Generators.HighLow, g => g.Params(new
            //    {
            //        table = "HiLoValues",
            //        column = "NextHigh",
            //        max_lo = 100,
            //        where = $"EntityName = '{typeof(User).Name.ToLowerInvariant()}'"
            //    })));

            //Property(
            //    x => x.Username,
            //    m =>
            //    {
            //        m.NotNullable(true);
            //        m.Unique(true);
            //    });
            //Property(x => x.);
            //Property(x => x.LastName);
            //Property(x => x.LatestLogin);
            //Property(
            //    x => x.Role,
            //    m =>
            //    {
            //        m.NotNullable(true);
            //        m.Type<EnumStringType<Role>>();
            //    });
            //Property(x => x.IsDeleted, m => m.NotNullable(true));

            //// NHibernate Version is used when you want to implement Optimistic concurrency control.
            //Version(x => x.Version, m => m.Column("Version"));
        }
    }
}
