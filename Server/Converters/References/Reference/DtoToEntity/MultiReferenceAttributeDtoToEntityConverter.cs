using System;
using System.Collections.Generic;
using System.Linq;
using Server.Model;
using Shared.Core.Attributes;
using Shared.Core.Context;
using Shared.Core.Dtos;
using System.Reflection;
using System.Collections;
using Server.Daos;
using Shared.Core.Dtos.References;

namespace Server.Converters.References.Reference.DtoToEntity
{
    /// <summary>
    /// Converts the multi references from DTO To Entity
    /// </summary>
    /// <typeparam name="T">The type of the source entity</typeparam>
    /// <typeparam name="U">The type of the referenced entity</typeparam>
    public class MultiReferenceAttributeDtoToEntityConverter<T, U> : IReferenceConverter
        where T : BaseEntity
        where U : BaseEntity
    {
        public void Convert(IUnitOfWork unitOfWork, BaseEntity sourceEntity, BaseDto dto, PropertyInfo sourcePropertyInfo, ReferenceAttribute referenceAttribute, ReferenceString referenceString)
        {
            PropertyInfo targetProperty = sourceEntity.GetType().GetProperty(referenceAttribute.RefencedPropertyName);
            // TODO ???
            //if(referenceString == null || referenceString.Value == null)
            //{
            //    return;
            //}
            List<Guid> referencedIds = new List<Guid>();
            if (referenceString != null && referenceString.Value != null)
            {
                referencedIds = referenceString.GetIds();
            }

            ICollection<U> referencedEntities = (ICollection<U>)targetProperty.GetValue(sourceEntity);
            if (IsReferenciesCreated(referencedIds, referencedEntities))
            {
                CreateMultiReferences(unitOfWork, sourceEntity, targetProperty, referencedIds, referencedEntities);
            }
            else
            {
                UpdateMultiReference(unitOfWork, sourceEntity, targetProperty, referencedIds, referencedEntities);
            }
        }

        private bool IsReferenciesCreated(List<Guid> referencedIds, ICollection<U> referencedEntities)
        {
            // return referencedIds != null && referencedIds.Count > 0 && referencedEntities == null;
            return referencedEntities == null;
        }

        private void CreateMultiReferences(IUnitOfWork unitOfWork, object entity, PropertyInfo targetProperty, List<Guid> referencedIds, ICollection<U> referencedEntities)
        {
            targetProperty.SetValue(entity, CollectReferencedEntities(unitOfWork, targetProperty, referencedIds));//GetReferencedTargetPropertyGenericType(targetProperty));
        }

        private void UpdateMultiReference(IUnitOfWork unitOfWork, BaseEntity entity, PropertyInfo targetProperty, List<Guid> referencedIds, ICollection<U> referencedEntities)
        {
            GenericDao genericDao = new GenericDao(unitOfWork);
            IList currentReferencedEntities = CollectReferencedEntities(unitOfWork, targetProperty, referencedIds);
            ICollection<U> targetPropertyValue = (ICollection<U>)targetProperty.GetValue(entity);
            targetPropertyValue.Clear();
            genericDao.Attach(entity.GetType(), entity);
            foreach (U currentReferencedEntity in currentReferencedEntities)
            {
                genericDao.Attach<U>(currentReferencedEntity);
            }
            targetPropertyValue.Clear();
            foreach (U currentReferencedEntity in currentReferencedEntities)
            {
                targetPropertyValue.Add(currentReferencedEntity);
            }
        }

        private IList CollectReferencedEntities(IUnitOfWork unitOfWork, PropertyInfo targetProperty, List<Guid> referencedIds)
        {
            GenericDao genericDao = new GenericDao(unitOfWork);
            IList referencedEntities = new List<U>();
            foreach (Guid referencedId in referencedIds)
            {
                referencedEntities.Add(genericDao.FindTracking<U>(referencedId));
            }
            return referencedEntities;
        }

        private Type GetReferencedTargetPropertyGenericType(PropertyInfo targetProperty)
        {
            return targetProperty.PropertyType.GetGenericArguments().FirstOrDefault();
        }
    }
}
