using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using mrousavy.Morph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Morph.Tests {
    /// <author>Marc Rousavy</author>
    /// <summary>
    /// Unit Test Class for testing Morph functions (DEBU)G
    /// </summary>
    [TestClass]
    public class MorphDebugTests {

        [TestMethod]
        public void FullTest() {
            var person = Parser.ParseAsync<Person>(null);
        }
    }
}
