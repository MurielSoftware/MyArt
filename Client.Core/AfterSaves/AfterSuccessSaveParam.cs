using Shared.Core.Dtos;
using Shared.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Client.Core.AfterSaves
{
    public class AfterSuccessSaveParam : BaseAfterParam
    {
        public virtual BaseDto Dto { get; private set; }
        public virtual Guid Id { get; set; }
        public virtual string TargetHtmlId { get; private set; }
        public virtual Message Message { get; private set; }
        public virtual int NextStep { get; set; }

        private AfterSuccessSaveParam(Guid id, Message message, string action, string controller, object routeValues, string targetHtmlId, int nextStep)
            : base(action, controller, routeValues)
        {
            Id = id;
            Message = message;
            TargetHtmlId = targetHtmlId;
            NextStep = nextStep;
        }

        private AfterSuccessSaveParam(BaseDto dto, string action, string controller, object routeValues, string targetHtmlId)
            : base(action, controller, routeValues)
        {
            Dto = dto;
            TargetHtmlId = targetHtmlId;
            NextStep = -1;
        }

        public static AfterSuccessSaveParam Create(BaseDto dto, string action, string controller = null, object routeValues = null, string targetHtmlId = null)
        {
            return new AfterSuccessSaveParam(dto, action, controller, routeValues, targetHtmlId);
        }

        public static AfterSuccessSaveParam Create(Guid id, Message message, string action, string controller = null, object routeValues = null, string targetHtmlId = null, int nextStep = -1)
        {
            return new AfterSuccessSaveParam(id, message, action, controller, routeValues, targetHtmlId, nextStep);
        }
    }
}
