using AutoMapper;
using Volvo.NVS.Utilities.ResourceManagement;
using Volvo.LAT.MVCWebUIComponent.Models.Shared;
using Volvo.LAT.POLineDomain.DomainLayer.Projections;
using POLineDomainEntities = Volvo.LAT.POLineDomain.DomainLayer.Entities;

namespace Volvo.LAT.MVCWebUIComponent.Models.Translators
{
    /// <summary>
    /// The user related automapper mapping profile.
    /// </summary>
    public class POLineDomainMvcProfile : Profile
    {
        /// <summary>
        /// Gets the name of the automapper profile.
        /// </summary>
        public override string ProfileName => "POLineDomainMvcProfile";

        /// <summary>
        /// Configure the user related mappings.
        /// </summary>
        protected override void Configure()
        {
            CreateMap<POLineDomainEntities.POLine, POLineModel>();

            CreateMap<POLineSelection, POLineModel>();

            CreateMap<POLineModel, POLineDomainEntities.POLine>()
                //.ForMember(dest => dest.contractType, opt => opt.ResolveUsing(src => new POLineDomainEntities.ContractType()
                //{ Name = src.contractType }))
                .ForMember(dest => dest.PurchaseOrderLineId, opt => opt.Ignore())
                .ForMember(dest => dest.Version, opt => opt.Ignore())
                 .ForMember(dest => dest.PoLine, opt => opt.Ignore())
                  .ForMember(dest => dest.StartDate, opt => opt.Ignore())
                   .ForMember(dest => dest.EndDate, opt => opt.Ignore())
                .ForMember(dest => dest.OwnerName, opt => opt.Ignore()) ;

            //CreateMap< POLineDomainEntities.ContractType,GUID>().ConvertUsing(src => src.ContractTypeId);


        }
    }
}