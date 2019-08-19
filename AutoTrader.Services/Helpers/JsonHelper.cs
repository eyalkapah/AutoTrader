using AutoTrader.Models.Utils;
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
        //public static void DeserializeJson(string path)
        //{
        //    try
        //    {
        //        using (var sr = File.OpenText(path))
        //        {
        //            var json = await sr.ReadToEndAsync();

        //            return JsonConvert.DeserializeObject<T>(json);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public static T DeserializeJson<T>(string path)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    using (var r = new StreamReader(fs))
                    {
                        var json = r.ReadToEnd();

                        return JsonConvert.DeserializeObject<T>(json);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SerializeJson(object obj, string path)
        {
            try
            {
                var json = JsonConvert.SerializeObject(obj, Formatting.Indented);

                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    using (var w = new StreamWriter(fs))
                    {
                        w.Write(json);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}