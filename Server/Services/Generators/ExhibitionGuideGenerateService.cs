using Microsoft.Office.Interop.Word;
using Shared.Core.Context;
using Shared.Core.Services;
using Shared.Dtos.Generators;
using Shared.Services.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Generators
{
    public class ExhibitionGuideGenerateService : BaseService, IExhibitionGuideGenerateService
    {
        private static Dictionary<GenerateFormat, ISpecificExhibitionGenerator> GENERATORS = CreateGeneratorsMap();

        private static Dictionary<GenerateFormat, ISpecificExhibitionGenerator> CreateGeneratorsMap()
        {
            return new Dictionary<GenerateFormat, ISpecificExhibitionGenerator>()
            {
                { GenerateFormat.WORD, new ExhibitionWordGenerator() }
            };
        }

        public ExhibitionGuideGenerateService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public void Generate(ExhibitionGenerateDto exhibitionGenerateDto)
        {
            ISpecificExhibitionGenerator specificExhibitionGenerator = GENERATORS[exhibitionGenerateDto.Format];
            specificExhibitionGenerator.Generate(_unitOfWork, exhibitionGenerateDto);
        }
    }
}
