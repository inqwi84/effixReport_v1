using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace EffixReportSystem.Helper.Classes.ValidationRules
{
   public  class ProjectNameValidationRule: ValidationRule
    {
           public override ValidationResult Validate(object value, CultureInfo cultureInfo)
            {
                try
                {
                    var strValue = String.Empty;
                    if (value != null)
                    {
                        strValue= ((string) value);
                    }
                    //если есть проект с таким именем
                    if (strValue.Length < 0)
                        return new ValidationResult(false, "Наименование проекта не может быть пустым");
                    using (var model=new EntitiesModel())
                    {
                        return model.EF_Projects.Any(proj => proj.Project_name.ToLower() == strValue.ToLower())
                                   ? new ValidationResult(false, "Проект с таким наименованием уже существует")
                                   : new ValidationResult(true, null);
                    }
                }
                catch (Exception e)
                {
                    return new ValidationResult(false, e.Message);
                }
            }
        }
   public  class ProjectDescrValidationRule : ValidationRule
   {
       public override ValidationResult Validate(object value, CultureInfo cultureInfo)
       {
           try
           {
               var strValue = String.Empty;
               if (value != null)
               {
                   strValue = ((string)value);
               }
               //если есть проект с таким именем
               if (strValue.Length < 0)
                   return new ValidationResult(false, "Описание проекта не может быть пустым");
               using (var model = new EntitiesModel())
               {
                   return model.EF_Projects.Any(proj => proj.Project_descr.ToLower() == strValue.ToLower())
                              ? new ValidationResult(false, "Проект с таким описанием уже существует")
                              : new ValidationResult(true, null);
               }
           }
           catch (Exception e)
           {
               return new ValidationResult(false, e.Message);
           }
       }
   }
    }
