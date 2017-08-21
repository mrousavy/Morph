using System.Data;
using mrousavy.Morph;

namespace Morph.Tests {
    /// <author>Marc Rousavy</author>
    /// <summary>
    /// Test Person object representing a Person in the Database
    /// </summary>
    public class Person {
        /// <summary>
        /// This <see cref="Person"/>'s first name
        /// </summary>
        [ColumnName("FIRST_NAME")]
        public string FirstName { get; set; }
        /// <summary>
        /// This <see cref="Person"/>'s last name
        /// </summary>
        [ColumnName("LAST_NAME")]
        public string LastName { get; set; }
        /// <summary>
        /// This <see cref="Person"/>'s address
        /// </summary>
        [ColumnName("ADDRESS")]
        public string Address { get; set; }
    }
}
