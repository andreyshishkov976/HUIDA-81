using System;
using System.Collections.ObjectModel;

using GalaSoft.MvvmLight;

using Huida81_WPF.Contracts.ViewModels;
using Huida81_WPF.Core.Contracts.Services;
using Huida81_WPF.Core.Models;

namespace Huida81_WPF.ViewModels
{
    public class ActivityViewModel : ViewModelBase, INavigationAware
    {
        private readonly ISampleDataService _sampleDataService;

        public ObservableCollection<SampleOrder> Source { get; } = new ObservableCollection<SampleOrder>();

        public ActivityViewModel(ISampleDataService sampleDataService)
        {
            _sampleDataService = sampleDataService;
        }

        public async void OnNavigatedTo(object parameter)
        {
            Source.Clear();

            // Replace this with your actual data
            var data = await _sampleDataService.GetGridDataAsync();

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
