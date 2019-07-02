using InMemoryDatabase.Components;
using InMemotyDatabase.Utilities;
using System;
using System.Collections.Generic;

namespace InMemoryDatabase.Managers
{
    public class DbTransformations
    {
        private Database database;

        public DbTransformations(Database database)
        {
            this.database = database;
        }


        public IList<string> TableNames()
        {
            var names = new List<string>();

            foreach (var table in database.Tables)
            {
                names.Add(table.Name);
            }

            return names;
        }

        public Cell Agregate(string tableName, string searchColumnName, Cell searchValue,
            string targetColumnName, OperationType operation)
        {
            Table table = database.GetTable(tableName);
            Column searchColumn = table.GetColumn(searchColumnName);
            Column targetColumn = table.GetColumn(targetColumnName);

            var selectedCells = new List<Cell>();
            for (int i = 0; i < searchColumn.ElementCount(); ++i)
            {
                Cell cellToBeChecked = searchColumn.GetElementAt(i);
                if (cellToBeChecked == searchValue)
                {
                    var clonedTargetCell = targetColumn.GetElementAt(i).Clone();
                    selectedCells.Add(clonedTargetCell);
                }
            }

            var aggregator = new OperationExecutor(selectedCells);
            return aggregator.Execute(operation);
        }

        public int TableCount()
        {
            var tableCount = database.TableCount();

            return tableCount;
        }
    }
}
