using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using GameStoreBLL.Services.Abstraction;
using GameStoreBLL.Services.Implemantation;
using GameStoreDAL;
using GameStoreDAL.Repository.Abstract;
using GameStoreDAL.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStoreClient.Utilits
{
    public class AutofacConfiguration
    {
        public static void Configurate()
        {
            //1.ContainerBuilder
            var builder = new ContainerBuilder();
            //2.Register controllers in app
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            //3 Register Types
            builder.RegisterType<ApplicationContext>().As<DbContext>().SingleInstance();
            builder.RegisterGeneric(typeof(EFRepository<>)).As(typeof(IGenericRepository<>));
            builder.RegisterType<GameService>().As<IGameService>();
            builder.RegisterType<GenreService>().As<IGenreService>();
            builder.RegisterType<DeveloperService>().As<IDeveloperService>();
            builder.RegisterType<AddressService>().As<IAddressService>();
            builder.RegisterType<OrderService>().As<IOrderService>();
            var configurateMapper = new MapperConfiguration(cfg => cfg.AddProfile(new AutomapperConfig()));
            builder.RegisterInstance(configurateMapper.CreateMapper());
            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }
    }
}