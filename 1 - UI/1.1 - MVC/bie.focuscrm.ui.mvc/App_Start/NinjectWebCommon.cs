[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(bie.focuscrm.ui.mvc.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(bie.focuscrm.ui.mvc.App_Start.NinjectWebCommon), "Stop")]

namespace bie.focuscrm.ui.mvc.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    using application.Interfaces;
    using application;

    using domain.Interfaces.Service;
    using domain.Interfaces.Repository;
    using domain.Services;
    using infra.data.Repository;
    using infra.data;

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

            kernel.Bind(typeof(IAppServiceBase<>)).To(typeof(AppServiceBase<>));
            kernel.Bind(typeof(IServiceBase<>)).To(typeof(ServiceBase<>));
            kernel.Bind(typeof(IRepositoryBase<>)).To(typeof(RepositoryBase<>));

            //Alerta
            kernel.Bind<IAlertaService>().To<AlertaService>();
            kernel.Bind<IAlertaAppService>().To<AlertaAppService>();
            kernel.Bind<IAlertaRepository>().To<AlertaRepository>();




            //Atendimento
            kernel.Bind<IAtendimentoService>().To<AtendimentoService>();
            kernel.Bind<IAtendimentoAppService>().To<AtendimentoAppService>();
            kernel.Bind<IAtendimentoRepository>().To<AtendimentoRepository>();

            //Empresa
            kernel.Bind<IEmpresaService>().To<EmpresaService>();
            kernel.Bind<IEmpresaAppService>().To<EmpresaAppService>();
            kernel.Bind<IEmpresaRepository>().To<EmpresaRepository>();

            //Filial
            kernel.Bind<IFilialService>().To<FilialService>();
            kernel.Bind<IFilialAppService>().To<FilialAppService>();
            kernel.Bind<IFilialRepository>().To<FilialRepository>();

            //Pendencia
            kernel.Bind<IPendenciaService>().To<PendenciaService>();
            kernel.Bind<IPendenciaAppService>().To<PendenciaAppService>();
            kernel.Bind<IPendenciaRepository>().To<PendenciaRepository>();

            //Pesquisa
            kernel.Bind<IPesquisaService>().To<PesquisaService>();
            kernel.Bind<IPesquisaAppService>().To<PesquisaAppService>();
            kernel.Bind<IPesquisaRepository>().To<PesquisaRepository>();

            //RespostaPesquisa
            kernel.Bind<IRespostaPesquisaService>().To<RespostaPesquisaService>();
            kernel.Bind<IRespostaPesquisaAppService>().To<RespostaPesquisaAppService>();
            kernel.Bind<IRespostaPesquisaRepository>().To<RespostaPesquisaRepository>();

            //Usuario
            kernel.Bind<IUsuarioService>().To<UsuarioService>();
            kernel.Bind<IUsuarioAppService>().To<UsuarioAppService>();
            kernel.Bind<IUsuarioRepository>().To<UsuarioRepository>();

            // Setor
            kernel.Bind<ISetorService>().To<SetorService>();
            kernel.Bind<ISetorAppService>().To<SetorAppService>();
            kernel.Bind<ISetorRepository>().To<SetorRepository>();


            //Notificação
            kernel.Bind<INotificationAppService>().To<NotificationAppService>();



        }
    }
}

