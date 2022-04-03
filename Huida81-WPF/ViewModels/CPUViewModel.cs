using System;
using System.Collections.ObjectModel;
using System.Linq;

using GalaSoft.MvvmLight;

using Huida81_WPF.Contracts.ViewModels;
using Huida81_WPF.Core.Contracts.Services;
using Huida81_WPF.Core.Models;

namespace Huida81_WPF.ViewModels
{
    public class CPUViewModel : ViewModelBase, INavigationAware
    {
        private readonly IWin32InfoService _win32InfoService;
        private Win32CpuInfo _selected;

        public Win32CpuInfo Selected
        {
            get { return _selected; }
            set { Set(ref _selected, value); }
        }

        public ObservableCollection<Win32CpuInfo> SampleItems { get; private set; } = new ObservableCollection<Win32CpuInfo>();

        public CPUViewModel(IWin32InfoService win32InfoService)
        {
            _win32InfoService = win32InfoService;
        }

        public async void OnNavigatedTo(object parameter)
        {
            SampleItems.Clear();

            var data = await _win32InfoService.GetListDetailsDataAsync();

            foreach (var item in data)
            {
                SampleItems.Add(item);
            }

            Selected = SampleItems.First();
        }

        public void OnNavigatedFrom()
        {
        }
    }
}
