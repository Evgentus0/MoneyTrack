using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyTrack.Core.AppServices;
using MoneyTrack.Core.AppServices.Automapper;
using MoneyTrack.Core.Data.LiteDB;
using MoneyTrack.Core.DomainServices;
using MoneyTrack.WPF.Client.Automapper;
using MoneyTrack.WPF.Client.ViewModels;
using MoneyTrack.WPF.Infrastructure.Settings;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
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
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
            Settings = Configuration.Get<AppSettings>();
            Settings.LiteDBConnection = Configuration.GetConnectionString("LiteDB");

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

            services.AddScoped<MainViewModel>();
            services.AddScoped<HomeViewModel>();
            services.AddScoped<AnalyticsViewModel>();
            services.AddScoped<TransactionListViewModel>();
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
