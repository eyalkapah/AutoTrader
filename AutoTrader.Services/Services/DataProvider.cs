using AutoTrader.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Services.Services
{
    public static class DataProvider
    {
        public async static Task<SettingsContract> GetSettings()
        {
            try
            {
                //// Read RecipeData.json from this PCL's DataModel folder

                var name = typeof(DataProvider).AssemblyQualifiedName.Split(',')[1].Trim();

                var assembly = Assembly.Load(new AssemblyName(name));

                var stream = assembly.GetManifestResourceStream($"{name}.Data.settings.json");

                using (var reader = new StreamReader(stream))

                {
                    string json = await reader.ReadToEndAsync();

                    return JsonConvert.DeserializeObject<SettingsContract>(json);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}