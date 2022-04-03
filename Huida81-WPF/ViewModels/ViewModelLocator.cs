using System;
using System.Windows.Controls;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

using Huida81_WPF.Contracts.Services;
using Huida81_WPF.Contracts.Views;
using Huida81_WPF.Core.Contracts.Services;
using Huida81_WPF.Core.Services;
using Huida81_WPF.Models;
using Huida81_WPF.Services;
using Huida81_WPF.Views;

using Microsoft.Extensions.Configuration;

namespace Huida81_WPF.ViewModels
{
    public class ViewModelLocator
    {
        private IPageService PageService
            => SimpleIoc.Default.GetInstance<IPageService>();

        public ShellViewModel ShellViewModel
            => SimpleIoc.Default.GetInstance<ShellViewModel>();

        public MotherboardViewModel MotherboardViewModel
            => SimpleIoc.Default.GetInstance<MotherboardViewModel>();

        public BIOSViewModel BIOSViewModel
            => SimpleIoc.Default.GetInstance<BIOSViewModel>();

        public CPUViewModel CPUViewModel
            => SimpleIoc.Default.GetInstance<CPUViewModel>();

        public HDDViewModel HDDViewModel
            => SimpleIoc.Default.GetInstance<HDDViewModel>();

        public PartitionViewModel PartitionViewModel
            => SimpleIoc.Default.GetInstance<PartitionViewModel>();

        public GraphicViewModel GraphicViewModel
            => SimpleIoc.Default.GetInstance<GraphicViewModel>();

        public DisplayViewModel DisplayViewModel
            => SimpleIoc.Default.GetInstance<DisplayViewModel>();

        public KeyboardViewModel KeyboardViewModel
            => SimpleIoc.Default.GetInstance<KeyboardViewModel>();

        public MouseViewModel MouseViewModel
            => SimpleIoc.Default.GetInstance<MouseViewModel>();

        public NetworkViewModel NetworkViewModel
            => SimpleIoc.Default.GetInstance<NetworkViewModel>();

        public ActivityViewModel ActivityViewModel
            => SimpleIoc.Default.GetInstance<ActivityViewModel>();

        public SettingsViewModel SettingsViewModel
            => SimpleIoc.Default.GetInstance<SettingsViewModel>();

        public ViewModelLocator()
        {
            // App Host
            SimpleIoc.Default.Register<IApplicationHostService, ApplicationHostService>();

            // Activation Handlers

            // Core Services
            SimpleIoc.Default.Register<IApplicationInfoService, ApplicationInfoService>();
            SimpleIoc.Default.Register<ISystemService, SystemService>();
            SimpleIoc.Default.Register<IFileService, FileService>();
            SimpleIoc.Default.Register<ISampleDataService, SampleDataService>();
            SimpleIoc.Default.Register<IWin32InfoService, Win32InfoService>();

            // Services
            SimpleIoc.Default.Register<IPersistAndRestoreService, PersistAndRestoreService>();
            SimpleIoc.Default.Register<IThemeSelectorService, ThemeSelectorService>();
            SimpleIoc.Default.Register<IPageService, PageService>();
            SimpleIoc.Default.Register<INavigationService, NavigationService>();

            // Window
            SimpleIoc.Default.Register<IShellWindow, ShellWindow>();
            SimpleIoc.Default.Register<ShellViewModel>();

            // Pages
            Register<MotherboardViewModel, MotherboardPage>();
            Register<BIOSViewModel, BIOSPage>();
            Register<CPUViewModel, CPUPage>();
            Register<HDDViewModel, HDDPage>();
            Register<PartitionViewModel, PartitionPage>();
            Register<GraphicViewModel, GraphicPage>();
            Register<DisplayViewModel, DisplayPage>();
            Register<KeyboardViewModel, KeyboardPage>();
            Register<MouseViewModel, MousePage>();
            Register<NetworkViewModel, NetworkPage>();
            Register<ActivityViewModel, ActivityPage>();
            Register<SettingsViewModel, SettingsPage>();
        }

        private void Register<VM, V>()
            where VM : ViewModelBase
            where V : Page
        {
            SimpleIoc.Default.Register<VM>();
            SimpleIoc.Default.Register<V>();
            PageService.Configure<VM, V>();
        }

        public void AddConfiguration(IConfiguration configuration)
        {
            var appConfig = configuration
                .GetSection(nameof(AppConfig))
                .Get<AppConfig>();

            // Register configurations to IoC
            SimpleIoc.Default.Register(() => configuration);
            SimpleIoc.Default.Register(() => appConfig);
        }
    }
}
