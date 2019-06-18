using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Volvo.POS.OrderDomain.DomainLayer.ValueObjects;
using Volvo.POS.PartDomain.DomainLayer.Projections;
using Volvo.POS.Proxy.Warehouse.Subscribe.Contracts;

namespace Volvo.POS.Proxy.Warehouse.Subscribe.Translator
{
    /// <summary>
    /// The AutoMapper profile translating the objects representing external messages into domain objects.
    /// </summary>
    public class WarehouseSubscribeProfile : Profile
    {
        /// <summary>
        /// Gets the name of the profile.
        /// </summary>
        public override string ProfileName => "WarehouseSubscribeProfile";

        /// <summary>
        /// Configures the profile mappings.
        /// </summary>
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        protected override void Configure()
        {
            CreateMap<PartReservationResponsePart, OrderDomain.DomainLayer.Entities.OrderLine>()
                .ForMember(dest => dest.Part, opt => opt.MapFrom(src => new PartView { Number = src.Number }))
                .ForMember(dest => dest.RequestedQuantity, opt => opt.MapFrom(src => src.RequestedQuantity))
                .ForMember(dest => dest.ReservedQuantity, opt => opt.MapFrom(src => src.ReservedQuantity))
                .ForMember(dest => dest.Order, opt => opt.Ignore())
                .ForMember(dest => dest.ReservationStatus, opt => opt.Ignore())
                .ForMember(dest => dest.Number, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ValidationResults, opt => opt.Ignore())
                .ForMember(dest => dest.Version, opt => opt.Ignore());

            CreateMap<PartsAvailabilityPart, PartNewAvailability>();

            CreateMap<decimal, PartDomain.DomainLayer.Entities.PartAvailability>()
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Date, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Validate, opt => opt.Ignore())
                .ForMember(dest => dest.IsValid, opt => opt.Ignore())
                .ForMember(dest => dest.Version, opt => opt.Ignore())
                .ForMember(dest => dest.ValidationResults, opt => opt.Ignore());
        }
    }
}
