using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Volvo.LAT.PartDomain.DomainLayer.Entities;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;

namespace Volvo.LAT.MVCWebUIComponent.Models.ViewModel
{
    public class PoLineViewModel
    {
        public PurchaseOrder PurchaseOrder { get; set; }
        public List<CustomModel> PoLines { get; set; }
    }
}