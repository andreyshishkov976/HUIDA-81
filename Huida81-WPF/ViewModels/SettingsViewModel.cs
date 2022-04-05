using System;
using System.Windows.Input;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Huida81.Core.Contracts.Services;
using Huida81_WPF.Contracts.Services;
using Huida81_WPF.Contracts.ViewModels;
using Huida81_WPF.Models;
using Microsoft.Win32;

namespace Huida81_WPF.ViewModels
{
    // TODO WTS: Change the URL for your privacy policy in the appsettings.json file, currently set to https://YourPrivacyUrlGoesHere
    public class SettingsViewModel : ViewModelBase, INavigationAware
    {
        private readonly AppConfig _appConfig;
        private SaveFileDialog _saveFileDialog;
        private readonly IThemeSelectorService _themeSelectorService;
        private readonly ISystemService _systemService;
        private readonly IApplicationInfoService _applicationInfoService;
        private IWin32InfoService _infoService;
        private AppTheme _theme;
        private string _versionDescription;
        private ICommand _setThemeCommand;
        private ICommand _privacyStatementCommand;
        private ICommand _saveWin32Info;

        public AppTheme Theme
        {
            get { return _theme; }
            set { Set(ref _theme, value); }
        }

        public string VersionDescription
        {
            get { return _versionDescription; }
            set { Set(ref _versionDescription, value); }
        }

        public ICommand SetThemeCommand => _setThemeCommand ?? (_setThemeCommand = new RelayCommand<string>(OnSetTheme));

        public ICommand PrivacyStatementCommand => _privacyStatementCommand ?? (_privacyStatementCommand = new RelayCommand(OnPrivacyStatement));

        public ICommand SaveWin32Info => _saveWin32Info ?? (_saveWin32Info = new RelayCommand(OnSaveWin32Info));

        public SettingsViewModel(AppConfig appConfig, IThemeSelectorService themeSelectorService, ISystemService systemService, IApplicationInfoService applicationInfoService, IWin32InfoService infoService)
        {
            _appConfig = appConfig;
            _themeSelectorService = themeSelectorService;
            _systemService = systemService;
            _applicationInfoService = applicationInfoService;
            _infoService = infoService;
            CreateSaveFileDialog();
        }

        private void CreateSaveFileDialog()
        {
            _saveFileDialog = new SaveFileDialog();
            _saveFileDialog.Title = "Save report as";
            _saveFileDialog.InitialDirectory = Environment.CurrentDirectory;
            _saveFileDialog.Filter = "Text file (.txt)|*.txt|JSON File (.json)|*.json";
            _saveFileDialog.DefaultExt = "JSON File (.json)|*.json";
            _saveFileDialog.AddExtension = true;
            _saveFileDialog.OverwritePrompt = true;
        }

        private async void OnSaveWin32Info()
        {
            if (_saveFileDialog.ShowDialog() == true)
            {
                await _infoService.SaveWin32InfoDataAsync(_saveFileDialog.InitialDirectory, _saveFileDialog.FileName);
            }
        }

        public void OnNavigatedTo(object parameter)
        {
            VersionDescription = $"{Properties.Resources.AppDisplayName} - {_applicationInfoService.GetVersion()}";
            Theme = _themeSelectorService.GetCurrentTheme();
        }

        public void OnNavigatedFrom()
        {
        }

        private void OnSetTheme(string themeName)
        {
            var theme = (AppTheme)Enum.Parse(typeof(AppTheme), themeName);
            _themeSelectorService.SetTheme(theme);
        }

        private void OnPrivacyStatement()
            => _systemService.OpenInWebBrowser(_appConfig.PrivacyStatement);
    }
}
