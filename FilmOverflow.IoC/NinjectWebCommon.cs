using System;
using System.Web;
using FilmOverflow.DAL;
using FilmOverflow.DAL.UnitOfWork;
using FilmOverflow.IoC;
using FilmOverflow.Services;
using FilmOverflow.Services.Interfaces;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace FilmOverflow.IoC
{
	public static class NinjectWebCommon
	{

		private static readonly Bootstrapper Bootstrapper = new Bootstrapper();


		/// <summary>
		/// Starts the application
		/// </summary>
		public static void Start()
		{
			DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
			DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
			Bootstrapper.Initialize(CreateKernel);
		}

		/// <summary>
		/// Stops the application.
		/// </summary>
		public static void Stop()
		{
			Bootstrapper.ShutDown();
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
				RegisterHubServices(kernel);
				RegisterCommonServices(kernel);

				kernel = new StandardKernel();
				RegisterWebServices(kernel);
				RegisterCommonServices(kernel);

				return kernel;
			}
			catch
			{
				kernel.Dispose();
				throw;
			}
		}

		private static void RegisterWebServices(IKernel kernel)
		{
			kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
			kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
			kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
			kernel.Bind<ApplicationDbContext>().ToSelf().InRequestScope();
			kernel.Bind<ISeanceService>().To<SeanceService>().InRequestScope();
			kernel.Bind<IReservedSeatService>().To<ReservedSeatService>().InRequestScope();
		}

		private static void RegisterCommonServices(IKernel kernel)
		{
			kernel.Bind<IUserManagerService>().To<UserManagerService>().InRequestScope();
			kernel.Bind<IRoleManagerService>().To<RoleManagerService>().InRequestScope();
			kernel.Bind<ICinemaService>().To<CinemaService>().InRequestScope();
			kernel.Bind<IFilmService>().To<FilmService>().InRequestScope();
			kernel.Bind<IPaymentMethodService>().To<PaymentMethodService>().InRequestScope();
			kernel.Bind<IReviewService>().To<ReviewService>().InRequestScope();
			kernel.Bind<ITicketService>().To<TicketService>().InRequestScope();
			kernel.Bind<IHallService>().To<HallService>().InRequestScope();
			kernel.Bind<ISeatService>().To<SeatService>().InRequestScope();
		}

		private static void RegisterHubServices(IKernel kernel)
		{
			kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
			kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
			kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InTransientScope();
			kernel.Bind<ApplicationDbContext>().ToSelf().InTransientScope();
			GlobalHost.DependencyResolver.Register(typeof(IHubActivator), () => new HubActivator(kernel));
			kernel.Bind<ISeanceService>().To<SeanceService>().InTransientScope();
			kernel.Bind<IReservedSeatService>().To<ReservedSeatService>().InTransientScope();
		}
	}
}
