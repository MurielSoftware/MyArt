using Client.Core.Controllers;
using Shared.Core.Constants;
using Shared.Dtos.Generators;
using Shared.Services.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyArt.Areas.Admin.Controllers
{
    public class ExhibitionGenerateController : ServiceController<IExhibitionGuideGenerateService>
    {
        public ActionResult Create(ExhibitionGenerateDto exhibitionGenerateDto)
        {
            return PartialView(exhibitionGenerateDto);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Generate(ExhibitionGenerateDto exhibitionGenerateDto)
        {
            GetService().Generate(exhibitionGenerateDto);
            return View(WebConstants.VIEW_DETAILS, WebConstants.CONTROLLER_EXHIBITION, new { id = exhibitionGenerateDto.ExhibitionId });
        }
    }
}