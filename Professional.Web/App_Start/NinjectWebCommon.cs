[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Professional.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Professional.Web.App_Start.NinjectWebCommon), "Stop")]

namespace Professional.Web.App_Start
{
    using System;
    using System.Web;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Professional.Data;
    using Professional.Data.Contracts;
    using Professional.Data.Repositories;
    using Professional.Web.Infrastructure.HtmlSanitise;
    using Professional.Web.Infrastructure.Services.Contracts;
    using Professional.Web.Infrastructure.Services;
    using Professional.Web.Infrastructure.Caching;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IApplicationDbContext>().To<ApplicationDbContext>();
            kernel.Bind<IApplicationData>().To<ApplicationData>();
            kernel.Bind(typeof(IRepository<>)).To(typeof(GenericRepository<>));

            kernel.Bind(typeof(ISanitiser)).To(typeof(HtmlSanitizerAdapter));

            kernel.Bind(typeof(ICacheService)).To(typeof(InMemoryCache));
            kernel.Bind(typeof(IHomeServices)).To(typeof(HomeServices));
            kernel.Bind(typeof(IListingServices)).To(typeof(ListingServices));
            kernel.Bind(typeof(IProfileServices)).To(typeof(ProfileServices));
        }        
    }
}
