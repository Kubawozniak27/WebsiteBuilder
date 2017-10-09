using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteBuilder.Core
{
    public static class IoCContainer
    {
        private static IWindsorContainer ContainerField = new WindsorContainer();

        /// <summary>
        /// returns IoC container
        /// </summary>
        public static IWindsorContainer Container
        {
            get
            {
                return ContainerField;
            }
        }

        /// <summary>
        /// Robi dispose poprzedniego i tworzy nowy container. UWAGA: tylko na potrzeby testów - nie powinno być nigdy użyte w aplikacji.
        /// </summary>
        public static void _resetContainer()
        {
            ContainerField.Dispose();
            ContainerField = new WindsorContainer();
        }

        public static object Resolve(Type service)
        {
            return Container.Resolve(service);
        }
    }
}
