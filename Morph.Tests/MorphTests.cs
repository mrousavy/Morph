using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mrousavy.Morph;

namespace Morph.Tests {
    /// <author>Marc Rousavy</author>
    /// <summary>
    /// Unit Test Class for testing Morph functions
    /// </summary>
    [TestClass]
    public class MorphTests {
        private SqlConnection Connection { get; set; }
        private IDataReader Reader { get; set; }

        private const string SqlServer = "VIE-VMRO-Win10";
        private const string SqlDatabase = "SPI_ADMIN";
        private const string SqlUser = "SPI_DBAMN";
        private const string SqlPass = "SPI_DBA";

        [TestInitialize]
        public void Initialize() {
            Connection = new SqlConnection($"Server={SqlServer};Database={SqlDatabase};User ID={SqlUser};Password={SqlPass}"); //MSSQL
            Connection.Open();

            //Fire command
            using (IDbCommand command = new SqlCommand("SELECT * FROM Person;", Connection)) {
                Reader = command.ExecuteReader();
            }
        }

        [TestCleanup]
        public void Cleanup() {
            Connection?.Dispose();
            Reader?.Dispose();
        }

        [TestMethod]
        public void FullTest() {
            //Fire command
            using (IDbCommand command = new SqlCommand("SELECT * FROM Person;", Connection)) {
                //execute reader
                using (IDataReader reader = command.ExecuteReader()) {
                    Person person = reader.Parse<Person>(); //parse to person
                    Assert.IsNotNull(person);
                }
            }
        }

        [TestMethod]
        public void ParseTest() {
            Person person = Reader.Parse<Person>(); //parse to person
            Assert.IsNotNull(person);
        }
    }
}
