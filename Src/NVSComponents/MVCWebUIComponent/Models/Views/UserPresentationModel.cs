using System.Collections.Generic;
using Volvo.LAT.MVCWebUIComponent.Models.Shared;
using Volvo.LAT.UserDomain.DomainLayer.Entities;

namespace Volvo.LAT.MVCWebUIComponent.Models.Views
{
    public class UserPresentationModel
    {
        public UserModel User { get; set; }

        public bool IsMyProfile { get; set; }

        public bool IsUserAdmin { get; set; }

        public bool IsNewUser { get; set; }
    }
}