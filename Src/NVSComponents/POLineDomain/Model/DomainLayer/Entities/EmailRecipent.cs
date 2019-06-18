using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volvo.LAT.PartDomain.DomainLayer.Entities
{
    public class EmailRecipent
    {
        public string EbdNumber { get; set; }
        public string PurchaseOrderLineId { get; set; }
        public List<RequestorEmail> RequestorEmail { get; set; }
        public List<ContactPersonEmail> ContactPersonEmail { get; set; }
    }
    public class Recipents
    {
        public string EbdNumber{ get; set; }
        public List<RecipentEmail> RecipentEmail { get; set; }
    }

    public class RecipentEmail
    {
        public string EmailRecipent { get; set; }
        public string PolineIds { get; set; }
    }
    public class RequestorEmail
    {
        public string RequestorEmailId { get; set; }
        public int PoLineId { get; set; }
        public string EbdNumber { get; set; }
    }
    public class ContactPersonEmail
    {
        public string ContactPersonEmailId { get; set; }
        public int PoLineId { get; set; }
        public string EbdNumber { get; set; }
    }

    public class Requestor
    {
        public List<int> PoLineId  { get; set; }
        public string RequestorEmail { get; set; }
    }
    public class ContactPerson
    {
        public List<int> PoLineId { get; set; }
        public string ContactPersonEmail { get; set; }
    }

    public class PoLineEmailRecipent
    {
        public string EbdNumber { get; set; }
        public string PurchaseOrderLineId { get; set; }
        public List<Requestor> RequestorEmails { get; set; }
        public List<ContactPerson> ContactPersonEmails { get; set; }

    }

}
