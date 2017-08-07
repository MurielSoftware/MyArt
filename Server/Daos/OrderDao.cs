using Server.Model;
using Shared.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Daos
{
    public class OrderDao : GenericDao
    {
        public OrderDao(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        /// <summary>
        /// Changes the order.
        /// </summary>
        /// <typeparam name="T">The type of the entity</typeparam>
        /// <param name="sourceId">The source ID</param>
        /// <param name="targetId">The target ID</param>
        internal void ChangeOrder<T>(Guid sourceId, Guid targetId) where T : BaseEntity
        {
            T sourceEntity = Find<T>(sourceId);
            T targetEntity = Find<T>(targetId);

            ChangeOrder(Find<T>(sourceId), Find<T>(targetId));
        }

        /// <summary>
        /// Changes the order.
        /// </summary>
        /// <typeparam name="T">The type of the entity</typeparam>
        /// <param name="sourceEntity">The source order entity</param>
        /// <param name="targetEntity">The target order entity</param>
        internal void ChangeOrder<T>(T sourceEntity, T targetEntity) where T : BaseEntity
        {
            if (sourceEntity == null || targetEntity == null)
            {
                return;
            }

            IOrderableEntity sourceOrderEntity = (IOrderableEntity)sourceEntity;
            IOrderableEntity targetOrderEntity = (IOrderableEntity)targetEntity;

            int tempOrder = sourceOrderEntity.Order;
            sourceOrderEntity.Order = targetOrderEntity.Order;
            targetOrderEntity.Order = tempOrder;

            Persist(sourceEntity);
            Persist(targetEntity);
        }
    }
}
