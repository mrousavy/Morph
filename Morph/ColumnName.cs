using System;

namespace mrousavy.Morph {
    /// <author>Marc Rousavy</author>
    /// <summary>
    /// <see cref="ColumnName"/> <see cref="Attribute"/> for parsing from a Database to a .NET Object
    /// </summary>
    public class ColumnName : Attribute {
        public string Name { get; }

        public ColumnName(string name) {
            Name = name;
        }
    }
}
