using System;
using System.Collections;
using System.Collections.Generic;
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
}
