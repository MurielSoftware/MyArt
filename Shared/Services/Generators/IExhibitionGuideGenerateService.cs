using Shared.Core.Services;
using Shared.Dtos.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services.Generators
{
    public interface IExhibitionGuideGenerateService : IService
    {
        void Generate(ExhibitionGenerateDto exhibitionGenerateDto);
    }
}
