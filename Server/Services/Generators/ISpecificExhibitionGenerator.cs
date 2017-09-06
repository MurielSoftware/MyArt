using Shared.Core.Context;
using Shared.Dtos.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Generators
{
    public interface ISpecificExhibitionGenerator
    {
        void Generate(IUnitOfWork unitOfWork, ExhibitionGenerateDto generateDto);
    }
}
