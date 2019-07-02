using InMemoryDatabase.Components;
using InMemoryDatabase.Enums;
using InMemoryDatabase.Managers;
using InMemotyDatabase.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InMemoryDatabase.Test
{
    [TestClass]
    public class TransformationsTests
    {
        [TestMethod]
        public void CheckIfAggregateReturnsMaximalCellCorrectly()
        {
            var database = DatabaseMock.CreateDatabase();
            var tableChanger = new DbTransformations(database);

            var searchCell = new Cell(EnumType.Integer, "3");
            Cell actual = tableChanger.Agregate("MockTable", "MockColumn2", searchCell, "MockColumn3", OperationType.Max);

            Cell expectedCell = new Cell(EnumType.Real, "100");

            Assert.IsTrue(expectedCell == actual);
        }

        [TestMethod]
        public void CheckIfAggregateReturnsMinimalCellCorrectly()
        {
            var database = DatabaseMock.CreateDatabase();
            var tableChanger = new DbTransformations(database);

            var searchCell = new Cell(EnumType.Integer, "3");
            Cell actual = tableChanger.Agregate("MockTable", "MockColumn2", searchCell, "MockColumn3", OperationType.Min);

            Cell expectedCell = new Cell(EnumType.Real, "12.45");

            Assert.IsTrue(expectedCell == actual);
        }

        [TestMethod]
        public void CheckIfAggregateReturnsTheCorrectProductOfSelectedCells()
        {
            var database = DatabaseMock.CreateDatabase();
            var tableChanger = new DbTransformations(database);

            var searchCell = new Cell(EnumType.Integer, "3");
            Cell actual = tableChanger.Agregate("MockTable", "MockColumn2", searchCell, "MockColumn3", OperationType.Product);

            Cell expectedCell = new Cell(EnumType.Real, "1245");

            Assert.IsTrue(expectedCell == actual);
        }

        [TestMethod]
        public void CheckIfAggregateReturnsTheCorrectSumOfSelectedCells()
        {
            var database = DatabaseMock.CreateDatabase();
            var tableChanger = new DbTransformations(database);

            var searchCell = new Cell(EnumType.Integer, "3");
            Cell actual = tableChanger.Agregate("MockTable", "MockColumn2", searchCell, "MockColumn3", OperationType.Sum);

            Cell expectedCell = new Cell(EnumType.Real, "112.45");

            Assert.IsTrue(expectedCell == actual);
        }
    }
}
