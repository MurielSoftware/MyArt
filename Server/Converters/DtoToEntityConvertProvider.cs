using Server.Converters.References.Reference;
using Server.Converters.References.Reference.DtoToEntity;
using Server.Daos;
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
    /// Base DTO To Entity convert provider.
    /// </summary>
    /// <typeparam name="T">The type of the DTO</typeparam>
    /// <typeparam name="U">The type of the Entity</typeparam>
    public class DtoToEntityConvertProvider<T, U> : BaseConvertProvider<T, U> 
        where T : BaseDto
        where U : BaseEntity
    {
        protected override void RegisterConverters()
        {
            base.RegisterConverters();
            _converters.Add(new ReferenceAttributeDtoToEntityConverter<T, U>());
        }

        protected override U CreateInstance(IUnitOfWork unitOfWork, T source)
        {
            if (Guid.Empty.Equals(source.Id))
            {
                return base.CreateInstance(unitOfWork, source);
            }
            return GetUpdatedEntity(unitOfWork, source.Id);
        }

        private U GetUpdatedEntity(IUnitOfWork unitOfWork, Guid id)
        {
            return new GenericDao(unitOfWork).Find<U>(id);
        }
    }
}
