using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace mrousavy.Morph {
    public static class DbReaderExtensions {

        public static async Task<T> Parse<T>(this IDataReader @this) {
            if(@this == null) throw new ArgumentNullException(nameof(@this)); //check parameter

            //if no DbDataReader object -> throw
            if (!(@this is DbDataReader reader)) throw new ArgumentException(nameof(@this));

            return await Parser.ParseAsync<T>(reader);
        }
    }
}
