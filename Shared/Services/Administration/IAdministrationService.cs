using Shared.Core.Services;
using Shared.Dtos.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services.Administration
{
    public interface IAdministrationService : IService
    {
        AdministrationDto Read();
    }
}
