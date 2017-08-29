using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace mrousavy.Morph {
    /// <author>Marc Rousavy</author>
    /// <summary>
    /// Internal static class for parsing Values from 
    /// a Data Reader (<see cref="IDataReader"/>, <see cref="DbDataReader"/>)
    /// to managed .NET objects via reflecting on <see cref="ColumnNameAttribute"/> attributes.
    /// </summary>
    public static class Parser {

        public static async Task<T> ParseAsync<T>(DbDataReader reader, bool throwOnError = false) {
            var result = default(T);
            var type = typeof(T);
            var dictionary = AttributeReflector(type);

            //read all returned rows
            while (await reader.ReadAsync()) {
                //fill attributes
                for (int i = 0; i < reader.FieldCount; i++) {
                    try {
                        
                    } catch(Exception ex) {
                        //value could not be parsed
                        if (throwOnError)
                            throw new ParseException($"Could not parse reader field {i}!", reader, ex);
                    }
                }
            }
            return result;
        }

        public static T Parse<T>(IDataReader reader, bool throwOnError = false) {
            T result = default(T);
            while (reader.Read()) {
                //fill attributes
                Type type = typeof(T);
            }
            return result;
        }

        /// <summary>
        /// Reflect on <see cref="Type"/> Members and build a Property-Name + <see cref="ColumnNameAttribute"/> dictionary
        /// </summary>
        /// <param name="type">The type to reflect on</param>
        /// <returns>A built <see cref="IDictionary{TKey,TValue}"/> with the Properties + <see cref="ColumnNameAttribute"/></returns>
        private static IDictionary<string, string> AttributeReflector(Type type) {
            IEnumerable<PropertyInfo> properties = type.GetRuntimeProperties();
            IDictionary<string, string> dictionary = new Dictionary<string, string>();

            foreach (var property in properties) {
                IEnumerable<Attribute> attributes = property.GetCustomAttributes(true);
                var column = attributes.FirstOrDefault(a => a.GetType() == typeof(ColumnNameAttribute)) as ColumnNameAttribute;
                string name = column?.Name ?? property.Name;

                dictionary.Add(property.Name, name);
            }
            return dictionary;
        }
    }



    public class ParseException : Exception {
        public DbDataReader Reader { get; set; }

        public ParseException(string message, DbDataReader reader, Exception inner) : base(message, inner) {
            Reader = reader;
        }
    }
}
