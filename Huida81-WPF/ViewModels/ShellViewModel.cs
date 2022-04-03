using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using Huida81_WPF.Contracts.Services;
using Huida81_WPF.Properties;

using MahApps.Metro.Controls;

namespace Huida81_WPF.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private HamburgerMenuItem _selectedMenuItem;
        private HamburgerMenuItem _selectedOptionsMenuItem;
        private RelayCommand _goBackCommand;
        private ICommand _menuItemInvokedCommand;
        private ICommand _optionsMenuItemInvokedCommand;
        private ICommand _loadedCommand;
        private ICommand _unloadedCommand;

        public HamburgerMenuItem SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set { Set(ref _selectedMenuItem, value); }
        }

        public HamburgerMenuItem SelectedOptionsMenuItem
        {
            get { return _selectedOptionsMenuItem; }
            set { Set(ref _selectedOptionsMenuItem, value); }
        }

        // TODO WTS: Change the icons and titles for all HamburgerMenuItems here.
        public ObservableCollection<HamburgerMenuItem> MenuItems { get; } = new ObservableCollection<HamburgerMenuItem>()
        {
            new HamburgerMenuGlyphItem() { Label = Resources.ShellMotherboardPage, Glyph = "\uF404", TargetPageType = typeof(MotherboardViewModel) },
            new HamburgerMenuGlyphItem() { Label = Resources.ShellBIOSPage, Glyph = "\uF22C", TargetPageType = typeof(BIOSViewModel) },
            new HamburgerMenuGlyphItem() { Label = Resources.ShellCPUPage, Glyph = "\uE950", TargetPageType = typeof(CPUViewModel) },
            new HamburgerMenuGlyphItem() { Label = Resources.ShellHDDPage, Glyph = "\uE83B", TargetPageType = typeof(HDDViewModel) },
            new HamburgerMenuGlyphItem() { Label = Resources.ShellPartitionPage, Glyph = "\uF168", TargetPageType = typeof(PartitionViewModel) },
            new HamburgerMenuGlyphItem() { Label = Resources.ShellGraphicPage, Glyph = "\uF211", TargetPageType = typeof(GraphicViewModel) },
            new HamburgerMenuGlyphItem() { Label = Resources.ShellDisplayPage, Glyph = "\uE7F4", TargetPageType = typeof(DisplayViewModel) },
            new HamburgerMenuGlyphItem() { Label = Resources.ShellKeyboardPage, Glyph = "\uE765", TargetPageType = typeof(KeyboardViewModel) },
            new HamburgerMenuGlyphItem() { Label = Resources.ShellMousePage, Glyph = "\uE962", TargetPageType = typeof(MouseViewModel) },
            new HamburgerMenuGlyphItem() { Label = Resources.ShellNetworkPage, Glyph = "\uE774", TargetPageType = typeof(NetworkViewModel) },
            new HamburgerMenuGlyphItem() { Label = Resources.ShellActivityPage, Glyph = "\uE7BC", TargetPageType = typeof(ActivityViewModel) },
        };

        public ObservableCollection<HamburgerMenuItem> OptionMenuItems { get; } = new ObservableCollection<HamburgerMenuItem>()
        {
            new HamburgerMenuGlyphItem() { Label = Resources.ShellSettingsPage, Glyph = "\uE713", TargetPageType = typeof(SettingsViewModel) }
        };

        public RelayCommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new RelayCommand(OnGoBack, CanGoBack));

        public ICommand MenuItemInvokedCommand => _menuItemInvokedCommand ?? (_menuItemInvokedCommand = new RelayCommand(OnMenuItemInvoked));

        public ICommand OptionsMenuItemInvokedCommand => _optionsMenuItemInvokedCommand ?? (_optionsMenuItemInvokedCommand = new RelayCommand(OnOptionsMenuItemInvoked));

        public ICommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new RelayCommand(OnLoaded));

        public ICommand UnloadedCommand => _unloadedCommand ?? (_unloadedCommand = new RelayCommand(OnUnloaded));

        public ShellViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private void OnLoaded()
        {
            _navigationService.Navigated += OnNavigated;
        }

        private void OnUnloaded()
        {
            _navigationService.Navigated -= OnNavigated;
        }

        private bool CanGoBack()
            => _navigationService.CanGoBack;

        private void OnGoBack()
            => _navigationService.GoBack();

        private void OnMenuItemInvoked()
            => NavigateTo(SelectedMenuItem.TargetPageType);

        private void OnOptionsMenuItemInvoked()
            => NavigateTo(SelectedOptionsMenuItem.TargetPageType);

        private void NavigateTo(Type targetViewModel)
        {
            if (targetViewModel != null)
            {
                _navigationService.NavigateTo(targetViewModel.FullName);
            }
        }

        private void OnNavigated(object sender, string viewModelName)
        {
            var item = MenuItems
                        .OfType<HamburgerMenuItem>()
                        .FirstOrDefault(i => viewModelName == i.TargetPageType?.FullName);
            if (item != null)
            {
                SelectedMenuItem = item;
            }
            else
            {
                SelectedOptionsMenuItem = OptionMenuItems
                        .OfType<HamburgerMenuItem>()
                        .FirstOrDefault(i => viewModelName == i.TargetPageType?.FullName);
            }

            GoBackCommand.RaiseCanExecuteChanged();
        }
    }
}
