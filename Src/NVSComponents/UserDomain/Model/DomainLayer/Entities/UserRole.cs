using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volvo.LAT.UserDomain.DomainLayer.Entities
{
   public class UserRole
    {
        public virtual Guid RoleID { get; set; }

        public virtual string Name { get; set; }

        public virtual DateTime TimeStamp { get; set; }
    }
}
