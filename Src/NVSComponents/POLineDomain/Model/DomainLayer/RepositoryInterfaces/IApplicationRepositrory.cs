using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;
using Volvo.NVS.Persistence.Repositories;

namespace Volvo.LAT.POLineDomain.DomainLayer.RepositoryInterfaces
{
    public interface IApplicationRepositrory : IGenericRepository<App, long>
    {
        App FindAppById(long number);

        IEnumerable<App> GetAllApps(string appId = null);
    }
}
