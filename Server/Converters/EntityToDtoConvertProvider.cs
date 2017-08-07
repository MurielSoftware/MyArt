using Server.Converters.References.List;
using Server.Converters.References.List.EntityToDto;
using Server.Converters.References.Reference.EntityToDto;
using Server.Model;
using Shared.Core.Context;
using Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Server.Converters
{
    /// <summary>
    /// Base Entity To DTO convert provider.
    /// </summary>
    /// <typeparam name="T">The type of the Entity</typeparam>
    /// <typeparam name="U">The type of the DTO</typeparam>
    public class EntityToDtoConvertProvider<T, U> : BaseConvertProvider<T, U>
        where T : BaseEntity
        where U : BaseDto
    {
        protected override void RegisterConverters()
        {
            base.RegisterConverters();
            _converters.Add(new ReferenceAttributeEntityToDtoConverter<T, U>());
            _converters.Add(new ListReferenceAttributeEntityToDto<T, U>());
        }

        protected override U CreateInstance(IUnitOfWork unitOfWork, T source)
        {
            return base.CreateInstance(unitOfWork, source);
        }
    }
}
