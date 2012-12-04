using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EffixReportSystem.Helper.Classes;
using EffixReportSystem.Helper.Interfaces;

namespace EffixReportSystem.Views.Publication.ViewModels
{
    class EditPublicationViewModel:ObservableObject, IPageViewModel
    {
        public string Name { get; private set; }
    }
}
