using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Windows;

namespace EffixReportSystem.Helper.Classes
{
    public static class DataHelper
    {
        private static List<UnmanagedMemoryStream> GetResourceNames(System.Globalization.CultureInfo culture)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Stream s = asm.GetManifestResourceStream("StringDictionary");
            string resourceName = asm.GetName().Name + ".g";
            ResourceManager rm = new ResourceManager(resourceName, asm);
            ResourceSet resourceSet = rm.GetResourceSet(culture, true, true);
            List<string> resources = new List<string>();
            List<UnmanagedMemoryStream> resources2 = new List<UnmanagedMemoryStream>();
            foreach (DictionaryEntry resource in resourceSet)
            {
                if (((string) resource.Key).Contains("stringdictionary"))
                {
                    resources2.Add((UnmanagedMemoryStream) resource.Value);
                }
                resources.Add((string) resource.Key);
            }
            rm.ReleaseAllResources();
            return resources2;
        }

        public static string GetStringFromDictionary(string dictionaryName, string key)
        {
            try
            {
                var resource =
                    Application.Current.Resources.MergedDictionaries.First(
                        item => item.Source.ToString().ToLower().Contains(dictionaryName.ToLower()));
                return resource[key] as string;
            }
            catch (Exception)
            {
                return "";
            }

        }
    }


    public class PublicationHelper
    {
        private static PublicationHelper _instance;
        public ObservableCollection<EF_SMI> Smi;
        public  ObservableCollection<EF_Tonality> Tonalities;
        public ObservableCollection<EF_SMI_Type> SmiTypes;
        public ObservableCollection<EF_Exclusivity> Exclusivities;
        public ObservableCollection<EF_SMI_priority> Priorities;
        public ObservableCollection<EF_Dictionary> Initiated;
        public ObservableCollection<EF_Dictionary> Planed;
        public ObservableCollection<EF_Dictionary> Photo;
        private PublicationHelper()
        {
            using(var model= new EntitiesModel())
            {
                Smi= new ObservableCollection<EF_SMI>(model.EF_SMIs);
               Tonalities=new ObservableCollection<EF_Tonality>(model.EF_Tonalities);
                SmiTypes= new ObservableCollection<EF_SMI_Type>(model.EF_SMI_Types);
                Exclusivities=new ObservableCollection<EF_Exclusivity>(model.EF_Exclusivities);
                Priorities=new ObservableCollection<EF_SMI_priority>(model.EF_SMI_priorities);
                Initiated = new ObservableCollection<EF_Dictionary>(model.EF_Dictionaries.Where(item => item.Type_name == "initiated"));
                Planed =
                    new ObservableCollection<EF_Dictionary>(
                        model.EF_Dictionaries.Where(item => item.Type_name == "planed"));
                Photo = new ObservableCollection<EF_Dictionary>(model.EF_Dictionaries.Where(item => item.Type_name == "photo"));
            }
            
        }

        public static PublicationHelper Instance
        {
            get { return _instance ?? (_instance = new PublicationHelper()); }
        }
    }
    //public static class PublicationHelper
    //{
    //    private ObservableCollection<EF_Tonality> _tonalities=new ObservableCollection<EF_Tonality>();
    //    public static EF_Tonality GetTonalityByID(long id)
    //    {
            
    //    }
    //}
}
