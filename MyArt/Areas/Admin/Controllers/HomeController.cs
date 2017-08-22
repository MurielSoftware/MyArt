using Client.Core.Controllers;
using Shared.Dtos.Paintings;
using Shared.Services.Paintings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shared.Core.Dtos;
using Shared.Services.Administration;

namespace MyArt.Areas.Admin.Controllers
{
    public class HomeController : ServiceController<IAdministrationService>
    {
        public override ActionResult Index()
        {
            return View(GetService().Read());
        }
    }
}