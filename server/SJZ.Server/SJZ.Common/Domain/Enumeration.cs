using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace SJZ.Common.Domain
{
    public abstract class Enumeration : IComparable
    {
        public string Name { get; private set; }

        public int Code { get; private set; }

        public string DisplayName { get; private set; }

        protected Enumeration(int code, string name)
        {
            Code = code;
            Name = name;
            DisplayName = name;
        }

        protected Enumeration(int code, string name, string displayName)
            : this(code, name)
        {
            DisplayName = displayName;
        }

        /// <summary>
        /// For testing purpose
        /// </summary>
        public Enumeration() { }

        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration;

            if (otherValue == null)
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Code.Equals(otherValue.Code);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => Code.GetHashCode();

        public static T FromValue<T>(int? value) where T : Enumeration
        {
            if (value == null)
            {
                return default(T);
            }

            var matchingItem = Parse<T>(item => item.Code.Equals(value));
            return matchingItem;
        }

        public static T FromValue<T>(object value) where T : Enumeration
        {
            try
            {
                var intValue = value == null ? (int?)null : Convert.ToInt32(value);
                return FromValue<T>(intValue);
            }
            catch
            {
                return default(T);
            }
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumeration
        {
            var newDisplayName = Regex.Replace(displayName, @"[^0-9a-zA-Z]+", "");
            if (newDisplayName == displayName)
            {
                return Parse<T>(item => item.DisplayName == newDisplayName);
            }
            else
            {
                return Parse<T>(item => Regex.Replace(item.DisplayName, @"[^0-9a-zA-Z]+", "") == newDisplayName);
            }
        }

        public static TTo ParseCodeToEnum<TTo>(Enumeration e)
        {
            if (e == null)
            {
                return default(TTo);
            }

            var t = typeof(TTo);
            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                t = Nullable.GetUnderlyingType(t);
            }

            if (!t.IsEnum)
            {
                // C# 7.0 support where clause to Enum, no need to verify here after upgrading
                throw new ArgumentException("TTo must be an enumerated type");
            }

            return (TTo)Enum.ToObject(t, e.Code);
        }

        private static T Parse<T>(Func<T, bool> predicate) where T : Enumeration
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
            {
                return default(T);
            }

            return matchingItem;
        }

        public int CompareTo(object other) => Code.CompareTo(((Enumeration)other).Code);

        public static bool operator ==(Enumeration left, Enumeration right)
        {
            if (object.Equals(left, null))
            {
                return object.Equals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator !=(Enumeration left, Enumeration right)
        {
            return !(left == right);
        }
    }
}
