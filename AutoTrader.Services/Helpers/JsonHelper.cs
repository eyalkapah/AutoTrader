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
    public static class JsonHelper
    {
        public async static Task<T> DeserializeAsync<T>(string path)
        {
            try
            {
                using (var sr = File.OpenText(path))
                {
                    var json = await sr.ReadToEndAsync();

                    return JsonConvert.DeserializeObject<T>(json);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async static Task SerializeAsync(object obj, string path)
        {
            try
            {
                var json = JsonConvert.SerializeObject(obj, Formatting.Indented);

                if (!File.Exists(path))
                {
                    File.Create(path);
                }

                using (var writer = File.CreateText(path))
                {
                    await writer.WriteAsync(json);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}