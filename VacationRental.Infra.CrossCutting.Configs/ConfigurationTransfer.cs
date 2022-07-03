using Microsoft.Extensions.Configuration;
using System;

namespace VacationRental.Infra.CrossCutting.Configs
{
    public static class ConfigurationTransfer
    {
        public static T GetObject<T>(IConfiguration configuration, string customObjectPath = null)
        {
            Type type = typeof(T);
            string sectionName = string.IsNullOrEmpty(customObjectPath) ? type.Name : customObjectPath;

            var objectData = configuration.GetSection(sectionName).Get<T>();
            return objectData;
        }
    }
}
