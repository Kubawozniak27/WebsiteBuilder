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

        public static IWindsorContainer Container
        {
            get
            {
                return ContainerField;
            }
        }
    }
}
