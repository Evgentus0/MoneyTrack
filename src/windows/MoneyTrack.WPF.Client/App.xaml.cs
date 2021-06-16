﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MoneyTrack.Core.AppServices.DependencyResolver;
using MoneyTrack.WPF.DomainServices.DependencyResolver;
using MoneyTrack.WPF.Infrastructure.Settings;
using MoneyTrack.WPF.Client.ViewModels;

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
                
            });

            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();

            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddAppServices();
            services.AddDomainServices();

            services.AddSingleton(Settings);
            services.AddScoped(provider =>
            {
                var mainWindow = new MainWindow();
                mainWindow.DataContext = provider.GetRequiredService<MainViewModel>();
                mainWindow.InitializeResources();

                return mainWindow;
            });

            services.AddScoped<MainViewModel>();
            services.AddScoped<HomeViewModel>();
            services.AddScoped<AnalyticsViewModel>();
        }
    }
}
