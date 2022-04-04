using GalaSoft.MvvmLight;
using Huida81.Core.Contracts.Services;
using Huida81.Core.Enums;
using Huida81.Core.Models;
using Huida81_WPF.Contracts.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;

namespace Huida81_WPF.ViewModels
{
    public class BIOSViewModel : ViewModelBase, INavigationAware
    {
        private readonly IWin32InfoService _win32InfoService;
        private Win32Info _selected;

        public Win32Info Selected
        {
            get { return _selected; }
            set { Set(ref _selected, value); }
        }

        public ObservableCollection<Win32Info> Win32InfoItems { get; private set; } = new ObservableCollection<Win32Info>();

        public BIOSViewModel(IWin32InfoService win32InfoService)
        {
            _win32InfoService = win32InfoService;
        }

        public async void OnNavigatedTo(object parameter)
        {
            Win32InfoItems.Clear();

            var data = await _win32InfoService.GetWin32InfoDataAsync(Win32InfoKey.Win32_BIOS);

            foreach (var item in data)
            {
                Win32InfoItems.Add(item);
            }

            Selected = Win32InfoItems.First();
        }

        public void OnNavigatedFrom()
        {
        }
    }
}
