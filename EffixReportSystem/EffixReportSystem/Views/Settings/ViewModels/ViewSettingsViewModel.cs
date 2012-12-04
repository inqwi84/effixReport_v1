using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EffixReportSystem.Helper.Classes;
using EffixReportSystem.Helper.Interfaces;

namespace EffixReportSystem.Views.Settings.ViewModels
{
    class ViewSettingsViewModel : ObservableObject, IPageViewModel
    {
        public string Name { get; private set; }
    }
}
