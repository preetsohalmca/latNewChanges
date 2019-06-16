using AutoMapper;
using Volvo.NVS.Utilities.ResourceManagement;
using Volvo.LAT.MVCWebUIComponent.Models.Shared;
using Volvo.LAT.UserDomain.DomainLayer.Projections.Management; 
using UserDomainEntities = Volvo.LAT.UserDomain.DomainLayer.Entities;

namespace Volvo.LAT.MVCWebUIComponent.Models.Translators
{
    /// <summary>
    /// The user related automapper mapping profile.
    /// </summary>
    public class UserDomainMvcProfile : Profile
    {
        /// <summary>
        /// Gets the name of the automapper profile.
        /// </summary>
        public override string ProfileName => "UserDomainMvcProfile";

        /// <summary>
        /// Configure the user related mappings.
        /// </summary>
        protected override void Configure()
        {
            CreateMap<UserDomainEntities.User, UserModel>();

            CreateMap<UserManagement, UserModel>();

            //CreateMap<UserDomainEntities.Role, RoleModel>()
            //   .ForMember(dest => dest.Number, opt => opt.MapFrom(src => (int)src))
            //   .ForMember(dest => dest.Name, opt => opt.MapFrom(src => UserDomain_Resources.ResourceManager.GetEnumValue(src)));

            CreateMap<UserModel, UserDomainEntities.User>()
                .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => (UserDomainEntities.UserRole)src.UserRole))
                //.ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Version, opt => opt.Ignore())
                .ForMember(dest => dest.ValidationResults, opt => opt.Ignore())
                .ForMember(dest => dest.Validate, opt => opt.Ignore());
                //.ForMember(dest => dest.LatestLogin, opt => opt.Ignore());
        }
    }
}