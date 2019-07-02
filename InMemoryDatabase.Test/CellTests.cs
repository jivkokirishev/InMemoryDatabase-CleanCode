using InMemoryDatabase.Components;
using InMemoryDatabase.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace InMemotyDatabase.Test
{
    [TestClass]
    public class CellTests
    {
        [TestMethod]
        public void GetNullValueTest()
        {
            //Arrange
            Cell cell = new Cell(EnumType.Integer);

            //Act  
            string actual = cell.Value;

            //Assert  
            string expected = "NULL";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetValueTest()
        {
            //Arrange
            Cell cell = new Cell(EnumType.Integer, "123");

            //Act  
            string actual = cell.Value;

            //Assert  
            string expected = "123";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingValueThaCanNotBeParsedToTheCellTypeShouldThrowException()
        {
            Cell cell = new Cell(EnumType.Integer, "123.506");
        }

        [TestMethod]
        public void CheckingValueOfAClonedObject()
        {
            Cell cell = new Cell(EnumType.Real, "123.506");
            Cell clonedCell = cell.Clone();

            clonedCell.Value = "43.345";

            string actual = cell.Value;
            string expected = "123.506";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckingIsNullOfAClonedObject()
        {
            Cell cell = new Cell(EnumType.Real);
            Cell clonedCell = cell.Clone();

            clonedCell.Value = "43.345";

            string actual = cell.Value;
            string expected = "NULL";
            Assert.AreEqual(expected, actual);
        }
    }
}
