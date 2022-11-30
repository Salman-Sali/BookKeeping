using Bk.Infrastructure.Context;
using Bk.Infrastructure.Queries.BookEntries;
using Bk.UserInterface.Extentions;
using Bk.UserInterface.Views.Login;
using BK.Application.Commands.Users;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace Bk.UserInterface
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
            this.Dispatcher.UnhandledException += OnDispatcherUnhandledException;
        }

        private void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<AppDbContext>((provider, options) =>
            {
                options.EnableSensitiveDataLogging(false);
            });

            services.AddMediatR(typeof(GetBookEntries).Assembly);
            AssemblyScanner.FindValidatorsInAssembly(typeof(GetBookEntries).Assembly)
            .ForEach(item => services.AddScoped(item.InterfaceType, item.ValidatorType));
            services.AddMediatR(typeof(AddUserCommand).Assembly);
            AssemblyScanner.FindValidatorsInAssembly(typeof(AddUserCommand).Assembly)
            .ForEach(item => services.AddScoped(item.InterfaceType, item.ValidatorType));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(MediatRValidationPipelineBehavior<,>));
            services.AddMediatR(typeof(ServiceCollectionExtensions).GetTypeInfo().Assembly);


            services.AddSingleton<MainWindow>();
            services.AddSingleton<Login>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
            mainWindow.MainContent.Content = _serviceProvider.GetService<Login>();
        }

        void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string errorMessage = e.Exception.Message;
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}
