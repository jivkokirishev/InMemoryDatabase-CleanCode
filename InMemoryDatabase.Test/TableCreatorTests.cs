using InMemoryDatabase.Components;
using InMemoryDatabase.Enums;
using InMemoryDatabase.Managers;
using InMemotyDatabase.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InMemoryDatabase.Test
{
    [TestClass]
    public class TableCreatorTests
    {
        [TestMethod]
        public void CheckIfWhereGetsOnlyTheRowsThatMeetTheConditions()
        {
            var database = DatabaseMock.CreateDatabase();
            var tableCreator = new DbTableCreator(database);

            var searchCell = new Cell(EnumType.Integer, "3");
            Table filteredTable = tableCreator.Where("MockTable", "MockColumn2", searchCell);

            int actualRowsCount = filteredTable.GetColumn("MockColumn1").ElementCount();
            int expectedRowsCount = 2;

            Assert.AreEqual(actualRowsCount, expectedRowsCount);
        }
    }
}
