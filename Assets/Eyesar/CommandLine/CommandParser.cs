// ************************
// Author: Sean Cheung
// Create: 2016/06/29/11:20
// Modified: 2016/06/29/11:58
// ************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;

namespace Eyesar.CommandLine
{
    public static class CommandParser
    {
        private static readonly Dictionary<string, string> map = new Dictionary<string, string>();

        /// <summary>
        /// Arguments count
        /// </summary>
        public static readonly int Count;

        /// <summary>
        /// Host/Caller path
        /// </summary>
        /// <remarks>
        /// If there is no host/caller, the value is <see cref="string.Empty"/>
        /// </remarks>
        public static readonly string Executable;

        static CommandParser()
        {
            var args = Environment.GetCommandLineArgs();
            Executable = args[0];
            if (args.Length <= 1)
                return;
            for (var i = 1; i < args.Length; i += 2) //skip the first parameter since it's entry name
            {
                var name = args[i];
                if (name.Length <= 2 || !name.StartsWith("-"))
                    throw new ArgumentException();
                name = name.Substring(1);
                if (i >= args.Length - 1)
                    throw new ArgumentOutOfRangeException();
                var value = args[i + 1];
                map.Add(name, value);
            }
            Count = map.Count;
        }

        /// <summary>
        /// Get value and convert it to the specified type
        /// </summary>
        /// <typeparam name="T">Argument value type</typeparam>
        /// <param name="name">Argument name</param>
        /// <param name="throw">Throw on not found</param>
        /// <returns></returns>
        /// <remarks>
        /// If thow is true and argument finding or converting failed, an exception will be thrown, otherwise the default value of <see cref="T"/> is returned
        /// </remarks>
        public static T GetValue<T>([NotNull] string name, bool @throw = true) where T : IConvertible
        {
            string value;
            if (!map.TryGetValue(name, out value))
            {
                if(@throw)
                    throw new KeyNotFoundException();
                return default(T);
            }
            var converter = TypeDescriptor.GetConverter(typeof (T));
            try
            {
                return (T)converter.ConvertFromString(value);
            }
            catch
            {
                if(@throw)
                    throw;
                return default(T);
            }
        }

        [CanBeNull]
        public static string GetString([NotNull] string name, bool @throw = true)
        {
            string value;
            if (!map.TryGetValue(name, out value))
            {
                if (@throw)
                    throw new KeyNotFoundException();
                return null;
            }
            return value;
        }

        public static int GetInt([NotNull] string name, bool @throw = true)
        {
            return GetValue<int>(name, @throw);
        }

        public static bool GetBool([NotNull] string name, bool @throw = true)
        {
            return GetValue<bool>(name, @throw);
        }

        public static float GetFloat([NotNull] string name, bool @throw = true)
        {
            return GetValue<float>(name, @throw);
        }

        public static long GetLong([NotNull] string name, bool @throw = true)
        {
            return GetValue<long>(name, @throw);
        }

        public static DateTime GetDateTime([NotNull] string name, bool @throw = true)
        {
            return GetValue<DateTime>(name, @throw);
        }
    }
}