using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ICompAccounting
{
    public static class Extensions
    {
        public static void Copy<T>(this T dist, T source)
        {
            foreach (PropertyInfo SourceProperty in source.GetType().GetProperties( BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                Type type = dist.GetType();

                var DistField = type.GetProperty(SourceProperty.Name);
                DistField.SetValue(dist, SourceProperty.GetValue(source));
            }
        }

    }
}
