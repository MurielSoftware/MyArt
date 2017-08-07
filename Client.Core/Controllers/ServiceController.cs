using Server.Core;
using Shared.Core.Context;
using Shared.Core.Dtos;
using Shared.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Core.Controllers
{
    public abstract class ServiceController<T> : BaseController
        where T : IService

    {
        private ServiceManager _serviceManager;
        private IUnitOfWork _unitOfWork;
        private T _service;

        public ServiceController()
        {
            _unitOfWork = new UnitOfWork();
            _serviceManager = new ServiceManager(_unitOfWork);
            _service = _serviceManager.Get<T>();
        }

        protected T GetService()
        {
            return _service;
        }

        protected IUnitOfWork GetUnitOfWork()
        {
            return _unitOfWork;
        }

        protected ServiceManager GetServiceManager()
        {
            return _serviceManager;
        }

        protected string GetControllerName()
        {
            var routeValues = Request.RequestContext.RouteData.Values;

            if (routeValues.ContainsKey("controller"))
            {
                return (string)routeValues["controller"];
            }
            return string.Empty;
        }
    }
}
