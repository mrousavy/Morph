using System;
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

        public static async Task<T> ParseAsync<T>(DbDataReader reader) {
            T result = default(T);
            Type type = typeof(T);

            //read all returned rows
            while (await reader.ReadAsync()) {
                //fill attributes
                var properties = type.GetRuntimeProperties();
                var attributes = properties.Select(property => property.Attributes);
                try {
                    
                } catch {
                    //value could not be parsed
                }
            }
            return result;
        }

        public static T Parse<T>(IDataReader reader) {
            T result = default(T);
            while (reader.Read()) {
                //fill attributes
                Type type = typeof(T);
            }
            return result;
        }
    }
}
