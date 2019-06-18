using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;

namespace Volvo.LAT.PartDomain.DomainLayer.Entities
{
    public class SearchModel
    {
        public IEnumerable<App> Applications { get; set; }
        public IEnumerable<ContractType> ContractTypes { get; set; }
        public IEnumerable<ActivityType> ActivityTypes { get; set; }
        public IEnumerable<Owner> Owners { get; set; }
        public IEnumerable<DDLModel> WBS { get; set; }
        public IEnumerable<DDLModel> AssignmentCodes { get; set; }

        public IEnumerable<DDLModel> Requestores { get; set; }
        public SearchModel()
        {
            this.Applications = new List<App>();
            this.ContractTypes = new List<ContractType>();
            this.ActivityTypes = new List<ActivityType>();
            this.Owners = new List<Owner>();
            this.WBS = new List<DDLModel>();
            this.AssignmentCodes = new List<DDLModel>();
            this.Requestores = new List<DDLModel>();

        }
    }

    public class DDLModel
    {
        public string value { get; set; }
        public string text { get; set; }
    }
}
