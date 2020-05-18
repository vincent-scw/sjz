using System;
using System.Collections.Generic;
using System.Text;

namespace SJZ.Common.Extensions
{
    public static class ObjectExtension
    {
        public static T ConvertTo<T>(this object obj)
            where T : class
        {
            var properties = obj.GetType().GetProperties();
            var t = typeof(T);

            var instance = Activator.CreateInstance(t, true) as T;
            foreach (var prop in properties)
            {
                var tp = t.GetProperty(prop.Name);
                if (tp != null)
                {
                    tp.SetValue(instance, prop.GetValue(obj));
                }
            }

            return instance;
        }
    }
}
