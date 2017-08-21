using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace mrousavy.Morph {
    internal class Parser {

        internal static async Task<T> ParseAsync<T>(DbDataReader reader) {
            T result = default(T);
            while (await reader.ReadAsync()) {
                //fill attributes
                Type type = typeof(T);
            }
            return result;
        }

        internal static T Parse<T>(IDataReader reader) {
            T result = default(T);
            while (reader.Read()) {
                //fill attributes
                Type type = typeof(T);
            }
            return result;
        }
    }
}
