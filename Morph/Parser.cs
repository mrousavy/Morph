using System;
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
            if(reader == null)
                throw new ArgumentNullException(nameof(reader));
            if (reader.IsClosed)
                throw new ParseException("Cannot parse a closed DataReader!");
            if (!reader.HasRows)
                throw new ParseException("DataReader does not contain any rows!");

            var result = default(T);
            var type = typeof(T);
            IDictionary<string, PropertyInfo> dictionary = AttributeReflector(type);

            //read all returned rows | TODO: while(..) or if(..)?
            while (await reader.ReadAsync()) {
                //fill attributes
                foreach(string key in dictionary.Keys) {
                    try {
                        var value = reader[key];
                        //NULL OR DBNULL? //value = value == DBNull.Value ? null : value;
                        dictionary[key].SetValue(result, value); //fill up the object with values
                    } catch(Exception ex) {
                        //value could not be parsed
                        if (throwOnError)
                            throw new ParseException($"Could not parse reader field with key \"{key}\"!", reader, ex);
                    }
                }
            }
            return result;
        }

        public static T Parse<T>(IDataReader reader, bool throwOnError = false) {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));
            if (reader.IsClosed)
                throw new ParseException("Cannot parse a closed DataReader!");

            var result = default(T);
            var type = typeof(T);
            IDictionary<string, PropertyInfo> dictionary = AttributeReflector(type);

            //read all returned rows | TODO: while(..) or if(..)?
            while (reader.Read()) {
                //fill attributes
                foreach (string key in dictionary.Keys) {
                    try {
                        var value = reader[key];
                        //NULL OR DBNULL? //value = value == DBNull.Value ? null : value;
                        dictionary[key].SetValue(result, value); //fill up the object with values
                    } catch (Exception ex) {
                        //value could not be parsed
                        if (throwOnError)
                            throw new ParseException($"Could not parse reader field with key \"{key}\"!", reader, ex);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Reflect on <see cref="Type"/> Members and build a Colum Name (Key) + <see cref="PropertyInfo"/> dictionary
        /// </summary>
        /// <param name="type">The type to reflect on</param>
        /// <returns>A built <see cref="IDictionary{TKey,TValue}"/> with the Column Name (Key) and the Property as <see cref="PropertyInfo"/></returns>
        private static IDictionary<string, PropertyInfo> AttributeReflector(Type type) {
            IEnumerable<PropertyInfo> properties = type.GetRuntimeProperties();
            IDictionary<string, PropertyInfo> dictionary = new Dictionary<string, PropertyInfo>();

            foreach (var property in properties) {
                //get all attributes (with inherits)
                IEnumerable<Attribute> attributes = property.GetCustomAttributes(true);
                //get column
                var column = attributes.FirstOrDefault(a => a.GetType() == typeof(ColumnNameAttribute)) as ColumnNameAttribute;
                string name = column?.Name ?? property.Name;

                if(name != null) //only add members with that attribute
                    dictionary.Add(name, property);
            }
            return dictionary;
        }
    }



    public class ParseException : Exception {
        public IDataReader Reader { get; set; }

        public ParseException(string message, IDataReader reader = null, Exception inner = null) : base(message, inner) {
            Reader = reader;
        }
    }
}
