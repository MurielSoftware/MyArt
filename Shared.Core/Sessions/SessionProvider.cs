using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Shared.Core.Sessions
{
    public class SessionProvider
    {
        private static SessionProvider INSTANCE = null;

        public SessionProvider()
        {

        }

        public static SessionProvider GetInstance()
        {
            if (INSTANCE == null)
            {
                INSTANCE = new SessionProvider();
            }
            return INSTANCE;
        }

        /// <summary>
        /// Adds the session to the Context.
        /// </summary>
        /// <param name="session">The session to add</param>
        public void AddSession(AbstractSession session)
        {
            HttpContext.Current.Session.Add(session.Name, session);
        }

        /// <summary>
        /// Gets the session from the context.
        /// </summary>
        /// <param name="name">The name of the session</param>
        /// <returns>Returns the session</returns>
        public T GetSession<T>(string name) where T : AbstractSession
        {
            if (HttpContext.Current == null)
            {
                return null;
            }
            return HttpContext.Current.Session[name] as T;
        }

        /// <summary>
        /// Removes the session from the context.
        /// </summary>
        /// <param name="session">The session to remove</param>
        public void RemoveSession(AbstractSession session)
        {
            HttpContext.Current.Session.Remove(session.Name);
        }

        /// <summary>
        /// Removes the session from the context.
        /// </summary>
        /// <param name="name">The name of the session to remove</param>
        public void RemoveSession(string name)
        {
            HttpContext.Current.Session.Remove(name);
        }

        /// <summary>
        /// Clears the sessions in the context.
        /// </summary>
        public void ClearSession()
        {
            HttpContext.Current.Session.Clear();
        }
    }
}
