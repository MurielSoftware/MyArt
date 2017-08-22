using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Json
{
    public enum JsonRefreshMode
    {
        NONE,
        //PARTIAL,
        //FULL,
        REFRESH_AFTER_WIZARD_NEXT_STEP,
        REFRESH_AFTER_DIALOG_CLOSE,
        REFRESH_TREE_AFTER_DIALOG_CLOSE,
        REFRESH_AFTER_UPLOAD_IMAGE_TO_RICHTEXTBOX,
    }
}


