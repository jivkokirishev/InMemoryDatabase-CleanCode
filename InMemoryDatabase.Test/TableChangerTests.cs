using InMemoryDatabase.Components;
using InMemoryDatabase.Enums;
using InMemoryDatabase.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace InMemotyDatabase.Test
{
    [TestClass]
    public class TableChangerTests
    {
        [TestMethod]
        public void CheckIfColumnIsAddedProperly()
        {
            var database = DatabaseMock.CreateDatabase();
            var tableChanger = new DbTableChanger(database);

            int currentColumns = database.GetTable("MockTable").ColumnCount();

            tableChanger.AddColumn("MockTable", "ProperlyAdded", EnumType.Integer);

            int actual = database.GetTable("MockTable").ColumnCount();
            int expected = currentColumns + 1;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void CheckIfNewColumnValuesAreNull()
        {
            var database = DatabaseMock.CreateDatabase();
            var tableChanger = new DbTableChanger(database);

            tableChanger.AddColumn("MockTable", "Name", EnumType.String);

            bool actual = true;

            var cells = database.GetTable("MockTable").GetColumn("Name").Cells;
            foreach (var cell in cells)
            {
                if (!cell.IsNull)
                {
                    actual = false;
                    break;
                }
            }

            Assert.IsTrue(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WhenAddingColumnNameShouldBeUnique()
        {
            var database = DatabaseMock.CreateDatabase();
            var tableChanger = new DbTableChanger(database);

            tableChanger.AddColumn("MockTable", "Id", EnumType.Integer);
            tableChanger.AddColumn("MockTable", "Id", EnumType.Real);
        }

        [TestMethod]
        public void CheckIfUpdateChangesSelectedCells()
        {
            var database = DatabaseMock.CreateDatabase();
            var tableChanger = new DbTableChanger(database);

            var searchCell = new Cell(EnumType.Integer, "2");
            var targetCell = new Cell(EnumType.String, "updated Value");
            tableChanger.Update("MockTable", "MockColumn2", searchCell, "MockColumn1", targetCell);

            Cell actual = database.GetTable("MockTable").GetColumn("MockColumn1").GetElementAt(1);

            Assert.IsTrue(actual == targetCell);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdatingNonExistingColumnThrowsAnException()
        {
            var database = DatabaseMock.CreateDatabase();
            var tableChanger = new DbTableChanger(database);

            var searchCell = new Cell(EnumType.Integer, "2");
            var targetCell = new Cell(EnumType.String, "updated Value");
            tableChanger.Update("MockTable", "MockColumn2", searchCell, "MockColumn5", targetCell);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdatingColumnWithDifferentCellTypeThrowsAnException()
        {
            var database = DatabaseMock.CreateDatabase();
            var tableChanger = new DbTableChanger(database);

            var searchCell = new Cell(EnumType.Integer, "2");
            var targetCell = new Cell(EnumType.Integer, "2134");
            tableChanger.Update("MockTable", "MockColumn2", searchCell, "MockColumn1", targetCell);
        }

        [TestMethod]
        public void CheckIfDeleteRemovesSelectedColumns()
        {
            var database = DatabaseMock.CreateDatabase();
            var tableChanger = new DbTableChanger(database);

            int currentRows = database.GetTable("MockTable").GetColumn("MockColumn1").ElementCount();

            var searchCell = new Cell(EnumType.Integer, "2");
            tableChanger.Delete("MockTable", "MockColumn2", searchCell);

            int actual = database.GetTable("MockTable").GetColumn("MockColumn1").ElementCount();

            Assert.AreEqual(currentRows - 1, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteThrowsAnExceptionIfThereIsNotSuchTable()
        {
            var database = DatabaseMock.CreateDatabase();
            var tableChanger = new DbTableChanger(database);

            var searchCell = new Cell(EnumType.Integer, "2");
            tableChanger.Delete("FakeTable", "MockColumn2", searchCell);
        }

        [TestMethod]
        public void DeleteShouldNotRemoveAnythingIfDoesNotFindSearchValue()
        {
            var database = DatabaseMock.CreateDatabase();
            var tableChanger = new DbTableChanger(database);

            int currentRows = database.GetTable("MockTable").GetColumn("MockColumn1").ElementCount();

            var searchCell = new Cell(EnumType.Integer, "8074");
            tableChanger.Delete("MockTable", "MockColumn2", searchCell);

            int actual = database.GetTable("MockTable").GetColumn("MockColumn1").ElementCount();

            Assert.AreEqual(currentRows, actual);
        }

        [TestMethod]
        public void CheckIfInsertAddsNewRow()
        {
            var database = DatabaseMock.CreateDatabase();
            var tableChanger = new DbTableChanger(database);

            int currentRows = database.GetTable("MockTable").GetColumn("MockColumn1").ElementCount();

            IList<Cell> row = new List<Cell>();
            var testCell1 = new Cell(EnumType.String, "test string");
            row.Add(testCell1);

            var testCell2 = new Cell(EnumType.Integer, "18");
            row.Add(testCell2);

            var testCell3 = new Cell(EnumType.Real, "18.18");
            row.Add(testCell3);

            tableChanger.Insert("MockTable", row);

            int actual = database.GetTable("MockTable").GetColumn("MockColumn1").ElementCount();

            Assert.AreEqual(currentRows + 1, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InsertThrowsExceptionWhenAddingMoreElementsInARow()
        {
            var database = DatabaseMock.CreateDatabase();
            var tableChanger = new DbTableChanger(database);

            IList<Cell> row = new List<Cell>();
            var testCell1 = new Cell(EnumType.String, "test string");
            row.Add(testCell1);

            var testCell2 = new Cell(EnumType.Integer, "18");
            row.Add(testCell2);

            var testCell3 = new Cell(EnumType.Real, "18.18");
            row.Add(testCell3);

            var testCell4 = new Cell(EnumType.Integer, "6834");
            row.Add(testCell4);

            tableChanger.Insert("MockTable", row);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InsertThrowsExceptionWhenAddingLessElementsInARow()
        {
            var database = DatabaseMock.CreateDatabase();
            var tableChanger = new DbTableChanger(database);

            IList<Cell> row = new List<Cell>();
            var testCell1 = new Cell(EnumType.String, "test string");
            row.Add(testCell1);

            var testCell2 = new Cell(EnumType.Integer, "18");
            row.Add(testCell2);

            tableChanger.Insert("MockTable", row);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertThrowsExceptionWhenAddingRowsWhichElementsHaveDifferentTypesFromTheColumns()
        {
            var database = DatabaseMock.CreateDatabase();
            var tableChanger = new DbTableChanger(database);

            IList<Cell> row = new List<Cell>();
            var testCell1 = new Cell(EnumType.String, "test string");
            row.Add(testCell1);

            var testCell2 = new Cell(EnumType.Real, "32.32");
            row.Add(testCell2);

            var testCell3 = new Cell(EnumType.Real, "18.18");
            row.Add(testCell3);

            tableChanger.Insert("MockTable", row);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RenameColumnThrowsAnExceptionIfTheNewNameIsNotUnique()
        {
            var database = DatabaseMock.CreateDatabase();
            var tableChanger = new DbTableChanger(database);

            tableChanger.RenameColumn("MockTable", "MockColumn1", "MockColumn3");
        }

        [TestMethod]
        public void CheckIfRenameColumnChangesColumnName()
        {
            var database = DatabaseMock.CreateDatabase();
            var tableChanger = new DbTableChanger(database);

            tableChanger.RenameColumn("MockTable", "MockColumn1", "TestColumn");

            var table = database.GetTable("MockTable");
            bool doesOldColumnExist = table.ColumnExist("MockColumn1");
            bool doesNewColumnExist = table.ColumnExist("TestColumn");

            bool actual = !doesOldColumnExist && doesNewColumnExist;
            Assert.IsTrue(actual);
        }
    }
}
