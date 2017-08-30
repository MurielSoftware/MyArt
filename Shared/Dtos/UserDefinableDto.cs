using Shared.Core.Dtos;
using Shared.Core.Dtos.References;
using System;

namespace Shared.Dtos
{
    public class UserDefinableDto : BaseDto
    {
        public virtual bool Public { get; set; }

        public virtual ReferenceString UserCreatorReference { get; set; }
        public virtual Guid? UserCreatorId { get; set; }
    }
}
