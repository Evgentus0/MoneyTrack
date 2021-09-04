using Microsoft.Extensions.DependencyInjection;
using MoneyTrack.Clients.Common.Automapper;
using MoneyTrack.Clients.Common.Settings;
using MoneyTrack.Clients.Common.ViewModels;
using MoneyTrack.Clients.Mobile.Models;
using MoneyTrack.Clients.Mobile.Services;
using MoneyTrack.Clients.Mobile.Views;
using MoneyTrack.Core.AppServices;
using MoneyTrack.Core.AppServices.Automapper;
using MoneyTrack.Core.Data.LiteDB;
using MoneyTrack.Core.DomainServices;
using System;
using Xamarin.Forms;

namespace MoneyTrack.Clients.Mobile
{
    public partial class App : Application
    {

        public AppSettings Settings { get; private set; }
        public static IServiceProvider ServiceProvider { get; private set; }

        public App(Action<IServiceCollection> addPlatformServices = null)
        {
            InitializeComponent();

            SetupgServices(addPlatformServices);
            MainPage = new AppShell();
        }

        private void SetupgServices(Action<IServiceCollection> addPlatformServices = null)
        {
            Settings = AppSettings.GetWithDefaultValues();

            var serviceCollection = new ServiceCollection();
            addPlatformServices?.Invoke(serviceCollection);

            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDataStore<Item> , MockDataStore>();

            services.AddAppServices();
            services.AddDomainServices();

            services.AddSingleton(Settings);
            services.AddLiteDb(Settings.LiteDBConnection);

            services.AddAutoMapper(config =>
            {
                config.AddProfile(new DtoModelsMapperProfile());
                config.AddProfile(new DomainModelsDtoMapperProfile());
            });

            ConfigureViewModels(services);
        }

        private void ConfigureViewModels(IServiceCollection services)
        {
            services.AddScoped<MainViewModel>();
            services.AddScoped<HomeViewModel>();
            services.AddScoped<AnalyticsViewModel>();
            services.AddScoped<TransactionListViewModel>();
            services.AddScoped<ManagingViewModel>();
        }

        public static TViewModel GetViewModel<TViewModel>() where TViewModel : BaseViewModel
            => ServiceProvider.GetService<TViewModel>();

        public static T GetService<T>() => ServiceProvider.GetService<T>();

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
