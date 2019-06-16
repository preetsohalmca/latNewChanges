using AutoMapper;
using Volvo.POS.OrderDomain.DomainLayer.Entities;
using Volvo.POS.Proxy.Warehouse.RequestReply.Contracts;

namespace Volvo.POS.Proxy.Warehouse.RequestReply.Translators
{
    /// <summary>
    /// Defines an AutoMapper profile for the <see cref="PartReservationRequest"/>.
    /// </summary>
    public class WarehouseRequestProfile : Profile
    {
        /// <summary>
        /// Gets the name of the profile.
        /// </summary>
        public override string ProfileName => "WarehouseRequestProfile";

        /// <summary>
        /// Configures the mapping profile.
        /// </summary>
        protected override void Configure()
        {
            CreateMap<Order, PartReservationRequest>()
                .ForMember(dest => dest.OrderNumber, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.OrderRows, opt => opt.MapFrom(src => src.OrderLines));

            CreateMap<OrderLine, PartReservationRequestPart>()
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Part.Number))
                .ForMember(dest => dest.NumberSpecified, opt => opt.UseValue(true))
                .ForMember(dest => dest.RequestedQuantitySpecified, opt => opt.UseValue(true));
        }
    }
}