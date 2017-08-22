using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Core.AfterSaves
{
    public class AfterFailSaveParam
    {
        public virtual string ValidationMessage { get; set; }

        private AfterFailSaveParam(string validationMessage)
        {
            ValidationMessage = validationMessage;
        }

        public static AfterFailSaveParam Create(string validationMessage)
        {
            return new AfterFailSaveParam(validationMessage);
        }
    }
}
