using System;
using System.Collections.Generic;
using System.Reflection;
using TrainingSuperville.Models.Entities;

namespace TrainingSuperville.Extensions
{
    public static class MyExtensionMethods
    {
        public static string GetDescription(this StorableEntity storableEntity)
        {
            string message = string.Empty;
            Type entityType = storableEntity.GetType();

            IList<PropertyInfo> props = new List<PropertyInfo>(entityType.GetProperties());

            foreach (PropertyInfo prop in props)
            {
                object propValue = prop.GetValue(storableEntity, null);

                if (prop != null && prop.Name != "Id" && prop.Name != "Description")
                {
                    message += string.Concat(prop.Name, " : ", (propValue ?? String.Empty).ToString(), " | ");
                }
            }

            message = message.Substring(0, message.Length - 2);

            return message;
        }
    }
}
