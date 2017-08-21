using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace mrousavy.Morph {
    /// <author>Marc Rousavy</author>
    /// <summary>
    /// Extension class for providing Morph Parse functions for
    /// <see cref="IDataReader"/> and <see cref="DbDataReader"/>
    /// </summary>
    public static class DbReaderExtensions {

        /// <summary>
        /// Asynchronously parses values from this <see cref="IDataReader"/>
        /// to a managed .NET object <see cref="T"/> where Properties of <see cref="T"/> must be public and flagged
        /// with the <see cref="ColumnName"/> attribute.
        /// </summary>
        public static async Task<T> ParseAsync<T>(this IDataReader @this) {
            if(@this == null) throw new ArgumentNullException(nameof(@this)); //check parameter
            
            //if no DbDataReader object -> throw
            if (!(@this is DbDataReader reader)) throw new ArgumentException(nameof(@this));
            
            return await Parser.ParseAsync<T>(reader); //actually parse
        }

        /// <summary>
        /// Parses values from this <see cref="IDataReader"/>
        /// to a managed .NET object <see cref="T"/> where Properties of <see cref="T"/> must be public and flagged
        /// with the <see cref="ColumnName"/> attribute.
        /// </summary>
        public static T Parse<T>(this IDataReader @this) {
            if (@this == null) throw new ArgumentNullException(nameof(@this)); //check parameter

            //if no DbDataReader object -> throw
            if (!(@this is DbDataReader reader)) throw new ArgumentException(nameof(@this));

            return Parser.Parse<T>(reader); //actually parse
        }
    }
}
