using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EffixReportSystem.Views.Publication.ViewModels
{
    class AddProjectViewModel:IDataErrorInfo
    {
        public string ProjectName { get; set; }
        public string ProjectDescr { get; set; }

        public bool HasError { get; set; }
        public AddProjectViewModel()
        {
            ProjectName = String.Empty;
            ProjectDescr = String.Empty;
            HasError = true;
        }
        public string Error
    {
        get { throw new NotImplementedException(); }
    }

    public string this[string columnName]
    {
        get 
        {
            string result = null;
            using (var model = new EntitiesModel())
            {
                if (columnName == "ProjectName")
                {
                    if (string.IsNullOrEmpty(ProjectName))
                        result = "Введите наименование проекта";
                    if(model.EF_Projects.Any(item=>item.Project_name.Trim().ToLower()==ProjectName.Trim().ToLower()))
                        result = "Проект с таким наименованием уже существует";
                }
                if (columnName == "ProjectDescr")
                {
                    if (string.IsNullOrEmpty(ProjectDescr))
                        result = "Введите описание проекта";
                    if (model.EF_Projects.Any(item => item.Project_descr.Trim().ToLower() == ProjectDescr.Trim().ToLower()))
                        result = "Проект с таким описанием уже существует";
                }
            }
            return result;
        }
    }
    }
}
