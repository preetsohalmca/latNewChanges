using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Volvo.LAT.PartDomain.DomainLayer.Entities;

namespace Volvo.LAT.MVCWebUIComponent.Models.Email
{
    public class SendEmail
    {
        public string  EbdNumber { get; set; }

        public List<EmailRecipent> Recipents { get; set; }
        
    }
}
