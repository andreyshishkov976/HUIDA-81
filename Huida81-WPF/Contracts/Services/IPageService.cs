﻿using System;
using System.Windows.Controls;

using GalaSoft.MvvmLight;

namespace Huida81_WPF.Contracts.Services
{
    public interface IPageService
    {
        Type GetPageType(string key);

        Page GetPage(string key);

        void Configure<VM, V>()
            where VM : ViewModelBase
            where V : Page;
    }
}
