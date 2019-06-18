namespace Volvo.LAT.POLineDomain.DomainLayer.Projections
{
    /// <summary>
    /// Projects a part entity for the part selection purposes.
    /// </summary>
    public class POLineSelection
    {
        public virtual string PoNumber { get; set; }

        public virtual string RequesterName { get; set; }
         

        public virtual decimal StartDate { get; set; }

        public virtual decimal EndDate { get; set; }

        public virtual int PoLine { get; set; }
        public virtual string OwnerName { get; set; }

        
    }
}
