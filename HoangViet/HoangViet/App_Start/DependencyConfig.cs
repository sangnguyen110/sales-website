using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac.Integration.Mvc;
using Repository.Pattern.Ef6;
using Repository.Pattern.UnitOfWork;
using HoangViet.Models;
using Repository.Pattern.DataContext;
using HoangViet.Services.Catalog;
using Repository.Pattern.Repositories;
using System.Web.Mvc;
using Service.Pattern;

namespace HoangViet.App_Start
{
    public class DependencyConfig
    {
        public static void RegisterServices(ContainerBuilder builder)
        {
            //builder.RegisterAssemblyTypes(
            //        typeof(MvcApplication).Assembly
            //        ).PropertiesAutowired();
            builder.RegisterControllers(typeof(MvcApplication).Assembly).InstancePerHttpRequest();
            //deal with your dependencies here
            builder.RegisterType<UnitOfWork>().As<IUnitOfWorkAsync>().InstancePerHttpRequest();
            builder.RegisterType<HoangVietDbContext>().As<IDataContextAsync>().InstancePerHttpRequest();
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerHttpRequest();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepositoryAsync<>)).InstancePerHttpRequest();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerHttpRequest();
            //builder.RegisterInstance(PerHttpSafeResolve<IDataContextAsync>());
            builder.RegisterModule<AutofacWebTypesModule>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
        //public static Func<T> PerHttpSafeResolve<T>()
        //{
        //    return () => DependencyResolver.Current.GetService<T>();
        //} 
    }
}