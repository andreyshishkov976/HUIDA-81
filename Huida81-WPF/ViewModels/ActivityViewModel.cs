using GalaSoft.MvvmLight;
using Huida81.Core.Contracts.Services;
using Huida81.Core.Models;
using Huida81_WPF.Contracts.ViewModels;
using System.Collections.ObjectModel;

namespace Huida81_WPF.ViewModels
{
    public class ActivityViewModel : ViewModelBase, INavigationAware
    {
        private readonly IWin32InfoService _win32InfoService;

        public ObservableCollection<Win32Process> Source { get; } = new ObservableCollection<Win32Process>();

        public ActivityViewModel(IWin32InfoService win32InfoService)
        {
            _win32InfoService = win32InfoService;
        }

        public async void OnNavigatedTo(object parameter)
        {
            Source.Clear();

            // Replace this with your actual data
            var data = await _win32InfoService.GetWin32ProcessDataAsync();

            foreach (var item in data)
            {
                Source.Add(item);
            }
        }

        public void OnNavigatedFrom()
        {
        }
    }
}
