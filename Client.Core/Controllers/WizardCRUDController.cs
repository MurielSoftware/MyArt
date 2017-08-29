using Client.Core.Constants;
using Shared.Core.Dtos;
using Shared.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Client.Core.Controllers
{
    public abstract class WizardCRUDController<T, U> : DialogCRUDController<T, U>
        where T : BaseDto
        where U : ICRUDService<T>
    {
        public override ActionResult Create(Guid? id)
        {
            return base.Create(id);
        }

        [HttpPost]
        public override ActionResult Create(T dto)
        {
            return null;
        }

        public ActionResult Previous(int currentStep)
        {
            return null;
        }

        public ActionResult Next(T dto, int currentStep)
        {
            ActionResult nextActionResult = DoNext(dto, currentStep);
            if(nextActionResult == null)
            {
                return Finish(dto, currentStep);
            }
            return nextActionResult;
        }

        public int GetNextStep(int currentStep)
        {
            return currentStep == GetWizardStepsCount() ? currentStep : currentStep + 1;
        }

        public int GetPreviousStep(int currentStep)
        {
            return currentStep == 0 ? currentStep : currentStep - 1;
        }

        protected abstract ActionResult Finish(T dto, int currentStep);
        protected abstract ActionResult DoNext(T dto, int currentStep);
        protected abstract int GetWizardStepsCount();
    }
}
