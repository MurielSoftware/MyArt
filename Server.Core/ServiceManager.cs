using Server.Services.Roles;
using Server.Services.Users;
using Shared.Core.Context;
using Shared.Services.Roles;
using Shared.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Core
{
    public class ServiceManager
    {
        private static System.Reflection.Assembly assembly = System.Reflection.Assembly.Load("Server");

        private IUnitOfWork _unitOfWork;

        public ServiceManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public T Get<T>()
        {
            Type type = assembly.GetTypes().Where(x => x.Name == GetInstanceName(typeof(T))).SingleOrDefault();
            return (T)Activator.CreateInstance(type, new object[] { _unitOfWork });
        }

        private static string GetInstanceName(Type type)
        {
            return type.Name.Substring(1);
        }
    }
}
