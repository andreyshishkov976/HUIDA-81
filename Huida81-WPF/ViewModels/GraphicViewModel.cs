using System;
using System.Collections.ObjectModel;
using System.Linq;

using GalaSoft.MvvmLight;

using Huida81_WPF.Contracts.ViewModels;
using Huida81_WPF.Core.Contracts.Services;
using Huida81_WPF.Core.Models;

namespace Huida81_WPF.ViewModels
{
    public class GraphicViewModel : ViewModelBase, INavigationAware
    {
        private readonly ISampleDataService _sampleDataService;
        private SampleOrder _selected;

        public SampleOrder Selected
        {
            get { return _selected; }
            set { Set(ref _selected, value); }
        }

        public ObservableCollection<SampleOrder> SampleItems { get; private set; } = new ObservableCollection<SampleOrder>();

        public GraphicViewModel(ISampleDataService sampleDataService)
        {
            _sampleDataService = sampleDataService;
        }

        public async void OnNavigatedTo(object parameter)
        {
            SampleItems.Clear();

            var data = await _sampleDataService.GetListDetailsDataAsync();

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
