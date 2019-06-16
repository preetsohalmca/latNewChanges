using System.Collections.Generic;

namespace Volvo.LAT.MVCWebUIComponent.Models.ViewModel
{
    public class CostListViewModel
    {
        public string DateAndYear { get; set; }
        public decimal Cost { get; set; }
        public string PoLineId { get; set; }
        public string PoNumber { get; set; }
    }

    public class PolineCostList
    {

        public PolineCostList()
        {
            this.CostLists = new List<CostListViewModel>();    
        }

        public List<CostListViewModel> CostLists { get; set; }

    }
}