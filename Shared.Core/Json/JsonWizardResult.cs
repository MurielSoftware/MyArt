using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Json
{
    public class JsonWizardResult : JsonDialogResult
    {
        public Guid Id { get; set; }
        public int NextStepIndex { get; set; }

        private JsonWizardResult(bool success, Guid id, object targetElementId, JsonRefreshMode refreshMode, string message, string afterAction, int nextStepIndex)
            : base(success, targetElementId, refreshMode, message, afterAction)
        {
            Id = id;
            NextStepIndex = nextStepIndex;
        }

        public static JsonWizardResult CreateSuccess(Guid id, object targetElementId, int nextStepIndex, string afterAction)
        {
            return new JsonWizardResult(true, id, targetElementId, JsonRefreshMode.REFRESH_AFTER_WIZARD_NEXT_STEP, null, afterAction, nextStepIndex);
        }
    }
}
