using InMemoryDatabase.Components;
using InMemoryDatabase.Enums;
using System.Collections.Generic;

namespace InMemotyDatabase.Test
{
    public static class DatabaseMock
    {
        public static Database CreateDatabase()
        {
            var database = new Database("Test");
            database.AddTable("MockTable");
            var table = database.GetTable("MockTable");

            table.AddColumn("MockColumn1", EnumType.String);
            table.AddColumn("MockColumn2", EnumType.Integer);
            table.AddColumn("MockColumn3", EnumType.Real);

            IList<Cell> row = new List<Cell>();
            var mockCell1 = new Cell(EnumType.String, "string1");
            row.Add(mockCell1);

            var mockCell2 = new Cell(EnumType.Integer, "1");
            row.Add(mockCell2);

            var mockCell3 = new Cell(EnumType.Real, "1.1");
            row.Add(mockCell3);

            table.AddRow(row);

            row = new List<Cell>();
            mockCell1 = new Cell(EnumType.String, "string2");
            row.Add(mockCell1);

            mockCell2 = new Cell(EnumType.Integer, "2");
            row.Add(mockCell2);

            mockCell3 = new Cell(EnumType.Real, "2.2");
            row.Add(mockCell3);

            table.AddRow(row);

            row = new List<Cell>();
            mockCell1 = new Cell(EnumType.String, "string33");
            row.Add(mockCell1);

            mockCell2 = new Cell(EnumType.Integer, "3");
            row.Add(mockCell2);

            mockCell3 = new Cell(EnumType.Real, "12.45");
            row.Add(mockCell3);

            table.AddRow(row);

            row = new List<Cell>();
            mockCell1 = new Cell(EnumType.String, "string333");
            row.Add(mockCell1);

            mockCell2 = new Cell(EnumType.Integer, "3");
            row.Add(mockCell2);

            mockCell3 = new Cell(EnumType.Real, "100");
            row.Add(mockCell3);

            table.AddRow(row);

            return database;
        }
    }
}