using System;

namespace mrousavy.Morph {
    /// <author>Marc Rousavy</author>
    /// <summary>
    /// <see cref="ColumnNameAttribute"/> for parsing from a Database to a .NET Object
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ColumnNameAttribute : Attribute {
        public string Name { get; }

        public ColumnNameAttribute([System.Runtime.CompilerServices.CallerMemberName] string name = null) {
            Name = name;
        }
    }
}
