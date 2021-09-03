using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyTrack.Clients.Common.Automapper;
using MoneyTrack.Clients.Common.Settings;
using MoneyTrack.Clients.Common.ViewModels;
using MoneyTrack.Core.AppServices;
using MoneyTrack.Core.AppServices.Automapper;
using MoneyTrack.Core.Data.LiteDB;
using MoneyTrack.Core.DomainServices;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace MoneyTrack.WPF.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }
        public AppSettings Settings { get; private set; }

        protected void OnStartup(object sender, StartupEventArgs e)
        {
            Settings = AppSettings.GetWithDefaultValues();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            serviceCollection.AddAutoMapper(config =>
            {
                config.AddProfile(new DtoModelsMapperProfile());
                config.AddProfile(new DomainModelsDtoMapperProfile());
            });

            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();

            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            WindowsSetup(services);

            services.AddAppServices();
            services.AddDomainServices();

            services.AddSingleton(Settings);
            services.AddLiteDb(Settings.LiteDBConnection);

            ConfigureViewModels(services);
        }

        private static void ConfigureViewModels(IServiceCollection services)
        {
            services.AddScoped<MainViewModel>();
            services.AddScoped<HomeViewModel>();
            services.AddScoped<AnalyticsViewModel>();
            services.AddScoped<TransactionListViewModel>();
            services.AddScoped<ManagingViewModel>();
        }

        private void WindowsSetup(IServiceCollection services)
        {
            services.AddScoped(provider =>
            {
                var mainWindow = new MainWindow();
                mainWindow.DataContext = provider.GetRequiredService<MainViewModel>();
                mainWindow.InitializeResources();

                return mainWindow;
            });
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            if(e.Exception is ValidationException)
            {
                e.Handled = true;
                MessageBox.Show(e.Exception.Message);
            }
        }
    }
}
