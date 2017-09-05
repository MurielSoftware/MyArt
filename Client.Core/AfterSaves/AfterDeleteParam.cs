using Shared.Core.Dtos;
using Shared.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Core.AfterSaves
{
    public class AfterDeleteParam : BaseAfterParam
    {
        public DeletionDto DeletionDto { get; private set; }
        public Message Message { get; private set; }
        public virtual string TargetHtml { get; private set; }

        public AfterDeleteParam(DeletionDto deletionDto, Message message, string action, string controller, object routeValues, string targetHtml)
            : base(action, controller, routeValues)
        {
            DeletionDto = deletionDto;
            Message = message;
            TargetHtml = targetHtml;
        }

        public static AfterDeleteParam Create(DeletionDto deletionDto, Message message, string action, string controller, object routeValues, string targetHtml)
        {
            return new AfterDeleteParam(deletionDto, message, action, controller, routeValues, targetHtml);
        }
    }
}
