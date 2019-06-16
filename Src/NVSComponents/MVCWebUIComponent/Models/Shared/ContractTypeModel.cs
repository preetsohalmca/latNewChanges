using System;
using System.ComponentModel.DataAnnotations;

namespace Volvo.LAT.MVCWebUIComponent.Models.Shared
{
    /// <summary>
    /// The UI specific user role information.
    /// </summary>
    public class ContractTypeModel
    {
       
        public virtual System.Guid ContractTypeId { get; set; }

        [Required]
        public int Number { get; set; }

        public static implicit operator string(ContractTypeModel v)
        {
            throw new NotImplementedException();
        }
    }
}