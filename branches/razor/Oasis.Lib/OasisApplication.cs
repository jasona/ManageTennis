namespace Oasis.Lib
{
    using MvcTurbine.ComponentModel;
    using MvcTurbine.Ninject;
    using MvcTurbine.Web;
    using Ninject;
    using MvcTurbine.Routing;
    using Oasis.Lib.Services;
    using System.Configuration;
    using System.Collections.Generic;
    using Oasis.Lib.Components;

    public class OasisApplication : TurbineApplication
    {
        //NOTE: You want to hit this piece of code only once.
        static OasisApplication()
        {
            // Initialize the Ninject Kernel
            IKernel kernel = InitializeNinject();

            // Tell the MVC Turbine runtime to use the initialized kernel
            ServiceLocatorManager.SetLocatorProvider(() => new NinjectServiceLocator(kernel));


            if (ConfigurationManager.AppSettings["SiteType"] == "secure")
            {
                // Preload the ranks
                var userService = kernel.Get<IUserService>();

                RankList.Instance().Ranks = userService.GetRanks();

            }
        }

        /// <summary>
        /// Create a new instance of <see cref="IKernel"/> with the necessary types registered.
        /// </summary>
        /// <returns></returns>
        static IKernel InitializeNinject()
        {
            IKernel kernel = new StandardKernel();

            // Add your type registration here
            kernel.Bind<IContentService>().To<ContentService>();
            kernel.Bind<IBlogService>().To<BlogService>();
            kernel.Bind<IGalleryService>().To<GalleryService>();
            kernel.Bind<IInvoiceService>().To<InvoiceService>();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IEventService>().To<EventService>();
            kernel.Bind<IMessageService>().To<MessageService>();

            return kernel;
        }

    }


}
