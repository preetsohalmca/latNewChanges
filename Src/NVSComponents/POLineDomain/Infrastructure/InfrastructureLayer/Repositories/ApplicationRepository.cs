using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;
using Volvo.LAT.POLineDomain.DomainLayer.RepositoryInterfaces;
using Volvo.NVS.Persistence.NHibernate.Repositories;

namespace Volvo.LAT.PartDomain.InfrastructureLayer.Repositories
{
    public class ApplicationRepository : GenericRepository<App>, IApplicationRepositrory
    {
        public App FindAppById(long number)
        {
            throw new NotImplementedException();
        }

        public App FindByNumber(long appId)
        {
            //return FindAll().First();
            var test = this.Session.QueryOver<App>().List();
            return test.FirstOrDefault();
            //Session.QueryOver<POLine>().Where(POLine => POLine.PoLine == number).List().FirstOrDefault();
        }

        public IEnumerable<App> GetAllApps(string appId = null)
        {
            var test = this.Session.QueryOver<App>().List();
            return test;
        }
    }
}
