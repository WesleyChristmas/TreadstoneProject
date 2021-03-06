using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;
using PartyCafe.Site.Controllers;
using PartyCafe.Site.Models;

namespace PartyCafe.Site.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
//    public class UnityConfig
//    {
//        #region Unity Container
//        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
//        {
//            var container = new UnityContainer();
//            RegisterTypes(container);
//            return container;
//        });

//        /// <summary>
//        /// Gets the configured Unity container.
//        /// </summary>
//        public static IUnityContainer GetConfiguredContainer()
//        {
//            return container.Value;
//        }
//        #endregion

//        /// <summary>Registers the type mappings with the Unity container.</summary>
//        /// <param name="container">The unity container to configure.</param>
//        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
//        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
//        public static void RegisterTypes(IUnityContainer container)
//        {
//            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
//            // container.LoadConfiguration();

//            // TODO: Register your types here
//            // container.RegisterType<IProductRepository, ProductRepository>();

//            container
//                .RegisterType<IDataContextAsync, PartyCafeDbContext>(new PerRequestLifetimeManager())
//                .RegisterType<IUnitOfWorkAsync, UnitOfWork>(new PerRequestLifetimeManager())
//                .RegisterType<UserManager<ApplicationUser>>(new HierarchicalLifetimeManager())
//                .RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new HierarchicalLifetimeManager())
//                .RegisterType<AccountController>(new InjectionConstructor())

//                //Repositories

//                .RegisterType<IRepositoryAsync<FoodMenu>, Repository<FoodMenu>>()
//                .RegisterType<IRepositoryAsync<FoodMenuType>, Repository<FoodMenuType>>()
//                .RegisterType<IRepositoryAsync<Photo>, Repository<Photo>>()
//                .RegisterType<IRepositoryAsync<PhotoType>, Repository<PhotoType>>()
//                .RegisterType<IRepositoryAsync<BlogCalendar>, Repository<BlogCalendar>>()
//                .RegisterType<IRepositoryAsync<BlogHeader>, Repository<BlogHeader>>()
//                .RegisterType<IRepositoryAsync<BlogPhoto>, Repository<BlogPhoto>>();

//                    //Services
///*
//                .RegisterType<IFoodMenuServiceBase, FoodMenuService>()
//                .RegisterType<IImageService, ImageService>()
//                .RegisterType<IBlogCalendarService, BlogCalendarService>()
//                .RegisterType<IBlogService, BlogService>();
//*/
//        }
//    }
}
