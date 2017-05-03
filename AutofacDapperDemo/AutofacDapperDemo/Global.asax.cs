using Autofac;
using Autofac.Integration.Mvc;
using AutofacDapperDemo.Service;
using AutofacDapperDemo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AutofacDapperDemo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Autofac初始化过程
            RegisterService();
        }

        private void RegisterService()
        {
            ContainerBuilder builder = new ContainerBuilder();
            var assemblys = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var baseType = typeof(IDependency);
            builder.RegisterAssemblyTypes(assemblys.ToArray());
            builder.RegisterAssemblyTypes(assemblys.ToArray())
                .Where(t => baseType.IsAssignableFrom(t) && t != baseType)
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            
            //注册泛型
            builder.RegisterGeneric(typeof(DemoRepository<,>)).As(typeof(IDemoRepository<,>)).InstancePerLifetimeScope();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
