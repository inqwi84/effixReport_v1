using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EffixReportSystem.Helper.Classes.Enums
{
    public enum ViewMode
    {
        EditMode,
        NewMode,
        AddMode,
        AddTemplated,
        ViewOnlyMode,
        AddToList,
        AddToTree,
        MakeSnapshot,
        ImportSnapshot
    };

    public enum SortingMode
    {
        NameMode,
        Position,
        Custom,
        IncidentType,
        SubjectType
    };

}