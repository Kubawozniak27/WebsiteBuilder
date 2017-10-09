using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebsiteBuilder.BusinessLogic.Infrastructure;
using WebsiteBuilder.Core;
using WebsiteBuilder.Data;
using WebsiteBuilder.Web.Controllers;

namespace WebsiteBuilder.Web
{
    public static class CastleWeb
    {
        public static void Start()
        {
            var container = IoCContainer.Container;
            container.Install(new IoCInstaller());

            // Create the Controller Factory
            var castleControllerFactory = new CastleControllerFactory(container.Kernel);

            // Add the Controller Factory into the MVC web request pipeline
            ControllerBuilder.Current.SetControllerFactory(castleControllerFactory);
        }
    }

    public class IoCInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var contollers = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.BaseType == typeof(BaseController)
                || x.BaseType == typeof(Controller)
                ).ToList();

            CommonCastleRegistrations.RegisterCommonWebTypes(container);

            foreach (var controller in contollers)
            {

                container.Register(Component.For(controller).LifestyleTransient());

            }
        }
    }

    public class CastleControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel kernel;

        public CastleControllerFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public override void ReleaseController(IController controller)
        {
            kernel.ReleaseComponent(controller);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404, string.Format("The controller for path '{0}' could not be found.", requestContext.HttpContext.Request.Path));
            }
            var controller = (IController)kernel.Resolve(controllerType);
            return controller;
        }
    }

    public static class CommonCastleRegistrations
    {
        public static void RegisterCommonWebTypes(IWindsorContainer container)
        {
            
            container.Register(
               Component.For<WebsiteBuilderDbContext>().ImplementedBy<WebsiteBuilderDbContext>()
                   .DependsOn(Dependency.OnValue("nameOrConnectionString",
                       ConfigurationManager.ConnectionStrings["WebsiteBuilderConnectionString"].ConnectionString))
                   .LifestylePerWebRequest()
               );

            var types = Types.FromAssemblyInThisApplication();
            container.Register(types
                    .BasedOn(typeof(CommandBase)).WithService.Self().LifestyleTransient()
                    .OrBasedOn(typeof(QueryBase)).WithService.Self().LifestyleTransient()
                 );
        }
    }
}